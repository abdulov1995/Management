using AutoMapper;
using Management.Auth.Dto;
using Management.Extentions.Helpers;
using Management.Users.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentWebApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Management.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string SignUp(SignUpRequestDto signUpRequest)
        {
            var existingUsername = _context.Users.FirstOrDefault(u => u.UserName == signUpRequest.Username);
            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == signUpRequest.Email);

            if (existingEmail != null)
            {
                return "Email is already in use.";
            }
            else if (existingUsername != null)
            {
                return "Username is already in use.";
            }

            var newUser = _mapper.Map<User>(signUpRequest);
            newUser.Password = PasswordHelper.CreateMd5(signUpRequest.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "SignUp successfull!!";
        }

        public string SignIn(SignInRequestDto signInRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == signInRequest.Email || u.UserName == signInRequest.Username);
            var hashPassword=PasswordHelper.CreateMd5(signInRequest.Password);
            if (user == null && user.Password == hashPassword)
            {
                return user.ToString();
            }
            return null;
        }
    }
}
