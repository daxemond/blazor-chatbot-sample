using BlazorChatbot.Models;
using BlazorChatbot.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;

namespace BlazorChatbot.Services
{
    public class TokenService : ITokenService
    {
        private string? _token = null;
        private readonly SecurityOption _config;
        public TokenService(IOptions<SecurityOption> options)
        {
            _config = options.Value;
        }

        public string GetServerBearerToken(string email)
        {
            if (_token == null)
            {
                string tk = generateToken(email);
                _token = tk;
            }
            return _token ?? "";
        }

        public string GetGuestBearerToken()
        {
            return generateToken("dummy@hotmail.com");
        }

        private string generateToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Aud, _config.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, _config.ClientId),
                new Claim(JwtRegisteredClaimNames.Sub,_config.ClientId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _config.ClientId,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }

}
