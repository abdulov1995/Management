namespace Management.Roles.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = "User";
        public List<UserRole> UserRoles { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
