using Management.Auth.Dto;
using Management.Users.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentWebApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;

namespace Management.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, AppDbContext context, IConfiguration configuration)
        {
            _authService = authService;
            _context=context;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequestDto signUpRequest)
        {
            var signUpResult = _authService.SignUp(signUpRequest);

            if (signUpResult.Contains("already in use"))
            {
                return Conflict(signUpResult);
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == signUpRequest.Email);
            var token = GenerateJwtToken(user);
            var response = new SignUpResponseDto
            {
                AccessToken = token
            };

            return Ok(response);
        }
       
        [HttpPost("signin")]
        [Authorize]
        public IActionResult SignIn([FromBody] SignInRequestDto signInRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == signInRequest.Email||u.UserName==signInRequest.Username);
            if (user == null )
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }
            var token = GenerateJwtToken(user);
            var response = new SignInResponseDto
            {
                AccessToken = token,
            };
            return Ok(response);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.FirstName)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
