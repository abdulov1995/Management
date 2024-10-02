namespace Management.Roles.Dto
{
    public class RoleUpdateDto
    {
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
    }
}
