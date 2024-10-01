namespace Management.Roles.Dto
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public List<int>UserIds { get; set; }
    }
}
