using Management.Users.Dto;

namespace Management.Roles.Dto
{
    public class RoleDetailDto
    {
        public string Name { get; set; }
        public List<UserDto>Users { get; set; }
    }
}
