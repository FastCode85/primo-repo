using System.Security.Authentication;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string path=@"contatti.json";
string lastIdPath=@"lastId.json";
string risposta;

while(true)
{
    Console.WriteLine("Premi 1 per leggere i contatti\nPremi 2 per aggiungere un contatto\nPremi 3 per eliminare un contatto");
    risposta=Console.ReadLine();
    if(risposta=="1")
    {
        List<Contatto> contatti=LeggiFileContatti(path);
        StampaContatti(contatti);
    }
    else if(risposta=="2")
    {
        int nuovoId=LeggiId(lastIdPath)+1;
        Contatto contatto=LeggiNuovoContatto(nuovoId);

        List<Contatto> contatti=LeggiFileContatti(path);
        contatti.Add(contatto);
        ScriviFileContatti(path,contatti);

        AggiornaId(lastIdPath,nuovoId);
        StampaContatti(contatti);
    }
    else if(risposta=="3")
    {
        List<Contatto> contatti=LeggiFileContatti(path);
        StampaContatti(contatti);
        Console.WriteLine("Inserisci l'id del contatto da eliminare");
        int idDaEliminare=int.Parse(Console.ReadLine());
        EliminaContattoById(path,idDaEliminare);
    }
    else
        break;



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
            indexToRemove=i;
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
    string jsonText=File.ReadAllText(filePath);
    Identificatore identificatore=JsonConvert.DeserializeObject<Identificatore>(jsonText);
    return identificatore.Id;
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
