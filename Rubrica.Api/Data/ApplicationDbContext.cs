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