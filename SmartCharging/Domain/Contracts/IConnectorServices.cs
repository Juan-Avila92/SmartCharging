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
    public interface IConnectorServices
    {
        Task<Result<Connector>> CreateAsync(Guid chargeStationId, ConnectorDTO connectorDTO);
        Task<Result<Connector>> UpdateAsync(int id, ConnectorDTO connectorDTO);
        Task <Result<Connector>> DeleteAsync(int id);
    }
}
