namespace Management.Roles.Dto
{
    public class RoleCreateDto
    {
        public string Name { get; set; }
        public List<int>UserIds { get; set; }
    }
}
