/*
    Gestisce
*/

using Microsoft.AspNetCore.Identity;
using Rubrica.Api.Dtos;
using Rubrica.Api.Helpers;
using Rubrica.Api.Models;

namespace Rubrica.Api.Services;

public class AuthService
{
    //è una classe fornita da ASP.NET Idetity che serve per gestire gli utenti
    private readonly UserManager<ApplicationUser> _userManager;
    //è una classe che si occupa della gestione dell'autenticazione, fornita da ASP.NET Identity
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;

    public AuthService(
        UserManager<ApplicationUser> userManager,//dependency injection
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
        //conferma anche l'operazione sul database
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

    public async Task<AuthResponseDto> UpdateAsync(UpdateUserDto dto, string userId)
    {
        //trova l'utente by id
        ApplicationUser user=await _userManager.FindByIdAsync(userId);

        //se utente non trovato esce con errore
        if(user==null)
        {
            //errore
            return null;
        }

        //update dell'utente trovato con i nuovi dati ricevuti dal DTO
        user.NomeCompleto=dto.NomeCompleto;
        user.PhoneNumber=dto.PhoneNumber;

        //effettua l'update dell'utente
        IdentityResult identityResult = await _userManager.UpdateAsync(user);

        //gestisce il caso in cui l'upodate non vada a buon fine
        if(!identityResult.Succeeded)
        {
            //errore
            return null;
        }

        AuthResponseDto authResponseDto=new AuthResponseDto();
        authResponseDto.Email=user.Email;
        authResponseDto.UserId=user.Id;
        authResponseDto.NomeCompleto=user.NomeCompleto;
        
        return authResponseDto;

    }

    public async Task<GetUserDto> GetByIdAsync(string userId)
    {
        ApplicationUser user=await _userManager.FindByIdAsync(userId);
        if(user==null)
        {
            return null;
        }
        GetUserDto getUserDto=new GetUserDto();
        getUserDto.Email=user.Email;
        getUserDto.Id=user.Id;
        getUserDto.NomeCompleto=user.NomeCompleto;
        getUserDto.PhoneNumber=user.PhoneNumber;
        getUserDto.CreatedAt=user.CreatedAt;
        return getUserDto;
    }

    public async Task<IdentityResult>DeleteByIdAsync(string userId)
    {
        ApplicationUser user=await _userManager.FindByIdAsync(userId);
        if(user==null)
        {
            IdentityError error=new IdentityError();
            error.Description="Utente non trovato.";
            
            List<IdentityError> errors=new List<IdentityError>();
            errors.Add(error);

            return IdentityResult.Failed(errors.ToArray());
        }
        IdentityResult result=await _userManager.DeleteAsync(user);
        return result;
    }

}