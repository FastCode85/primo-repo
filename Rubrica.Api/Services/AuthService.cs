using Microsoft.AspNetCore.Identity;
using Rubrica.Api.Dtos;
using Rubrica.Api.Helpers;
using Rubrica.Api.Models;

namespace Rubrica.Api.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtHelper jwtHelper
        )
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _jwtHelper=jwtHelper;
    }

    /*
    Questo è un metodo asincrono che restituisce un IdentityResult, 
    che indica se la registrazione è riuscita o no, 
    e contiene eventuali errori e un metodo asincrono che è un metodo 
    che può essere eseguito in modo non bloccante, 
    cioè può fare operazioni che richiedono tempo 
    (come accedere al database) senza bloccare il thread principale 
    dell'applicazione
    */

    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        //controlliamo se esiste già un utente con questa email
        ApplicationUser? existingUser=await _userManager.FindByEmailAsync(dto.Email);

        if(existingUser!=null)
        {
            IdentityError error=new IdentityError();
            error.Description="Email già registrata.";
            
            List<IdentityError> errors=new List<IdentityError>();
            errors.Add(error);

            return IdentityResult.Failed(errors.ToArray());
        }

        //creiamo il nuovo utente
        ApplicationUser user=new ApplicationUser();
        user.UserName=dto.Email;//usiamo la mail anche come username
        user.Email=dto.Email;
        user.NomeCompleto=dto.NomeCompleto;
        user.PhoneNumber=dto.PhoneNumber;
        user.CreatedAt=DateTime.UtcNow;

        //Identity salva l'utente e crea l'hash sicuro della password
        IdentityResult result=await _userManager.CreateAsync(user,dto.Password);
        return result;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        ApplicationUser user=await _userManager.FindByEmailAsync(dto.Email);

        if(user==null)
        {
            return null;
        }

        //controlliamo se la password è giusta
        //await fa restare in attesa il thread finché l'operazione non è completata, ma senza bloccarlo
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password,false);

        if(!result.Succeeded)
        {
            return null;
        }

        //se tutto va bene generiamo il token
        string token=_jwtHelper.GenerateToken(user);
        AuthResponseDto response=new AuthResponseDto();
        response.Token=token;
        response.UserId=user.Id;
        response.Email=user.Email ?? "";
        response.NomeCompleto=user.NomeCompleto;

        return response;
    }
}