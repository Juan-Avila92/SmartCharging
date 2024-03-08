using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCharging.API.Data;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Services;
using SmartCharging.API.DTOs;
using SmartCharging.API.ViewModelServices.Contracts;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class ConnectorController : Controller
    {
        private readonly IChargeStationServices _chargeStationServices;
        private readonly IConnectorServices _connectorServices;

        public ConnectorController(IChargeStationServices services,
            IConnectorServices connectorServices)
        {
            _chargeStationServices = services;
            _connectorServices = connectorServices;
        }

        [HttpPost("/[controller]/ChargeStation/{chargeStationId}")]
        public async Task<IActionResult> Create([FromRoute] Guid chargeStationId, [FromBody] ConnectorDTO chargeStationDTO)
        {
            var results = await _connectorServices.CreateAsync(chargeStationId, chargeStationDTO);

            return Json(results);
        }

        [HttpDelete("/[controller]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _connectorServices.DeleteAsync(id);

            return Json(result);
        }

        [HttpPut("[Controller]/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ConnectorDTO connectorDTO)
        {
            var results = await _connectorServices.UpdateAsync(id, connectorDTO);

            return Json(results);
        }
    }
}
