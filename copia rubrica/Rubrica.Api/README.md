## WEBAPI RUBRICA COMPLETA V1
- ApplicationUser che estende IdentityUser
- Tabella Interest collegata all'utente
- AuthService
- InterestService
- controller semplici con operazioni CRUD

## Struttura

```Bash
Rubrica.Api
    Controllers
        AuthController.cs
        InterestsController.cs
    Data
        ApplicationDbContext.cs
    Dtos
        AuthResponseDto.cs
        InterestCreateDto.cs
        InterestDto.cs
        InterestDto.cs
        LoginDto.cs
        RegisterDto.cs
    Helpers
        JwtHelper.cs
    Models
        ApplicationUser.cs
        Interest.cs
    Services
        AuthService.cs
        InterestService.cs
    Program.cs
    appsettings.json
```

## Model

Models/ApplicationUser.cs
ApplicationUser estende IdentityUser, che è la classe base di Identity per rappresentare un utente. Aggiungiamo alcune proprietà personalizzate come NomeCompleto, CreatedAt e l'elenco di interessi collegati all'utente. Viene mappata alla tabella "users" nel database e ha una relazione uno a molti con Interest
```C#
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComèponentModel.DataAnnotations.Schema;

namespace Rubrica.Api.Models;

[Table("users")]
public class ApplicationUser : IdentityUser
{
    //IdentityUser ha già
    //Id, Username, Email, PasswordHash, PhoneNumber, ecc

    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;} = string.Empty;
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public List<Interest> Interests {get;set;} = new List<Interest>();

}


```

Models/Interest.cs
Interest rappresenta un oggetto dell'utente, con un nome e un collegamento all'utente a cui appartiene. Viene mappato alla tabella "interests" nel database e ha una relazione molti a uno con ApplicationUser

```C#
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrica.Api.Models;

[Table("Interests")]
public class Interest
{
    public int Id {get;set;}

    [Required]
    [StringLength(100)]
    public string Nome {get;set;} = string.Empty;

    //Con Identity l'id è string
    [Required]
    public string UserId {get;set;} = string.Empty;

    //collegamento all'utente
    [ForeignKey("UserId")]
    public ApplicationUser User {get;set;}
}
```

Dtos/RegisterDto.cs
Serve per fornire i dati necessari alla registrazione di un nuovo utente. Viene usato come input per l'endpoint di registrazione nell'AuthController.

```C#
public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = string.Empty;

    [Required]
    [StringLength(100, MinimumLength=6)]
    public string Password {get;set;} = string.Empty;

    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;} = string.Empty;

    public string PhoneNumber {get;set;} = string.Empty;

}
```

Dtos/LoginDto.cs
Serve a fornire i dati necessari al login di un utente esistente. Viene usato come input poer l'endpoint di login nell'AuthController.

```C#
using System.ComponentModel.DataAnnotations;

namespace Rubrica.Api.Dtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = string.Empty;

    [Required]
    public string Password {get;set;} =string.Empty;
}

```

Dtos/AuthResponseDto.cs
Serve per restituire i dati di risposta dopo una registrazione o un login riusciti. Contiene il token JWT generato, l'id dell'utente, l'email e il nome completo. Viene usato come output per gli endpoint di registrazione e login nell'AuthController.

```C#
namespace Rubrica.Api.Dtos;

public class AuthResponseDto
{
    public string Token {get;set;} = string.Empty;
    public string UserId {get;set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string NomeCompleto {get;set;} = string.Empty;
}
```


Dtos/InterestCreateDto.cs

```C#
namespace Rubrica.Api.Dtos;

public class InterestCreateDto
{
    [Required]
    [StringLength(100)]
    public string Nome {get;set;} = string.Empty;
}
```

Dtos/InterestCreateDto.cs

```C#
namespace Rubrica.Api.Dtos;

public class InterestDto
{
    public int Id {get;set;}
    public string Nome {get;set;} = string.Empty;
}
```

Data/ApplicationDbContext.cs

```C#
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rubrica.Api.Models;

namespace Rubrica.Api.Data

public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
{
    //Questo DbContext usa Identity solo per gli utenti
    //e in più aggiunge la tabella Interests
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Interest> Interests {get;set;}
}
```

Helpers/JwtHelper.cs

```C#
using System.IdentityModel.Tokens.Jwt;
using System.SecurityClaims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Models

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
```


Services/AuthService.cs
Auth service gestisce la logica di registrazione e login degli utenti, utilizzando UserManager e SignInManager di Identity per interagire con il database degli utenti e JwtHelper per generare i token JWT.

```C#
using Microsoft.AspNetore.Identity;
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
        _userManager=_userManager;
        _signInManager=signInManager;
        _jwtHelper=jwtHelper;
    }

    /*
    Questo è un metodo asincrono che restituisce un IdentityResult, che indica se la registrazione è riuscita o no, e contiene eventuali errori e un metodo asincrono che è un metodo che può essere eseguito in modo non bloccante, cioè può fare operazioni che richiedono tempo (come accedere al database) senza bloccare il thread principale dell'applicazione
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

```

















## WEBAPI RUBRICA COMPLETA
La web api rubrica userà JWT per autenticare i client Angular, e avrà:

## Model

- un modello Contatto con proprietà come Id, nome, Telefono, lista competenze, stato attivo, data creazione
- un modello User con id, username, passwordHash e Ruolo per gestire l'autenticazione e autorizzazione e il collegamento con i contatti

- data annotations e decorators

## DTO:

- un DTO ContattoDTO con solo alcune proprietà per esporre i dati in modo sicuro, potrebbe esporre solo Id, Nome completo, e telefono
- un DTO UserDTO per esporre solo Username e Ruolo

## Controller:

- un controller ContattoController con endpoint CRUD per gestire i contatti
- un controller UserController per gestire la registrazione e gestione degli utenti
- un controller AuthController per gestire l'autenticazione e la generazione dei token JWT

## Services:

- un servizio ContattoService che contiene la logica di business per i contatti
- un servizio IndirizzoService per la logica degli indirizzi
- un servizio AuthService per la logica di autenticazione e gestione dei token JWT

## Repository:

- un repository ContattoRepository che interagisce con il database usando Entity Framework Core
- un repository UserRepository per gestire gli utenti con le credenziali di autenticazione
- un repository AuthRepository per gestire la logica di autenticazione e validazione delle credenziali

## Data:

- Un DbContext ApplicationDbContext che rappresenta il database e contiene un `DbSet<Contatto>` e un `DbSet<User>`
- Middleware per gestire l'autenticazione JQT e proteggere gli endpoint
- configurazione in Program.cs per registrare i servizi, configurare Entity Framework, e abilitare l'autenticazione JWT

## Middleware:

- un middleware JwtMiddleware per intercettare le richieste e validare i token JWT, assicurando che solo gli utenti autenticati possano accedere agli endpoint protetti
- un middleware di gestione degli errori per catturare eccezioni globali e restituire le resposte HTTP appropriate in caso di errori

## Helpers: 

- un helper JwtHelper per generare e validare i token JWT
- un helper PasswordHelper per gestire l'hashing e la verifica delle password

## Configurazione in Program.cs:

la configurazione del DB con Entity Framework ed i JWT

## Migrazioni:

- migrazioni per creare le tabelle Contatti e Users nel database usando Entity Framework Core

## Creazione progetto e comandi
Creazione archetipo webapi

```bash
dotnet new webapi -o Rubrica.Api
```

Installazione librerie

```bash
// Entity Framework Core e SqLite
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite


// Strumenti per migrazioni
dotnet add package Microsoft.EntityFrameworkCore.Tools

// JWT e autenticazione
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.IdentityModel.Tokens
```

## Configurazione DLL SqLite
Scaricare i driver per Win64 dal sito SqLite e mettere la dll in c:\programmi\sqlite.
Aprire le variabili d'ambiente di windows, cercare la variabile Path e aggiungervi il percorso alla cartella che contiene la dll.

## Creazione del DbContext
Il DbContext è la classe principale di Entoty Framework che gestisce la connessione al database e le operazioni CRUD che vengono eseguite sulle entità dai services dell'applicazione. I Repository poi scriveranno sul DB.

Creazione DbContext:

File ApplicationDbContext.cs in /Data
```C#

public class ApplicationDbContext : DbContext
{
    //Costruttore che accetta le opzioni di configurazione del DbContext
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
    {
        //qui non serve aggiungere niente, il costruttore base si occupa di configurare il DbContext con le opzioni fornite in Program.cs
    }

    //DbSet per la tabella Contatto
    public DbSet<Contatto> Contatti {get;set;}
    //DbSet per la tabella Users
    public DbSet<User> Users {get;set;}
}

```

## Creazione modelli
I modelli rappresentano le entità del dominio e sono mappati a tabelle del database. In questo caso, abbiamo un modello Contatto e un modello User.

Creazione modello Contatto
File Contatto.cs in /Models

```C#
public class Contatto
{
    public int Id {get;set;}
    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;}
    [Required]
    [StringLength(30)]
    public string Telefono {get;set;}
    //lista delle competenze del contatto.
    //in sqlite la salveremo come testo JSON nel DbContext.
    public List<string> Competenze {get;set;} = new()
    public bool IsActive {get;set;}
    public DateTime CreatedAt {get;set;}
    //foreign key: ogni contatto appartiene ad un utente
    public int UserId {get;set;}
    //proprietà di navigazione: EF Core usa questa proprietà
    //per collegare il contatto al suo utente
    public User User {get;set;} =null!;

}
```

Creazione modello User
File User.cs in /Models

```C#
public class User
{
    [Required]
    public int Id {get;set;}
    [StringLength(50)]
    public string Username {get;set;} = string.Empty;
    [Required]
    public string PasswordHash {get;set;} = string.Empty;
    [Required]
    [StringLength(20)]
    public string Ruolo {get;set;} = "User";
    public List<Contatto> Contatti {get;set;}=new();
}
```


## Creazione DTOs
I DTOs ( Data Transfer Objects) servono per non esporre direttamente i Models e per controllare quali dati vengono trasferiti tra client e server

File ContattoDto.cs in /Dtos
```C#
public class ContattoDto
{
    public string NomeCompleto {get;set;}
    public string Telefono {get;set;}
    public List<string> Competenze {get;set;}
}
```

File UserDto.cs in /Dtos
```C#
public class UserDto
{
    public string Username {get;set;}
    public string Password {get;set;}
    public string Ruolo {get;set;}
}
```

- ContattoDto.cs ( contiene solo le proprietà che vogliamo esporre al frontend )
- UserDto.cs ( contiene solo username e ruolo )

IMPORTANTE: gli altri DTO che servono dobbiamo ancora farli e saranno:

- ContattoCreateDto.cs ( le competenze possono essere vuote ma non null )
- ContattoUpdateDto.cs
- RegisterUserDto.cs ( se non passiamo il ruolo diventa User di default )
- LoginDto.cs
- AuthResponseDto.cs ( DTO che ritorniamo dopo il login )



## Configurazione in Program.cs
il program.cs è il punto d'ingresso dell'applicazione, dove viene cponfigurato il pipeline di esecuzione e i servizi. Qui configuriamo Entity Framework, JWT, e registriamo i servizi e repository.

OPZIONALE: possiamo configurare un seed ( megòlio su file separato ) dove viene preso l'admin di default e tre utenti, uno per ogni ruolo

File Program.cs:
```C#
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Data;
using Rubrica.Api.Helpers;
using Rubrica.Api.Middleware;
using Rubrica.Api.Models;
using Rubrica.Api.Repositories;
using Rubrica.Api.Services;
using Rubrica.Api.Dtos;

//creazione del builder dell'applicazione
var builder = WebApplication.CreateBuilder(args);

//aggiunge i controller
builder.Services.AddControllers();

//configura il DbContext con Sqlite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.useSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
);

//configura CORS per permettere ad Angular in locale di chiamare l'API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .allowAnyHeader()
            .allowAnyMethod();
    });
});

//leggiamo la chiave JWT da appsettings
var jwtKey=builder.Configuration["Jwt:Key"]
    ?? throw new Exception("JwtKey mancante in appsettings.json");

//configurazione autenticazione JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //controlla che il token sia stato emesso dall'issuer corretto
            ValidateIssuer=true,
            ValidateAudience=true,
            ValidateLifetime=true,
            ValidateIssuerSigningKey=true,
            ValidIssuer=builder.Configuration["Jwt:Issuer"],
            ValidAudience=builder.Configuration["Jwt:Audience"],
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

//abilita l'autorizzazione con [Authorize]
builder.Services.AddAuthorization();

//Dependency Injection: registriamo repository, services e helper
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ContattoRepository>();



//configurazione autenticazione JQT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    
);

```
