using AutoMapper;
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
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateRoleDto, User>();
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role)));
        }
    }

}
