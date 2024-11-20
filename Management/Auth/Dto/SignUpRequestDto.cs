using Management.Roles.Model;
using System.ComponentModel.DataAnnotations;

namespace Management.Auth.Dto
{
    public class SignUpRequestDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
       // [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
