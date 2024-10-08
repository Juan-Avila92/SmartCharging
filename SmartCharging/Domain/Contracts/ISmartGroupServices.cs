﻿using Microsoft.AspNetCore.Mvc;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.API.Domain.Contracts
{
    public interface ISmartGroupServices
    {
        Task<Result<SmartGroup>> Create(SmartGroupDTO smartGroupDTO);
        List<SmartGroup> GetAll();
        Task<Result<SmartGroup>> Update(Guid id, SmartGroupDTO smartGroupDTO);
        Task <Result> Delete(Guid id);
    }
}
