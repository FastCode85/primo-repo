
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Rubrica.Api.Models;

namespace Rubrica.Api.Data;

public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
{
    //Questo DbContext usa Identity solo per gli utenti
    //e in più aggiunge la tabella Interests
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Interest> Interests {get;set;}

    //configura le relazioni tra tabelle
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //prima lasciamo a Identity configurare le sue tabelle standard
        base.OnModelCreating(builder);

        //Configura il collegamento tra utente e interessi
        builder.Entity<Interest>()
            .HasOne(i=>i.User)
            .WithMany(u=>u.Interests)
            .HasForeignKey(i=>i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}