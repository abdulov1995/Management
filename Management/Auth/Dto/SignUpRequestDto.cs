using Management.Roles.Model;

namespace Management.Auth.Dto
{
    public class SignUpRequestDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
