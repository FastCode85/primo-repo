/*

    Questa classe si occupa di generare i token JWT per l'autenticazione degli utenti.
    Utilizza i dati di configurazione presenti in appsettings.json per creare il token.

*/

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Models;

namespace Rubrica.Api.Helpers;
public class JwtHelper
{
    /*
        Viene usato per accedere ai dati di configurazione JWT in appsettings.json
    */
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
            issuer : issuer,  //chi ha creato il token
            audience : audience,  //chi può usare il token
            claims : claims,
            expires : DateTime.UtcNow.AddHours(1),
            signingCredentials : credentials

        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}