using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Contracts;

namespace SmartCharging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IRepository _repo;

        public GroupController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create()
        {
            var a = new SmartGroup { Name = "A" };
            _repo.Create(a);
            await _repo.SaveChangesAsync();

            return Ok("Created");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _repo.DeleteById<SmartGroup>(id);
            await _repo.SaveChangesAsync();

            return Ok("Removed");
        }
    }
}
