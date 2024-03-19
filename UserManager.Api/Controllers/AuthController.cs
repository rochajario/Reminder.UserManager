using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Domain.Interfaces;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentialsDto loginUserDto)
        {
            var result = await _authenticationService.LoginAsync(loginUserDto);
            if (result.Valid)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
