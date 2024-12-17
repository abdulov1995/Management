using Management.Roles.Dto;

namespace Management.Roles
{
    public interface IRoleService
    {
        Task<RoleDetailDto> GetByIdAsync(int roleId);
        Task<List<RoleDto>> GetAllAsync();
        Task CreateAsync(RoleCreateDto createRoleDto);
        Task UpdateAsync(int id, RoleUpdateDto updatedRoleDto);
        Task DeleteAsync(int roleId);
    }
}
