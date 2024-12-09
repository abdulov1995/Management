using FluentValidation;
using Management.Auth.Dto;
using Management.Extentions.TokenHelper;
using Management.Users.Dto;
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
    [Route("api/auth")]

    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;
        private readonly TokenHelper _tokenHelper;
        public AuthController(IAuthService authService, AppDbContext context, TokenHelper tokenHelper)
        {
            _authService = authService;
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequestDto signUpRequest)
        {
            var user = _authService.SignUp(signUpRequest);
            var token = _tokenHelper.GenerateJwtToken(user);
           
            var response = new SignUpResponseDto
            {
                AccessToken = token
            };
            return Ok(response);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] SignInRequestDto signInRequest)
        {
            
            var user = _authService.SignIn(signInRequest);
            var token = _tokenHelper.GenerateJwtToken(user);
            var response = new SignInResponseDto
            {
                AccessToken = token,
            };
            return Ok(response);
        }
    }
}
