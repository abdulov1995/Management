using Management.Auth.Dto;
using Management.Users.Model;

namespace Management.Auth
{
    public interface IAuthService
    {
        string SignUp(SignUpRequestDto signUpRequest);
        string SignIn(SignInRequestDto signInRequest);
    }
}
