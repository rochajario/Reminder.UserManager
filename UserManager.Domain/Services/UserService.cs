using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManager.Data.Entities;
using UserManager.Domain.Interfaces;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UserService(IMapper mapper, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public Task<IdentityResult> CreateAsync(UpsertUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            return _signInManager
                .UserManager
                .CreateAsync(user, userDto.Password);
        }

        public async Task<IdentityResult> UpdateAsync(string id, UpsertUserDto userDto)
        {
            User user = await _signInManager.UserManager.Users.Where(x => x.Id.Equals(id)).SingleAsync();
            
            user = _mapper.Map<User>(userDto);
            user.Id = id;

            return _signInManager.UserManager.UpdateAsync(user).Result;
        }

        public async Task<UpsertUserDto> ReadAsync(string id)
        {
            var result = await _signInManager.UserManager.Users.Where(x => x.Id.Equals(id)).SingleAsync();
            return _mapper.Map<UpsertUserDto>(result);

        }

        public Task DeleteAsync(string id)
        {
            return _signInManager.UserManager.Users.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
        }
    }
}