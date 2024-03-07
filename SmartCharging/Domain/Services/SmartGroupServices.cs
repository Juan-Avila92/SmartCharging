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

namespace SmartCharging.API.Domain.Services
{
    public class SmartGroupServices : ISmartGroupServices
    {
        private readonly IRepository _repo;
        private int _minimumCapacityInAmps;
        public SmartGroupServices(IRepository repository)
        {
            _repo = repository;
            _minimumCapacityInAmps = RangeValues.GetMinumumCapacityInAmps();
        }

        public async Task<IResult> Create(SmartGroupDTO smartGroupDTO)
        {
            var newSmartGroup = new SmartGroup()
            {
                Name = smartGroupDTO.Name,
                CapacityInAmps = smartGroupDTO.CapacityInAmps
            };

            var canBeCreated = IsCapacityInAmpsGreaterThanZero(smartGroupDTO.CapacityInAmps);

            if(!canBeCreated)
            {
                return Results.BadRequest("Unable to create group. Capacity in Amps must be greater than zero.");
            }

            _repo.Create(newSmartGroup);
            await _repo.SaveChangesAsync();

            return Results.Ok("A new group has been created.");
        }

        public List<SmartGroup> GetAll()
        {
            var smartGroups = _repo.GetAll<SmartGroup>();

            return smartGroups;
        }

        public async Task<IResult> Update(Guid id, SmartGroupDTO smartGroupDTO)
        {
            var groupToBeUpdated = _repo.GetById<SmartGroup>(id);

            groupToBeUpdated.Name = smartGroupDTO.Name;
            groupToBeUpdated.CapacityInAmps = smartGroupDTO.CapacityInAmps;

            var canBeUpdated = IsCapacityInAmpsGreaterThanZero(smartGroupDTO.CapacityInAmps);

            if (!canBeUpdated)
            {
                return Results.BadRequest("Unable to create group. Capacity in Amps must be greater than zero.");
            }

            _repo.Update(groupToBeUpdated);
            await _repo.SaveChangesAsync();

            return Results.Ok("Group has been updated.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _repo.DeleteById<SmartGroup>(id);
            await _repo.SaveChangesAsync();

            return Results.Ok("Group has been deleted.");
        }

        private bool IsCapacityInAmpsGreaterThanZero(int capacityInArms)
        {
            if (capacityInArms > _minimumCapacityInAmps)
                return true;

            return false;
        }
    }
}
