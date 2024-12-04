namespace Management.Users.Dto
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
    }
}
