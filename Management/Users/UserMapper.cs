using AutoMapper;
using Management.Auth.Dto;
using Management.Roles.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;

namespace Management.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<RoleUpdateDto, User>();
            CreateMap<SignUpRequestDto, User>();
            CreateMap<SignInRequestDto, User>();
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role)));
        }
    }

}
