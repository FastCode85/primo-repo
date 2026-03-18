using Microsoft.AspNetCore.Mvc;
using Rubrica.Api.Dtos;
using Rubrica.Api.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Rubrica.Api.Controllers;

/*

Questo controller gestisce le richieste di autenticazione
come la registrazione ed il login
*/
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    //servizio privato degll'autenticazione per interazione con il DB
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        //dependency injection: le dipendenze vengono fornite in modo automatico
        _authService = authService;
    }

    //mappa l'url register a questo metodo
    [HttpPost("register")]  
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();

            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }

        return Ok(new { message = "Registrazione completata." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        AuthResponseDto? response = await _authService.LoginAsync(dto);

        if (response == null)
        {
            return Unauthorized(new { message = "Email o password non validi." });
        }

        return Ok(response);
    }


    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        string userId = GetUserIdFromToken();
        AuthResponseDto authResponse = await _authService.UpdateAsync(dto, userId);
        if(authResponse==null)
        {
            return NotFound(new { message = "Utente non trovato." });
        }

        return Ok(authResponse);

    }

    [HttpGet("profile")]
    public async Task<IActionResult> Get()
    {
        string userId = GetUserIdFromToken();
        GetUserDto getUserDto = await _authService.GetByIdAsync(userId);
        if(getUserDto==null)
        {
            return NotFound(new { message = "Utente non trovato." });
        }

        return Ok(getUserDto);
        
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        
        string userId = GetUserIdFromToken();
        IdentityResult result = await _authService.DeleteByIdAsync(userId);
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            return BadRequest(errors);
        }

        return Ok(new { message = "Utente cancellato." });
        
    }

    private string GetUserIdFromToken()
    {
        // Leggiamo l'id utente che abbiamo salvato nel JWT
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("UserId non trovato nel token.");
        }

        return userId;
    }
}