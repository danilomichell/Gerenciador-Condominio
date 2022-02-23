using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Condominio.Domain.Entities;
using Condominio.Service.Services.Interface;
using Condominio.Util.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Condominio.Service.Services;

public class TokenService : ITokenService
{
    private readonly string _jwtSecretKey;

    public TokenService(IConfiguration configuration)
    {
        _jwtSecretKey = configuration.GetSection("CryptographConfig:JwtSecretKey").Value;
    }

    public async Task<string> TokenGenerate(int id, string name, EnumCargo cargo)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("UserId", id.ToString()),
                new Claim("Cargo", cargo.GetDescription()),
                new Claim(ClaimTypes.Role, cargo.GetDescription())
            }),
            Expires = DateTime.UtcNow.AddHours(3),

            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
}