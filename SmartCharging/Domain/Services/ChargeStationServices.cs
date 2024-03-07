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

namespace SmartCharging.API.Domain.Services
{
    public class ChargeStationServices : IChargeStationServices
    {
        private readonly IRepository _repo;
        private int _minimumNumberOfConnectors;
        private int _maximumNumberOfConnectors;

        public ChargeStationServices(IRepository repository)
        {
            _repo = repository;
            _minimumNumberOfConnectors = RangeValues.GetMinumumNumberOfConnectors();
            _maximumNumberOfConnectors = RangeValues.GetMaximumNumberOfConnectors();
        }

        public async Task<IResult> Create(Guid groupId, ChargeStationDTO chargeStationDTO)
        {
            var isNumberOfConnectorsAllowed = ValidateNumberOfConnectors(chargeStationDTO.Connectors);

            if(!isNumberOfConnectorsAllowed)
            {
                return Results.BadRequest("Unable to create charge station. Connecto number must be between 1 and 5");
            }

            var newChargeStation = CreateNewChargeStation(groupId, chargeStationDTO);

            _repo.Create(newChargeStation);
            await _repo.SaveChangesAsync();

            return Results.Ok("A new charge station has been created.");
        }

        public List<ChargeStation> GetAll()
        {
            var chargeStations = _repo.GetAll<ChargeStation>();

            return chargeStations;
        }

        public async Task<IResult> Update(Guid id, ChargeStationDTO chargeStationDTO)
        {
            var chargeStationToBeUpdated = _repo.GetById<ChargeStation>(id);

            chargeStationToBeUpdated.Name = chargeStationDTO.Name;
            chargeStationToBeUpdated.Connectors = chargeStationDTO.Connectors;

            var isNumberOfConnectorsAllowed = ValidateNumberOfConnectors(chargeStationDTO.Connectors);

            if (!isNumberOfConnectorsAllowed)
            {
                return Results.BadRequest("Unable to update charge station. Connecto number must be between 1 and 5");
            }

            _repo.Update(chargeStationToBeUpdated);
            await _repo.SaveChangesAsync();

            return Results.Ok("Charge station has been updated.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _repo.DeleteById<ChargeStation>(id);
            await _repo.SaveChangesAsync();

            return Results.Ok("Charge station has been deleted.");
        }

        private bool ValidateNumberOfConnectors(int connectors)
        {
            if(connectors >= _minimumNumberOfConnectors && connectors <= _maximumNumberOfConnectors)
            {
                return true;
            }

            return false;
        }

        private ChargeStation CreateNewChargeStation(Guid smartGroupId, ChargeStationDTO chargeStationDTO)
        {
            return new ChargeStation()
            {
                SmartGroupId = smartGroupId,
                Name = chargeStationDTO.Name,
                Connectors = chargeStationDTO.Connectors
            };
        }
    }
}
