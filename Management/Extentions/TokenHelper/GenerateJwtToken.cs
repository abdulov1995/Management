using Management.Users.Model;
using Management.Roles.Model;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Management.Auth.Dto;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

namespace Management.Extentions.TokenHelper
{
    public  class TokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public TokenHelper(IConfiguration configuration,AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public  string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.Name)
                    }),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
