using Management.Users.Dto;

namespace Management.Users
{
    public interface IUserService
    {
        UserDetailDto GetById(int userId);
        List<UserDto> GetAll();
        void Create(UserCreateDto createUserDto);
        void Update(int id, UserUpdateDto updatedUserDto);
        void Delete(int userId);
    }
}
