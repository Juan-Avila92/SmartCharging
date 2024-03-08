using Microsoft.AspNetCore.Mvc;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Services;
using SmartCharging.API.DTOs;
using SmartCharging.API.ViewModelServices.Contracts;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class ChargeStationController : Controller
    {
        private readonly IChargeStationServices _chargeStationServices;
        private readonly IChargeStationViewModelService _chargeStationViewModelService;

        public ChargeStationController(IChargeStationServices services,
            IChargeStationViewModelService chargeStationViewModelService)
        {
            _chargeStationServices = services;
            _chargeStationViewModelService = chargeStationViewModelService;
        }

        [HttpPost("/[controller]/group/{groupId}")]
        public async Task<IActionResult> Create([FromRoute] Guid groupId, [FromBody] ChargeStationDTO chargeStationDTO)
        {
            var result = await _chargeStationServices.Create(groupId, chargeStationDTO);

            return Json(result);
        }

        [HttpGet("[controller]/All")]
        public Task<IActionResult> GetAll()
        {
            var chargeStations = _chargeStationServices.GetAll();

            var viewModels = _chargeStationViewModelService.ConvertToViewModel(chargeStations);

            return Task.FromResult<IActionResult>(Json(viewModels));
        }

        [HttpDelete("/[controller]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _chargeStationServices.Delete(id);

            return Json(result);
        }

        [HttpPut("[Controller]/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ChargeStationBody body)
        {

            var result = await _chargeStationServices.Update(id, new ChargeStationDTO() { Name = body.Name});

            return Json(result);
        }
    }
}
