using Management.Roles.Model;

namespace Management.Users.Model
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
