using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;

namespace Wave.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettingCommand _settingCommand;

        public JwtService(IOptions<JwtSettingCommand> settingCommand)
        {
            _settingCommand = settingCommand.Value;
        }

        public string GenerateToken(string userId, string userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingCommand.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(ClaimTypes.Role, userRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _settingCommand.Issuer,
                audience: _settingCommand.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settingCommand.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
