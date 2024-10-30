using Management.Auth.Dto;
using Management.Users.Dto;
using Management.Users.Model;

namespace Management.Users
{
    public interface IUserService
    {
        UserDetailDto GetById(int userId);
        List<UserDto> GetAll();
        User Create(UserCreateDto createUserDto);
        void Update(int id, UserUpdateDto updatedUserDto);
        void Delete(int userId);
    }
}
