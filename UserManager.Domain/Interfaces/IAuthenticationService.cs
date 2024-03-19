using UserManager.Data.Entities;
using UserManager.Domain.Models.Dtos;
using UserManager.Domain.Models;

namespace UserManager.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResult> LoginAsync(LoginCredentialsDto loginUserDto);
    }
}
