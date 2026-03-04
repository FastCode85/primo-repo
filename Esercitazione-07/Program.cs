using System.Security.Authentication;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;


string contattiPath=@"contatti.json";
string risposta;

ContattiController contattiController=new ContattiController();





while(true)
{
    Console.WriteLine("Premi 1 per leggere i contatti\nPremi 2 per aggiungere un contatto\nPremi 3 per modificare un contatto\nPremi 4 per eliminare un contatto");
    risposta=Console.ReadLine();
    if(risposta=="1")
    {
        
        StampaContatti(contattiController.GetContatti());
    }
    else if(risposta=="2")
    {
        AggiungiContatto(contattiController);
        StampaContatti(contattiController.GetContatti());
    }
    else if(risposta=="3")
    {
        ModificaContatto(contattiController);
    }
    else if(risposta=="4")
    {
        StampaContatti(contattiController.GetContatti());
        Console.WriteLine("Inserisci l'id del contatto da eliminare");
        int idDaEliminare=int.Parse(Console.ReadLine());
        contattiController.EliminaContatto(idDaEliminare);
    }
    else
        break;


}

void ModificaContatto(ContattiController contattiController)
{
    while(true)
    {
        bool contattoModificato=false;
        StampaContatti(contattiController.GetContatti());
        Console.WriteLine("Inserisci l' ID del contatto da modificare");
        int idContattoDaModificare=int.Parse(Console.ReadLine());
        Contatto contatto=contattiController.GetContattoById(idContattoDaModificare);
        if(contatto!=null) //se è stato trovato un contatto per l' id idContattoDaModificare
        {
            Console.WriteLine("Premi 1 per modificare il nome\nPremi 2 per modificare il cognome\nPremi 3 per modificare la mail\nPremi 4 per modificare il telefono\nPremi 5 per modificare la presenza\nPremi 6 per modificare gli interessi\nPremi 0 per uscire");
            string risposta=Console.ReadLine();
            
            if(risposta=="1")
            {
                Console.WriteLine("Inserisci il nome");
                string input=Console.ReadLine();
                contatto.Nome=input;
                contattoModificato=true;
            }
            else if(risposta=="2")
            {
                Console.WriteLine("Inserisci il cognome");
                string input=Console.ReadLine();
                contatto.Cognome=input;
                contattoModificato=true;
            }
            else if(risposta=="3")
            {
                Console.WriteLine("Inserisci la mail");
                string input=Console.ReadLine();
                contatto.Email=input;
                contattoModificato=true;
            }
            else if(risposta=="4")
            {
                Console.WriteLine("Inserisci il telefono");
                string input=Console.ReadLine();
                contatto.Telefono=input;
                contattoModificato=true;
            }
            else if(risposta=="5")
            {
                Console.WriteLine("Inserisci la presenza (true/false)");
                string input=Console.ReadLine();
                bool presenza=bool.Parse(input);
                contatto.Presente=presenza;
                contattoModificato=true;
            }
            else if(risposta=="6")
            {
                Console.WriteLine("Inserisci gli interessi separati da virgola");
                string input=Console.ReadLine();
                contatto.Interessi=input.Split(",").ToList();
                contattoModificato=true;
            }
            else if(risposta=="0")
            {
                break;
            }
            else
                Console.WriteLine("Risposta non valida, riprova");
        }
        else
            Console.WriteLine($"Impossibile trovare il contatto con id {idContattoDaModificare}");
        if(contattoModificato)
        {
            contattiController.ModificaContatto(contatto.Id,contatto.Nome,contatto.Cognome,contatto.Telefono,contatto.Presente,contatto.Interessi);
            break;
        }
    }
}

Contatto trovaContattoById(int id, List<Contatto> contatti)
{
    foreach(Contatto contatto in contatti)
        if(contatto.Id==id)
            return contatto;
    return null;
}

Contatto AggiungiContatto(ContattiController contattiController)
{
    Console.WriteLine("Inserisci il nome ");
    string nome=Console.ReadLine();
    Console.WriteLine("Inserisci il cognome ");
    string cognome=Console.ReadLine();
    Console.WriteLine("Inserisci la mail ");
    string mail=Console.ReadLine();
    Console.WriteLine("Inserisci il numero di telefono");
    string telefono=Console.ReadLine();
    Console.WriteLine("Inserisci la presenza (true/false)");
    bool presenza=bool.Parse(Console.ReadLine());
    Console.WriteLine("Inserisci gli interessi separati da virgola");
    string[] interessi=Console.ReadLine().Split(",");

    contattiController.AggiungiContatto(nome,cognome,telefono,presenza,interessi.ToList());

    return null;
}

List<Contatto> LeggiFileContatti(string filePath)
{
    if(File.Exists(filePath))
    {
        
        List<Contatto> contatti=JsonHelper.Leggi<List<Contatto>>(filePath);
        //string jsonText=File.ReadAllText(filePath);
        //List<Contatto> contatti=JsonConvert.DeserializeObject<List<Contatto>>(jsonText);
        return contatti;
    }
    else
        Console.WriteLine($"Errore in LeggiFileContatti, il file {filePath} non esiste");
    return new List<Contatto>();
}

void StampaContatti(List<Contatto> contatti)
{
    Console.WriteLine($"Stampa dei contatti, contatti trovati: {contatti.Count}");
    foreach(Contatto contatto in contatti)
        StampaContatto(contatto);
}

void StampaContatto(Contatto contatto)
{
    Console.WriteLine($"{contatto.Id}\t{contatto.Nome}\t{contatto.Cognome}\t{contatto.Email}\t{contatto.Telefono}\t{contatto.Presente}\t{string.Join(",",contatto.Interessi)}");
}

void ScriviFileContatti(string filePath,List<Contatto> contatti)
{
    //string serialized=JsonConvert.SerializeObject(contatti,Formatting.Indented);
    //File.WriteAllText(filePath,serialized);
    JsonHelper.Salva(filePath,contatti);
}

void EliminaContattoById(string filePath, int id)
{

    List<Contatto> contatti=LeggiFileContatti(filePath);
    int indexToRemove=-1;
    for(int i=0;i<contatti.Count;i++)
    {
        Contatto contatto=contatti.ElementAt(i);
        if(contatto.Id==id)
        {
            indexToRemove=i;
            break;
        }
    }
    if(indexToRemove<0)
    {
        Console.WriteLine($"Impossibile trovare un contatto con id={id}");
    }
    else
    {
        contatti.RemoveAt(indexToRemove);
        ScriviFileContatti(filePath,contatti);
        Console.WriteLine($"Elemento con id={id} rimosso dalla lista contatti");
    }
}

int LeggiId(string filePath)
{
    if(File.Exists(filePath))
    {
        //string jsonText=File.ReadAllText(filePath);
        //LastId LastId=JsonConvert.DeserializeObject<LastId>(jsonText);
        //LastId.Validate();
        LastId lastId=JsonHelper.Leggi<LastId>(filePath);
        return lastId.Id;
    }
    else
    {
        Console.WriteLine($"Impossibile trovare il file {filePath}");
        return -1;
    }
}

int AggiornaId(string filePath, int nuovoId)
{
    LastId lastId=new LastId();
    lastId.Id=nuovoId;
    //string serialized=JsonConvert.SerializeObject(LastId,Formatting.Indented);
    //File.WriteAllText(filePath,serialized);
    JsonHelper.Salva(filePath,lastId);
    return lastId.Id;
}

void ValidaLastId(LastId lastId)
{
    ValidationContext context = new ValidationContext(lastId);
    try
    {
        // validate object restituisce un'eccezione se l'oggetto non è valido, altrimenti non restituisce nulla
        Validator.ValidateObject(lastId, context, true);
        Console.WriteLine($"Chiamato ValidaLastId(), valore di ID: {lastId.Id}");
    }
    catch (ValidationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}




