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

## Helpers

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



