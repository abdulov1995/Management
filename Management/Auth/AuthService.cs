using AutoMapper;
using Management.Auth.Dto;
using Management.Extentions.Helpers;
using Management.Users;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;

        public AuthService(AppDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public User SignUp(SignUpRequestDto signUpRequest)
        {
            var existingUsername = _context.Users.FirstOrDefault(u => u.UserName == signUpRequest.Username);
            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == signUpRequest.Email);

            if (existingEmail != null)
            {
                throw new ArgumentException("Email is already in use.");
            }
            else if (existingUsername != null)
            {
                throw new ArgumentException("Username is already in use.");
            }

            var newUser = new UserCreateDto
            {
                FirstName = signUpRequest.Username,
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
            };

            return _userService.Create(newUser);
        }

        public User SignIn(SignInRequestDto signInRequest)
        {
            var user = _context.Users.Include(u=>u.Role).FirstOrDefault(u => u.Email == signInRequest.Email || u.UserName == signInRequest.Username);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var hashPassword = PasswordHelper.CreateMd5(signInRequest.Password);
            if (user.Password != hashPassword)
            {
                throw new ArgumentException("Invalid credentials.");
            }
            return user;
        }
    }
}
