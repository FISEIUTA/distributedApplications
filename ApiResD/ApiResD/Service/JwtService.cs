using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiResD.Context;
using ApiResD.Models.dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ApiResD.Service
{
    public class JwtService
    {
        private readonly AppDB dbContext;
        private readonly IConfiguration configuration;
        public JwtService(AppDB dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<LogginResponseDto?> Authenticate(LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.userName) || string.IsNullOrWhiteSpace(loginRequest.password))
            {
                return null;  
            }
            var user = dbContext.User.FirstOrDefault(u => u.userName == loginRequest.userName);
            if (user == null || !PasswordHasher.VerifyPassword(loginRequest.password,user.password))
                return null;

            var issuer = configuration["JwtConfig:Issuer"];
            var audience = configuration["JwtConfig:Audience"];
            var key = configuration["JwtConfig:Key"];
            var tokenValidityMins = configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature),
                
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginRequest.userName)
                }),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LogginResponseDto
            {
                Accesstoken = accessToken,
                ExpirationIn = (int)tokenExpiration.Subtract(DateTime.UtcNow).TotalSeconds,
                UserName = user.userName
            };
        }
    }
}
