using AutoMapper;
using Management.Roles.Dto;
using Management.Roles.Model;

namespace Management.Roles
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
            CreateMap<Role, RoleDetailDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.User)));
        }
    }
}

