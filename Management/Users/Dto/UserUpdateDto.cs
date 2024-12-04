namespace Management.Users.Dto
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public int Age { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
