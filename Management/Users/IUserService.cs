using Management.Users.Dto;

namespace Management.Users
{
    public interface IUserService
    {
        UserDetailDto GetById(int userId);
        List<UserDto> GetAll();
        void Create(CreateUserDto createUserDto);
        void Update(int id, UpdateUserDto updatedUserDto);
        void Delete(int userId);
    }
}
