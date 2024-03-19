using Microsoft.AspNetCore.Identity;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(UpsertUserDto userDto);
        Task DeleteAsync(string id);
        Task<UpsertUserDto> ReadAsync(string id);
        Task<IdentityResult> UpdateAsync(string id, UpsertUserDto userDto);
    }
}
