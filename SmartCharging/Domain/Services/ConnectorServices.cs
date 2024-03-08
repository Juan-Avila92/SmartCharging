using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.API.Data.Repository.Repos;

namespace SmartCharging.API.Domain.Services
{
    public class ConnectorServices : IConnectorServices
    {
        private readonly IRepository _repo;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly ISmartGroupsRepository _smartGroupRepo;

        public ConnectorServices(IRepository repository,
            ISmartGroupsRepository smartGroupRepo,
            IChargeStationRepository chargeStationRepository)
        {
            _repo = repository;
            _smartGroupRepo = smartGroupRepo;
            _chargeStationRepository = chargeStationRepository;
        }

        public async Task<Result<Connector>> CreateAsync(Guid chargeStationId, ConnectorDTO connectorDTO)
        {
            if (connectorDTO.MaxCurrentInAmps <= 0)
                return new Result<Connector>().Fail("Cannot create a conector because max current in amps must be greater than zero");

            var doesChargeStationExist = await _repo.ExistAsync<ChargeStation>(chargeStation => chargeStation.ChargeStationId.Equals(chargeStationId));

            if (!doesChargeStationExist)
                return new Result<Connector>().Fail("Cannot create a conector because charge station doesn't exist.");

            var chargeStationWithConnectors = _chargeStationRepository.GetChargeStationByIdWithConnectors(chargeStationId);
            var chargeStationGroup = _repo.GetById<SmartGroup>(chargeStationWithConnectors.SmartGroupId);
            var groupChargeStations = _smartGroupRepo.GetChargeStationsById(chargeStationGroup.SmartGroupId);

            var connector = new Connector()
            {
                ChargeStationId = chargeStationId,
                MaxCurrentInAmps = connectorDTO.MaxCurrentInAmps
            };


            _repo.Create(connector);

            if (chargeStationWithConnectors.Connectors.Count > 5)
            {
                return new Result<Connector>().Fail("Cannot create more than 5 connectors to a charge station.");
            }

            if (chargeStationWithConnectors.Connectors.Sum(s => s.MaxCurrentInAmps) > chargeStationGroup.CapacityInAmps)
            {
                return new Result<Connector>()
                    .Fail("Cannot create more connectors because max current" +
                    " in amps is greater than Capacity in Amps of the belonging group.");
            }
            
            await _repo.SaveChangesAsync();

            return new Result<Connector>().Ok("A new connector has been created.").WithData(connector);
        }

        public async Task<Result<Connector>> UpdateAsync(int id, ConnectorDTO connectorDTO)
        {
            var connectorToBeUpdated = _repo.GetById<Connector>(id);

            connectorToBeUpdated.MaxCurrentInAmps = connectorDTO.MaxCurrentInAmps;

            var chargeStationWithConnectors = _chargeStationRepository.GetChargeStationByIdWithConnectors(connectorToBeUpdated.ChargeStationId);
            var chargeStationGroup = _repo.GetById<SmartGroup>(chargeStationWithConnectors.SmartGroupId);

            var sumOfAllConnectorsCapacityInAmps = _smartGroupRepo.GetSumOfAllConnectorsAmpValuesById(chargeStationGroup.SmartGroupId);

            if (sumOfAllConnectorsCapacityInAmps > chargeStationGroup.CapacityInAmps)
            {
                return new Result<Connector>().Fail("Cannot create more connectors because max current in amps is greater than Capacity in Amps of the belonging group.");
            }

            _repo.Update(connectorToBeUpdated);
            await _repo.SaveChangesAsync();

            return new Result<Connector>().Ok("Connector has been updated.").WithData(connectorToBeUpdated);
        }

        public async Task<Result<Connector>> DeleteAsync(int id)
        {
            await _repo.DeleteById<Connector>(id);
            await _repo.SaveChangesAsync();

            return new Result<Connector>().Ok("Connector has been deleted.");
        }
    }
}
