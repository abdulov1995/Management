using Management.Roles.Dto;

namespace Management.Roles
{
    public interface IRoleService
    {
        RoleDetailDto GetById(int roleId);
        List<RoleDto> GetAll();
        void Create(RoleCreateDto createRoleDto);
        void Update(int id, RoleUpdateDto updatedRoleDto);
        public void Delete(int roleId);
    }
}
