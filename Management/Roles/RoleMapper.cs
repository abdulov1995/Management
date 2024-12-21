using AutoMapper;
using Management.Roles.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;

namespace Management.Roles
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            CreateMap<User, UserDto>();

            CreateMap<Role, RoleDetailDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.User)));
        }
    }
}

