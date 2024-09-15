//using Microsoft.IdentityModel.Tokens;
//using MongoDBUsers.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace MongoDBUsers.Helpers

//{
//    public class TokenHelper
//    {
//        IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//        public string GenerateToken(LoginUsers u)
//        {
//            string secret = _configuration["Jwt:secret"];
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
//            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//            var claims = new[] {
//                    new Claim("Role", u.role),
//                    new Claim("Userid",u.userid),
//            };
//            var token = new JwtSecurityToken(_configuration["Jwt:issuer"],
//              _configuration["Jwt:audience"],
//             claims,
//             expires: DateTime.Now.AddDays(2),
//             signingCredentials: credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//    }
//}
using Microsoft.IdentityModel.Tokens;
using MongoDBUsers.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MongoDBUsers.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public string GenerateToken(LoginUsers user)
        {
            var secretKey = _configuration["Jwt:Secret"];
            var key = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(key);

            var claims = new[]
            {
                new Claim("Userid", user.userid),
                new Claim(ClaimTypes.Role, user.role)
            };

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
