using AutoMapper;
using UserManager.Data.Entities;
using UserManager.Domain.Models.Dtos;

namespace UserManager.Domain.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpsertUserDto, User>();
            CreateMap<LoginCredentialsDto, User>();
            CreateMap<User, UpsertUserDto>();
        }
    }
}
