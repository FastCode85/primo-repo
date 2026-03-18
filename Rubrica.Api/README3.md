## Services/InterestServices
InterestService gestisce la logica di business per le operazioni CRUD sugli interessi degli utenti. Utilizza ApplicationDbContext per interagire col database e implementa metodi asincroni per ottenere, creare, aggiornare e cancellare interessi, assicurandosi che ogni operazione sia autorizzata solo per l'utente a cui appartiene l'interesse

```C#

using Rubrica.Api.Data;
using Rubrica.Api.Dtos;
using Rubrica.Aèpi.Models;

namespace Rubrica.Api.Services;

public class InterestService
{
    private readonly ApplicationDbContext _context;

    public InterestService(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task<List<InterestDto>> GetAllByUserIdAsync(string userId)
    {
        List<InterestDto> result=new List<InterestDto>();

        //prendiamo tutti gli interessi dal database
        List<Interest> allInterests=_context.Interests.ToList();

        //filtriamo a mano solo quelli dell'utente loggato
        for(int i=0;i<allInterests.Count;i++)
        {
            Interest currentInterest=allInterests[i];

            if(currentInterest.UserId==userId)
            {
                InterestDto dto=new InterestDto();
                dto.Id=currentInterest.Id;
                dto.Nome=currentInterest.Nome;

                result.Add(dto);
            }
        }

        return await Task.FromResult(dto);
    }

    public async Task<InterestDto?> GetByIdAsync(int id, string userId)
    {
        Interest interest = await _context.Interests.FindAsync(id);

        if(interest==null)
            return null;
        //controlliamo che l'interesse appartenga all'utente giusto
        if(interest.UserId !=userId)
            return null;

        InterestDto dto=new InterestDto();
        dto.Id=Interest.Id;
        dto.Nome=interest.Nome;

        return dto;
    }

    public async Task<InterestDto?> CreateAsync(InterestCreateDto dto, string userId)
    {
        //controllo semplice per evitare doppioni
        List<Interest> allInterests=_context.Interests.ToList();

        for(int i=0;i<allInterests.Count;i++)
        {
            Interest currentInterest=allInterests[i];
            if(currentInterest.UserId == userId && currentInterest.Nome==dto.Nome)
            {
                return null;
            }
        }

        Interest interest=new Interest();
        interest.Nome=dto.Nome;
        interest.UserId=userId;

        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        InterestDto result=new InterestDto();
        result.Id=interest.Id;
        result.Nome=interest.Nome;

        return result;
    }

    public async Task<InterestDto?> UpdateAsync(int id, InterestCreateDto dto, string userId)
    {
        Interest interest = await _context.Interests.FindAsync(id);

        if(interest==null)
        {
            return null;
        }

        if(interest.UserId!=userId)
        {
            return null;
        }

        interest.Nome=dto.Nome;

        await _context.SaveChangesAsync();

        InterestDto result=new InterestDto();
        result.Id=interest.Id;
        result.Nome=interest.Nome;

        return result;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        Interest interest=await _context.Interests.FindAsync(id);

        if(interest==null)
        {
            return false;
        }

        if(interest.UserId != userId)
        {
            return false;
        }

        _context.Interests.Remove(interest);
        await _context.SaveChangesAsync();

        return true;

    }

}
```

## Controllers/AuthController.cs
In questa applicazione i controller gestiscono le richieste HTTP e restituiscono risposte. AuthController si occupa di gestire le operazioni di registrazione e login degli utenti, utilizzando AuthService per eseguire la logica di business e restituendo i risultati al client Angular.

```C#
using Microsoft.AspNetCore.Mvc;
using Rubrica.Api.Dtos;
using Rubrica.Api.Services;

namespace Rubrica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

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
}
```

## Controllers/InterestsController.cs
InterestsController gestisce le operazioni CRUD sugli interessi degli utenti. Utilizza InterestService per eseguire la logica di business e restituisce i risultati al client Angular. Tutti gli endpoint sono protetti con l'attributo [Authorize], quindi è necessario essere autenticati con un token JWT valido per accedervi.

```C#
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rubrica.Api.Dtos;
using Rubrica.Api.Services;

namespace Rubrica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InterestsController : ControllerBase
{
    private readonly InterestService _interestService;

    public InterestsController(InterestService interestService)
    {
        _interestService = interestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        string userId = GetUserIdFromToken();

        List<InterestDto> interests = await _interestService.GetAllByUserIdAsync(userId);

        return Ok(interests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        string userId = GetUserIdFromToken();

        InterestDto? interest = await _interestService.GetByIdAsync(id, userId);

        if (interest == null)
        {
            return NotFound(new { message = "Interesse non trovato." });
        }

        return Ok(interest);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InterestCreateDto dto)
    {
        string userId = GetUserIdFromToken();

        InterestDto? result = await _interestService.CreateAsync(dto, userId);

        if (result == null)
        {
            return BadRequest(new { message = "Interesse già presente oppure non valido." });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InterestCreateDto dto)
    {
        string userId = GetUserIdFromToken();

        InterestDto? result = await _interestService.UpdateAsync(id, dto, userId);

        if (result == null)
        {
            return NotFound(new { message = "Interesse non trovato." });
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        string userId = GetUserIdFromToken();

        bool deleted = await _interestService.DeleteAsync(id, userId);

        if (!deleted)
        {
            return NotFound(new { message = "Interesse non trovato." });
        }

        return NoContent();
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
```

## Seed/DataSeeder.cs
DataSeeder è una classe statica che si occupa di popolare il database con dati iniziali per facilitare i test e lo sviluppo. Il metodo SeedAsync crea alcuni utenti demo e interessi associati a quegli utenti, ma prima controlla se esistono già per evitare duplicazioni. Viene chiamato all'avvio dell'applicazione dopo aver applicato le migrazioni al database.

```C#

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rubrica.Api.Data;
using Rubrica.Api.Models;

namespace Rubrica.Api.Seed;

public static class DataSeeder
{
    // Questo metodo crea utenti e interessi iniziali.
    // se i dati esistono già, non li duplica.
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Creiamo il database se non esiste ancora
        await context.Database.EnsureCreatedAsync();

        // Creiamo alcuni utenti demo
        ApplicationUser utente1 = await CreateUserIfNotExistsAsync(
            userManager,
            "utente1@email.com",
            "123456",
            "Utente uno",
            "3331234567");

        ApplicationUser utente2 = await CreateUserIfNotExistsAsync(
            userManager,
            "utente2@email.com",
            "123456",
            "Utente due",
            "3337654321");

        ApplicationUser utente3 = await CreateUserIfNotExistsAsync(
            userManager,
            "utente3@email.com",
            "123456",
            "Utente tre",
            "3331112222");

        // Creiamo alcuni interessi per ogni utente
        await CreateInterestIfNotExistsAsync(context, utente1.Id, "Calcio");
        await CreateInterestIfNotExistsAsync(context, utente1.Id, "CSharp");
        await CreateInterestIfNotExistsAsync(context, utente1.Id, "Cinema");

        await CreateInterestIfNotExistsAsync(context, utente2.Id, "Nuoto");
        await CreateInterestIfNotExistsAsync(context, utente2.Id, "Angular");
        await CreateInterestIfNotExistsAsync(context, utente2.Id, "Musica");

        await CreateInterestIfNotExistsAsync(context, utente3.Id, "Lettura");
        await CreateInterestIfNotExistsAsync(context, utente3.Id, "Viaggi");
        await CreateInterestIfNotExistsAsync(context, utente3.Id, "Fotografia");
    }

    private static async Task<ApplicationUser> CreateUserIfNotExistsAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string nomeCompleto,
        string? phoneNumber)
    {
        // Controlliamo se l'utente esiste già tramite email
        ApplicationUser? existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return existingUser;
        }

        ApplicationUser user = new ApplicationUser();
        user.UserName = email;
        user.Email = email;
        user.NomeCompleto = nomeCompleto;
        user.PhoneNumber = phoneNumber;
        user.CreatedAt = DateTime.UtcNow;

        IdentityResult result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();

            foreach (IdentityError error in result.Errors)
            {
                errors.Add(error.Description);
            }

            string message = string.Join(" | ", errors);
            throw new Exception($"Errore durante la creazione dell'utente {email}: {message}");
        }

        return user;
    }

    private static async Task CreateInterestIfNotExistsAsync(
        ApplicationDbContext context,
        string userId,
        string nome)
    {
        // Leggiamo tutti gli interessi e controlliamo a mano
        // se questo interesse esiste già per quell'utente.
        List<Interest> interests = await context.Interests.ToListAsync();

        for (int i = 0; i < interests.Count; i++)
        {
            Interest currentInterest = interests[i];

            bool sameUser = currentInterest.UserId == userId;
            bool sameName = string.Equals(currentInterest.Nome, nome, StringComparison.OrdinalIgnoreCase);

            if (sameUser && sameName)
            {
                return;
            }
        }

        Interest interest = new Interest();
        interest.UserId = userId;
        interest.Nome = nome;

        context.Interests.Add(interest);
        await context.SaveChangesAsync();
    }
}

```

## COMANDI CURL

Richiede un nuovo token JWT e lo salva nella variabile $TOKEN del terminale
```Bash
TOKEN=$(curl -s -X POST "http://localhost:5032/api/Auth/login" \
-H "Content-Type: application/json" \
-d '{"email":"utente1@email.com","password":"123456"}' | jq -r '.token')
```

-H è un header indica che stiamo inviando i dati in formato Json
-d contiene i dati, in questo caso l'email e la password dell'utente che vogliamo loggare


LETTURA INTERESSI
```Bash
curl -X GET "http://localhost:5032/api/Interests" \
-H "Authorization: Bearer $TOKEN"
```


CREARE INTERESSE
```Bash
curl -X POST "http://localhost:5032/api/Interests" \
-H "Content-Type: application/json" \
-H "Authorization: Bearer $TOKEN" \
-d '{"nome":"Pallavolo"}'
```

MODIFICA INTERESSE
```Bash
curl -X PUT "http://localhost:5032/api/Interests/1" \
-H "Content-Type: application/json" \
-H "Authorization: Bearer $TOKEN" \
-d '{"nome":"Nuovo"}'
```

CANCELLA INTERESSE
```Bash
curl -X DELETE "http://localhost:5032/api/Interests/1" \
-H "Authorization: Bearer $TOKEN" 
```

CREA UTENTE
```Bash
curl -s -X POST "http://localhost:5032/api/Auth/register" \
-H "Content-Type: application/json" \
-d '{"email":"marco@email.com","password":"123456","nomecompleto":"marco","phonenumber":"111111"}'
```

MODIFCA UTENTE
```Bash
curl -X PUT "http://localhost:5032/api/Auth/update" \
-H "Content-Type: application/json" \
-H "Authorization: Bearer $TOKEN" \
-d '{"nomecompleto":"marco","phonenumber":"111111"}'
```

LETTURA UTENTE
```Bash
curl -X GET "http://localhost:5032/api/Auth/profile" \
-H "Authorization: Bearer $TOKEN" 
```

CANCELLAZIONE UTENTE
```Bash
curl -X DELETE "http://localhost:5032/api/Auth/delete" \
-H "Authorization: Bearer $TOKEN" 
```

