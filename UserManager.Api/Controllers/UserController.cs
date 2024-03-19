using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManager.Domain.Interfaces;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpsertUserDto userDto)
        {
            var result = await _userService.CreateAsync(userDto);
            if (result.Succeeded)
            {
                return Created(string.Empty, userDto);
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Read()
        {
            var userId = GetUserIdFromContext();
            var result = await _userService.ReadAsync(userId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        private string GetUserIdFromContext()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity!.Claims.FirstOrDefault(x => x.Type.Equals("userId"))!.Value;
        }
    }
}
