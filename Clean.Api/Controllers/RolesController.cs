using Application.Features.Roles;
using Application.Features.Roles.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await roleService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await roleService.GetByIdAsync(id));

        [HttpPost]  
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
            => Ok(await roleService.CreateRoleAsync(request));

    }
}
