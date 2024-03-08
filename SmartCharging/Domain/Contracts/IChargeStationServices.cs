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
        Task<Result<ChargeStation>> Create(Guid groupId, ChargeStationDTO chargeStationDTO);
        List<ChargeStation> GetAll();
        Task<Result<ChargeStation>> Update(Guid id, ChargeStationDTO chargeStationDTO);
        Task<Result<ChargeStation>> Delete(Guid id);
    }
}
