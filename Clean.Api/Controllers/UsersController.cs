using Application.Features.User;
using Application.Features.User.Create;
using Clean.Api.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, AppDbContext contex) : CustomBaseController
    {
       


        [HttpGet]
        public async Task<IActionResult> GetAll()
            => CustomActionResult(await userService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => CustomActionResult(await userService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
            => CustomActionResult(await userService.CreateUserAsync(request));



        [HttpGet("token/{userName}")]
        public async Task<IActionResult> GetToken(string userName)
            => CustomActionResult(await userService.GetTokenAsync(userName));




    }
}
