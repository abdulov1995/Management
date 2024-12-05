using AutoMapper;
using Management.Auth.Dto;
using Management.Extentions.Helpers;
using Management.Extentions.TokenHelper;
using Management.Users;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

namespace Management.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly TokenHelper _tokenHelper;


        public AuthService(AppDbContext context, IMapper mapper, IUserService userService, TokenHelper tokenHelper)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _tokenHelper = tokenHelper;

        }
        public User SignUp(SignUpRequestDto signUpRequest)
        {
            var existingUserName = _context.Users.FirstOrDefault(u => u.UserName == signUpRequest.UserName);
            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == signUpRequest.Email);
           
             if (existingEmail != null)
            {
                throw new ArgumentException("Email is already in use.");
            }
            else if (existingUserName != null)
            {
                throw new ArgumentException("UserName is already in use.");
            }
            var role = _context.Roles.FirstOrDefault(r => r.Id == 2);
            var newUser = new UserCreateDto
            {
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email,
                Password = PasswordHelper.CreateMd5(signUpRequest.Password),
                FirstName = signUpRequest.FirstName,
                LastName = signUpRequest.LastName,
               
                Age = signUpRequest.Age,
               
                RoleId = 2
            };

            return _userService.Create(newUser);
        }

        public User SignIn(SignInRequestDto signInRequest)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == signInRequest.Email || u.UserName == signInRequest.UserName);

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
