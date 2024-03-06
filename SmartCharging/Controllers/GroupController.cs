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

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            var a = new SmartGroup { Name = "A" };
            _repo.Create(a);
            await _repo.SaveChangesAsync();

            return Ok("Created");
        }
    }
}
