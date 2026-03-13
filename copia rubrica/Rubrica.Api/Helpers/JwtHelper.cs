using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Models;

namespace Rubrica.Api.Helpers;
public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration=configuration;
    }

    public string GenerateToken(ApplicationUser user)
    {
        //leggiamo i dati dal file appsettings.json
        string? key=_configuration["Jwt:Key"];
        string? issuer=_configuration["Jwt:Issuer"];
        string? audience=_configuration["Jwt:Audience"];

        if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) ||string.IsNullOrEmpty(audience))
        {
            throw new Exception("Configurazione JWT mancante.");
        }

        Claim[] claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName ?? ""),
            new Claim(ClaimTypes.Email,user.Email ?? "")
        };

        SymmetricSecurityKey securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        SigningCredentials credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer : issuer,
            audience : audience,
            claims : claims,
            expires : DateTime.UtcNow.AddHours(1),
            signingCredentials : credentials

        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}