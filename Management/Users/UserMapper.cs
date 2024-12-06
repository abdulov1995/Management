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
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<RoleUpdateDto, User>();
            CreateMap<SignUpRequestDto, User>();
            CreateMap<SignInRequestDto, User>();
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }

}
