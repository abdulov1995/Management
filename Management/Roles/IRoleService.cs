using Management.Roles.Dto;

namespace Management.Roles
{
    public interface IRoleService
    {
        RoleDetailDto GetById(int roleId);
        List<RoleDto> GetAll();
        void Create(CreateRoleDto createRoleDto);
        void Update(int id, UpdateRoleDto updatedRoleDto);
        public void Delete(int roleId);
    }
}
