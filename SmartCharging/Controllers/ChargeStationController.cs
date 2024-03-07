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
        private readonly IRepository _repo;

        public ChargeStationController(IChargeStationServices services, 
            IRepository repo,
            IChargeStationViewModelService chargeStationViewModelService)
        {
            _chargeStationServices = services;
            _repo = repo;
            _chargeStationViewModelService = chargeStationViewModelService;
        }

        [HttpPost("/[controller]/group/{groupId}")]
        public async Task<IResult> Create([FromRoute] Guid groupId, [FromBody] ChargeStationDTO chargeStationDTO)
        {
            var doesGroupExist = await _repo.ExistAsync<SmartGroup>(group => group.SmartGroupId.Equals(groupId));
            var doesChargeStationExistInGroupAlready = await _repo.ExistAsync<ChargeStation>(chargeStation => chargeStation.SmartGroupId.Equals(groupId));

            if (!doesGroupExist)
                return Results.BadRequest("Cannot create a charge station because group doesn't exist.");

            if (doesChargeStationExistInGroupAlready)
                return Results.BadRequest("Cannot create a charge station because it already belongs to a group.");

            var result = await _chargeStationServices.Create(groupId, chargeStationDTO);

            return result;
        }

        [HttpGet("[controller]/All")]
        public Task<IActionResult> GetAll()
        {
            var chargeStations = _chargeStationServices.GetAll();

            var viewModels = _chargeStationViewModelService.ConvertToViewModel(chargeStations);

            return Task.FromResult<IActionResult>(Json(viewModels));
        }

        [HttpDelete("/[controller]/{id}")]
        public async Task<IResult> Delete([FromRoute] Guid id)
        {
            var result = await _chargeStationServices.Delete(id);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] ChargeStationDTO chargeStationDTO)
        {
            var result = await _chargeStationServices.Update(id, chargeStationDTO);

            return result;
        }
    }
}
