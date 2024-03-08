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
using System.ComponentModel;
using Microsoft.IdentityModel.Tokens;

namespace SmartCharging.API.Domain.Services
{
    public class SmartGroupServices : ISmartGroupServices
    {
        private readonly IRepository _repo;
        private readonly ISmartGroupsRepository _smartGroupRepo;
        private int _minimumCapacityInAmps;
        public SmartGroupServices(IRepository repository,
            ISmartGroupsRepository smartGroupRepo)
        {
            _repo = repository;
            _minimumCapacityInAmps = RangeValues.GetMinumumCapacityInAmps();
            _smartGroupRepo = smartGroupRepo;
        }

        public async Task<Result<SmartGroup>> Create(SmartGroupDTO smartGroupDTO)
        {
            var newSmartGroup = new SmartGroup()
            {
                Name = smartGroupDTO.Name,
                CapacityInAmps = smartGroupDTO.CapacityInAmps
            };

            var canBeCreated = IsCapacityInAmpsGreaterThanZero(smartGroupDTO.CapacityInAmps);

            if(!canBeCreated)
            {
                return new Result<SmartGroup>().Fail("Unable to create group. Capacity in Amps must be greater than zero.");
            }

            var createdSmartGroup = _repo.Create(newSmartGroup);
            await _repo.SaveChangesAsync();

            return new Result<SmartGroup>().Ok("Group has been created").WithData(createdSmartGroup);
        }

        public List<SmartGroup> GetAll()
        {
            var smartGroups = _smartGroupRepo.GetAllWithChargeStations();

            return smartGroups;
        }

        public async Task<Result<SmartGroup>> Update(Guid id, SmartGroupDTO smartGroupDTO)
        {
            var groupToBeUpdated = _repo.GetById<SmartGroup>(id);

            groupToBeUpdated.Name = smartGroupDTO.Name;
            groupToBeUpdated.CapacityInAmps = smartGroupDTO.CapacityInAmps;

            var canBeUpdated = IsCapacityInAmpsGreaterThanZero(smartGroupDTO.CapacityInAmps);

            if (!canBeUpdated)
            {
                return new Result<SmartGroup>().Fail("Unable to update group. Capacity in Amps must be greater than zero.");
            }

            _repo.Update(groupToBeUpdated);
            await _repo.SaveChangesAsync();

            return new Result<SmartGroup>().Ok("Group has been updated").WithData(groupToBeUpdated);
        }

        public async Task<Result> Delete(Guid id)
        {
            await _repo.DeleteById<SmartGroup>(id);
            await _repo.SaveChangesAsync();

            return new Result().Ok("Group has been deleted.");
        }

        private bool IsCapacityInAmpsGreaterThanZero(int capacityInArms)
        {
            if (capacityInArms > _minimumCapacityInAmps)
                return true;

            return false;
        }
    }
}
