using Management.Roles.Model;

namespace Management.Users.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
