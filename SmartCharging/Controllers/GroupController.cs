using Microsoft.AspNetCore.Mvc;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.DTOs;
using SmartCharging.API.ViewModelServices.Contracts;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class GroupController : Controller
    {
        private readonly ISmartGroupServices _smartGroupServices;
        private readonly ISmartGroupViewModelService _smartGroupViewModelService;

        public GroupController(ISmartGroupServices services,
            ISmartGroupViewModelService smartGroupViewModelService)
        {
            _smartGroupServices = services;
            _smartGroupViewModelService = smartGroupViewModelService;
        }

        [HttpPost("[controller]")]
        public async Task<IResult> Create([FromBody] SmartGroupDTO smartGroupDTO)
        {
            var result = await _smartGroupServices.Create(smartGroupDTO);

            return result;
        }

        [HttpGet("[controller]/All")]
        public Task<IActionResult> GetAll()
        {
            var groups = _smartGroupServices.GetAll();

            var viewModels = _smartGroupViewModelService.ConvertToViewModel(groups);

            return Task.FromResult<IActionResult>(Json(viewModels));
        }

        [HttpDelete("[controller]/{id}")]
        public async Task<IResult> Delete([FromRoute] Guid id)
        {
            var result = await _smartGroupServices.Delete(id);

            return result;
        }

        [HttpPut("[controller]/{id}")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] SmartGroupDTO smartGroupDTO)
        {
            var result = await _smartGroupServices.Update(id, smartGroupDTO);

            return result;
        }
    }
}
