using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManager.Data.Entities;
using UserManager.Domain.Interfaces;
using UserManager.Domain.Models;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private SigningCredentials Credentials { get; set; }
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(IConfiguration config, SignInManager<User> signInManager)
        {
            Credentials = ConfigureCredentials(config);
            _signInManager = signInManager;
        }

        public async Task<LoginResult> LoginAsync(LoginCredentialsDto loginUserDto)
        {
            var accessToken = string.Empty;
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);

            if (result.Succeeded)
            {
                var user = _signInManager.UserManager.Users
                    .Where(user => !string.IsNullOrEmpty(user.UserName))
                    .First(user => user.UserName!.ToUpper().Equals(loginUserDto.Username.ToUpper()));

                accessToken = ObtainTokenValue(user);
            }

            return new LoginResult(accessToken);
        }

        private static SigningCredentials ConfigureCredentials(IConfiguration config)
        {
            var key = config.GetSection("JwtKey").Value;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Impossible to proceed the inicialization without a Jwt Key configured");
            }

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
        }

        private string ObtainTokenValue(User user)
        {
            var expirationTime = DateTime.Now.Add(TimeSpan.FromMinutes(30));
            Claim[] claims =
            [
                new("userId", user.Id),
                new("userName", user.UserName!),
            ];

            var token = new JwtSecurityToken(expires: expirationTime, claims: claims, signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}