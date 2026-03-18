

using Microsoft.EntityFrameworkCore;
using Rubrica.Api.Data;
using Rubrica.Api.Dtos;
using Rubrica.Api.Models;

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
        //List<Interest> allInterests=_context.Interests.ToList();
        List<Interest> allInterests=_context.Interests.ToListAsync().Result;

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

        return await Task.FromResult(result);
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
        dto.Id=interest.Id;
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
            Console.WriteLine("Interesse non trovato");
            return null;
        }

        if(interest.UserId!=userId)
        {
            Console.WriteLine("interest.UserId!=userId");
            return null;
        }

        if(interest.Nome==dto.Nome)
        {
            Console.WriteLine($"interest.Nome==dto.Nome {interest.Nome}|{dto.Nome}");
            return null;
        }

        interest.Nome=dto.Nome;
        
        /*
            Da guardare, da provare. Effettua l'update dell' oggetto sul DBSET degli interessi nel dbcontext
            _context.Interests.Update(interest);


        */
        //Aggiorniamo l'interesse se tutte le verifiche passano.
        //_context.Interests.Update(interest);
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