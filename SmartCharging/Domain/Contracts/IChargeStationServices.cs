using Microsoft.AspNetCore.Mvc;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.API.Domain.Contracts
{
    public interface IChargeStationServices
    {
        Task<IResult> Create(Guid groupId, ChargeStationDTO chargeStationDTO);
        List<ChargeStation> GetAll();
        Task<IResult> Update(Guid id, ChargeStationDTO chargeStationDTO);
        Task<IResult> Delete(Guid id);
    }
}
