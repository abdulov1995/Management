using Management.Users.Model;

namespace Management.Roles.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
