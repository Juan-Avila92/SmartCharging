using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.API.DTOs;
using SmartCharging.API.Domain.Utilities;
using SmartCharging.API.Data.Repository.Repos;

namespace SmartCharging.API.Domain.Services
{
    public class ChargeStationServices : IChargeStationServices
    {
        private readonly IRepository _repo;
        private readonly IChargeStationRepository _chargeStationRepo;
        private readonly IConnectorServices _connectorServices;

        public ChargeStationServices(IRepository repository,
            IConnectorServices connectorServices,
            IChargeStationRepository chargeStationRepo)
        {
            _repo = repository;
            _connectorServices = connectorServices;
            _chargeStationRepo = chargeStationRepo;
        }

        public async Task<Result<ChargeStation>> Create(Guid groupId, ChargeStationDTO chargeStationDTO)
        {
            var doesGroupExist = await _repo.ExistAsync<SmartGroup>(group => group.SmartGroupId.Equals(groupId));

            if (chargeStationDTO.Connectors.Any(c => c.MaxCurrentInAmps <= 0))
                return new Result<ChargeStation>().Fail("Cannot create a charge station because at least one connector has got invalid current amps.");

            if (!doesGroupExist)
                return new Result<ChargeStation>().Fail("Cannot create a charge station because group doesn't exist.");

            var newChargeStation = CreateNewChargeStation(groupId, chargeStationDTO);

            var createdChargeStation = _repo.Create(newChargeStation);

            if (createdChargeStation.Connectors.Count < 1)
                return new Result<ChargeStation>().Fail("Cannot create a charge station. There should be at least one connector");

            if (createdChargeStation.Connectors.Count > 5)
                return new Result<ChargeStation>().Fail("Cannot create a charge station. Number of Connectors should be less than 5");



            await _repo.SaveChangesAsync();

            return new Result<ChargeStation>().Ok("A new charge station has been created.").WithData(createdChargeStation);
        }

        public List<ChargeStation> GetAll()
        {
            var chargeStations = _chargeStationRepo.GetAllWithConnectors();

            return chargeStations;
        }

        public async Task<Result<ChargeStation>> Update(Guid id, ChargeStationDTO chargeStationDTO)
        {
            var chargeStationToBeUpdated = _repo.GetById<ChargeStation>(id);

            chargeStationToBeUpdated.Name = chargeStationDTO.Name;

            _repo.Update(chargeStationToBeUpdated);
            await _repo.SaveChangesAsync();

            return new Result<ChargeStation>().Ok("Charge station has been updated.").WithData(chargeStationToBeUpdated);
        }

        public async Task<Result<ChargeStation>> Delete(Guid id)
        {
            await _repo.DeleteById<ChargeStation>(id);
            await _repo.SaveChangesAsync();

            return new Result<ChargeStation>().Ok("Charge station has been updated.");
        }

        private ChargeStation CreateNewChargeStation(Guid smartGroupId, ChargeStationDTO chargeStationDTO)
        {
            return new ChargeStation()
            {
                SmartGroupId = smartGroupId,
                Name = chargeStationDTO.Name,
                Connectors = chargeStationDTO.Connectors.Select(c => new Connector() { MaxCurrentInAmps = c.MaxCurrentInAmps}).ToList()
            };
        }
    }
}
