using Management.Roles.Dto;

namespace Management.Users.Dto
{
    public class UserDetailDto
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
