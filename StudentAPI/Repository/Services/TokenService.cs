using Microsoft.IdentityModel.Tokens;
using StudentAPI.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI.Repository.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateToken(string userName, string role)
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.Name , userName),
              new Claim(ClaimTypes.Role , role),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:IssuerSigningKey"])
                );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _config["Jwt:Issuer"],
                  audience: _config["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(
                      Convert.ToDouble(_config["Jwt:TokenDuration"])
                      ),
                  signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
