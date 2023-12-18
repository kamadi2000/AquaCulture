using Fish_Farm.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fish_Farm.utils
{
    public class TokenGenerator
    {
        private IConfiguration _config;
        public TokenGenerator(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(string name, string email)
        {

            var issuer = _config["JWT:Issuer"];
            var audience = _config["JWT:Audience"];
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var subject = new ClaimsIdentity(new[]
             {

                    new Claim(JwtRegisteredClaimNames.Sub,name),
                    new Claim(JwtRegisteredClaimNames.Email,email),

             });
            var expires = DateTime.UtcNow.AddMinutes(120);
            var tokenDescriptor = new SecurityTokenDescriptor
             {

                    Subject = subject,
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials

             };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
            
        }
    }
}
