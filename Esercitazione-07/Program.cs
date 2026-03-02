using System.Security.Authentication;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string contattiPath=@"contatti.json";
string lastIdPath=@"lastId.json";
string risposta;

while(true)
{
    Console.WriteLine("Premi 1 per leggere i contatti\nPremi 2 per aggiungere un contatto\nPremi 3 per modificare un contatto\nPremi 4 per eliminare un contatto");
    risposta=Console.ReadLine();
    if(risposta=="1")
    {
        List<Contatto> contatti=LeggiFileContatti(contattiPath);
        StampaContatti(contatti);
    }
    else if(risposta=="2")
    {
        int nuovoId=LeggiId(lastIdPath)+1;
        if(nuovoId>0)
        {
            Contatto contatto=LeggiNuovoContatto(nuovoId);

            List<Contatto> contatti=LeggiFileContatti(contattiPath);
            contatti.Add(contatto);
            ScriviFileContatti(contattiPath,contatti);

            AggiornaId(lastIdPath,nuovoId);
            StampaContatti(contatti);
        }
    }
    else if(risposta=="3")
    {
        ModificaContatto(contattiPath);
    }
    else if(risposta=="4")
    {
        List<Contatto> contatti=LeggiFileContatti(contattiPath);
        StampaContatti(contatti);
        Console.WriteLine("Inserisci l'id del contatto da eliminare");
        int idDaEliminare=int.Parse(Console.ReadLine());
        EliminaContattoById(contattiPath,idDaEliminare);
    }
    else
        break;


}

void ModificaContatto(string filePath)
{
    while(true)
    {
        bool contattoModificato=false;
        List<Contatto> contatti=LeggiFileContatti(contattiPath);
        StampaContatti(contatti);
        Console.WriteLine("Inserisci l' ID del contatto da modificare");
        int idContattoDaModificare=int.Parse(Console.ReadLine());
        Contatto contatto=trovaContattoById(idContattoDaModificare,contatti);
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
            ScriviFileContatti(filePath,contatti);
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

Contatto LeggiNuovoContatto(int id)
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

    Contatto contatto=new Contatto();
    contatto.Id=id;
    contatto.Nome=nome;
    contatto.Cognome=cognome;
    contatto.Email=mail;
    contatto.Telefono=telefono;
    contatto.Presente=presenza;
    contatto.Interessi=interessi.ToList();

    return contatto;
}

List<Contatto> LeggiFileContatti(string filePath)
{
    if(File.Exists(filePath))
    {
        string jsonText=File.ReadAllText(filePath);
        List<Contatto> contatti=JsonConvert.DeserializeObject<List<Contatto>>(jsonText);
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
    string serialized=JsonConvert.SerializeObject(contatti,Formatting.Indented);
    File.WriteAllText(filePath,serialized);
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
        string jsonText=File.ReadAllText(filePath);
        Identificatore identificatore=JsonConvert.DeserializeObject<Identificatore>(jsonText);
        return identificatore.Id;
    }
    else
    {
        Console.WriteLine($"Impossibile trovare il file {filePath}");
        return -1;
    }
}

int AggiornaId(string filePath, int nuovoId)
{
    Identificatore identificatore=new Identificatore();
    identificatore.Id=nuovoId;
    string serialized=JsonConvert.SerializeObject(identificatore,Formatting.Indented);
    File.WriteAllText(filePath,serialized);
    return identificatore.Id;
}

public class Identificatore
{
    public int Id;
}

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
