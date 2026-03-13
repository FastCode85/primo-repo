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