using Microsoft.IdentityModel.Tokens;
using MongoDBUsers.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MongoDBUsers.Helpers

{
    public class TokenHelper
    {
        IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public string GenerateToken(LoginUsers u)
        {
            string secret = _configuration["Jwt:secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                    new Claim("Role", u.role),
                    new Claim("Userid",u.userid),
            };
            var token = new JwtSecurityToken(_configuration["Jwt:issuer"],
              _configuration["Jwt:audience"],
             claims,
             expires: DateTime.Now.AddDays(2),
             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
