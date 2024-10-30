using Management.Auth.Dto;
using Management.Users.Model;

namespace Management.Auth
{
    public interface IAuthService
    {
        User SignUp(SignUpRequestDto signUpRequest);
        User SignIn(SignInRequestDto signInRequest);
    }
}
