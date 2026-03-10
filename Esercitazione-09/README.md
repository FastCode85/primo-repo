# Web Api
L'archetipo WebAPI è quello che espone gli endpoint per consentire a client frontend come Angular di interagire con i dati prodotti dal backend.

Il comando per creare un'applicazione web Api è

```dash
dotnet new webapi -n Rubrica.Api
```

##Struttura tipica di una Web Api
Rubrica.Api
Controllers
    WeatherForecastController.cs
Program.cs
Startup

Rubrica.Api
    Controllers
    Models
    Services
    Repositories
    Data
    Dtos
    Migration
    Middleware
    Helpers
    Properties
        launchSettings.json
    Program.cs
    appsettings.json


## Cartelle principali
Controller: contiene i controller che gestiscono le richieste HTTP e restituiscono risposte
Models: contiene le classi che rappresentano i dati e le entità di dominio
Services: contiene la logica di business e i servizi che interagiscono con i dati, cioè le operazioni CRUD e altre logiche complesse
Repositories: contiene le classi che gestiscono l'acesso ai dati, ad esempio interagendo con Entity Framework o altri ORM.
Data: contiene il contesto del database e le classi di accesso ai dati
Dtos: contiene le classi Data Transfer Object, che sono specifici per il trasferimento dei dati tra client e server, spesso usati per evitare di esporre direttamente le entità del dominio
Migrations: contiene le migrazioni di Entity Framework per gestire le modifiche al database quando viene modificato un modello
Middleware: contiene componenti middleware personalizzati per gestire le richieste HTTP, ad esempio per la gestione degli errori o l'autenticazione
Helper: contiene classi di utilità e helper per operazioni comuni, come la gestione dei file, la validazione personalizzata, ecc.
Properties: contiene file di configurazione specifici del progetto, come launchSettings.json: definisce le configurazioni di avvio per l'applicazione
Program.cs: il punto d'ingresso dell' applicazione, dove viene configurato il pipeline di esecuzione e i servizi
appsettings.json: il file di configurazione principale dell'applicazone, dove vengono definiti i parametri come stringhe di connessione al database, chiave API, e altre impostazioni

## Controllers
I controller sono classi che ereditano da ControllerBase e sono decorati con l'attributo [ApiController]. Ogni metodo all'interno di un controller rappresenta un endpoint HTTP e viene decorato con attributi come
[HttpGet]
[HttpPost]
[HttpPut]
[HttpDelete]

per indicare il tipo di richiesta che gestisce.
Controller base:

```C#
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok();
    }
}
```

Il controller riceve richieste tipo:
```bash
GET /api/users
```

Di solito le richieste vengono inoltrate attraverso comandi CURL o client HTTP come Postman, oppure da un frontend Angular che consuma l'API.

I modelli rappresentano le entità del dominio e sono mappati a tabelle del database.
Ad esempio, un modello Contatto potrebbe essere

```C#
public class Contatto
{
    public int Id {get;set;}
    public string Nome {get;set;}
    public string Cognome {get;set;}
    public string Email {get;set;}
    public string Telefono {get;set;}
    public bool Presente {get;set;}
    public List<string> Interessi {get;set;}

}
```
Quando usiamo Entity Framework Core, diventano tabelle.

## DTOs ( Data Transfer Objects )
Servono per non esporre direttamente i models

```C#
public class ContattoDto
{
    public int Id {get;set;}
    public string Nome {get;set;}
    public string Cognome {get;set;}

}
```

Sono utili per sicurezza e controllo dei dati.

## Services
Qui mettiamo la logica di business, tipo le operazioni CRUD e altre logiche complesse.
Ad esempio, un ContattoService potrebbe avere metodi come:

```C#
public class ContattoService
{
    public List<Contatto> GetAll()
    {

    }

    public Contatto GetById(int id)
    {

    }

}
```

Il services viene poi iniettato nei controller per essere usato negli endpoint.

## Repositories
Accesso ai dati / database
Ad esempio, un ContattoRepository potrebbe usare Entity Framework per interagire con il database

```C#
public class ContattoRepository
{
    private readonly ApplicationDbContext _context;

    public ContattoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Contatto> GetAll()
    {
        return _context.Contatto.ToList();
    }
}
```

separa il database dalla logica.

## Data
Contiene il DbContext.
Ad esempio, ApplicationDbContext potrebbe essere:

```C#
public class AppDbContext : DbContext
{
    public DbSet<User> Users {get;set;}
}
```

Il DbContext è la classe principale di Entity Framework che gestisce la connessione al database e le operazioni CRUD che vengono eseguite sulle entità dal services dell'applicazione.

## Migrations
Le migrations vengono generate automaticamente da Entity Framework con un comando

```Bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

gestiscono modifiche schema database.

## Middleware
Per intercettare richieste globali:
- logging
- auth
- error handling

Ad esempio, un middleware per gestire eccezioni globali

```C#

public class ExceptionMiddleware
{

}

```

##Helpers

Tutte le funzionalità globali.
Funzioni utility.
Esempio:
- JWT generator
- Date formatter
- Hashing password

Nello specifico JWT sarà quello che si usa per autenticare i client Angular.

## Esempio pratico

Contatto
Richiesta:
POST /api/contatto/5/

Flusso:
- Controller riceve la richiesta
- Controller chiama ContattoService
- ContattoService chiama ContattoRepository
- ContattoRepository legge il db e restituisce i dati
- I dati vengono ritornati al Model e poi al Controller
- Controller restituisce risposta HTTP al client Angular passando attraverso un DTO
- Response in JSON a Angular

## Program.cs
Qui si configura il pipeline di esecuzione e i servizi
Ad esempio, per configurare Entity Framework e i servizi

```C#
var builder = new WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
var app=builder.Build();
app.MapControllers();
app.Run();

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
