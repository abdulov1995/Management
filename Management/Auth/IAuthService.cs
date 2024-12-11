using Management.Auth.Dto;
using Management.Users.Model;

namespace Management.Auth
{
    public interface IAuthService
    {
        Task<User> SignUp(SignUpRequestDto signUpRequest);
        Task<User> SignIn(SignInRequestDto signInRequest);
    }
}
