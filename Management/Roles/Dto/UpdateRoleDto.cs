namespace Management.Roles.Dto
{
    public class UpdateRoleDto
    {
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
    }
}
