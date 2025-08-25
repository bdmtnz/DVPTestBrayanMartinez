using DoubleVPartners.BackEnd.Domain.UserAggregate;
using DoubleVPartners.BackEnd.Domain.UserAggregate.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoubleVPartners.BackEnd.Infrastructure.Security
{
    public class JwtHandler(IOptions<JwtSettings> jwtOptions)
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        public string Generate(User user)
        {
            SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            SigningCredentials? credentials = new(key, SecurityAlgorithms.HmacSha256);

            List<Claim>? claims = new()
            {
                new Claim("id", user.Id.Value),
                new Claim("name", user.Name)
            };

            JwtSecurityToken? token = new(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Claim> Validate(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return [];
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = key,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                UserId.Create(jwtToken.Claims.First(x => x.Type == "id").Value);

                return jwtToken.Claims.ToList();
            }
            catch
            {
                return [];
            }
        }
    }
}
