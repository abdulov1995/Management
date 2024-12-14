using Management.Auth.Dto;
using Management.Users.Dto;
using Management.Users.Model;

namespace Management.Users
{
    public interface IUserService
    {
        Task<UserDetailDto> GetByIdAsync(int userId);
        Task<List<UserDto>> GetAllAsync();
        Task<User> CreateAsync(UserCreateDto createUserDto);
        Task UpdateAsync(int id, UserUpdateDto updatedUserDto);
        Task DeleteAsync(int userId);
    }
}
