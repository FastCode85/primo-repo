using System.ComponentModel.Design.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

var lastIdController=new LastIdController();
int nextId=lastIdController.GetNextId();
Console.WriteLine($"Il prossimo ID è {nextId}");

Contatto contatto1=new Contatto
{
    Id=1,
    Nome="nome",
    Cognome="nome",
    Email="nome",
    Telefono="nome",
    Presente=true,
    Interessi=new List<string> {"interesse1","interesse2"}
};

Contatto contatto2=new Contatto
{
    Id=1,
    Nome="nome",
    Cognome="nome",
    Email="nome",
    Telefono="nome",
    Presente=true,
    Interessi=new List<string> {"interesse1","interesse2"}
};

Console.WriteLine($"a==B: {contatto1==contatto2} a equals b: {contatto1.Equals(contatto2)} referenceEquals {Object.ReferenceEquals(contatto1,contatto2)}");

public class LastIdController
{
    private readonly string path="lastId.json";
    private Identificatore lastIdObj;

    public LastIdController()
    {
        
        if(!File.Exists(path))
        {
            lastIdObj=new Identificatore { Id=0 };
            Salva();
        }
        else
        {
            string json=File.ReadAllText(path);
            lastIdObj=JsonConvert.DeserializeObject<Identificatore>(json) ?? new Identificatore { Id=0 };
        }
    }

    public int GetNextId()
    {
        lastIdObj.Id++;
        Salva();
        return lastIdObj.Id;
    }

    private void Salva()
    {
        string json=JsonConvert.SerializeObject(lastIdObj,Formatting.Indented);
        File.WriteAllText(path,json);
    } 
}

public class ContattiController
{
    private readonly string path="contatti.json";
    private List<Contatto> contatti;
    private LastIdController lastIdController;

    public ContattiController()
    {
        lastIdController=new LastIdController();
        if(File.Exists(path))
        {
            contatti=new List<Contatto>();
            Salva();
        }
        else
        {
            string json=File.ReadAllText(path);
            contatti=JsonConvert.DeserializeObject<List<Contatto>>(json) ?? new List<Contatto>();
        }
    }

    public List<Contatto> GetContatti()
    {
        return contatti;
    }

    private void Salva()
    {
        string json=JsonConvert.SerializeObject(contatti,Formatting.Indented);
        File.WriteAllText(path,json);
    }

    public void AggiungiContatto(string nome, string cognome, string email, string telefono, bool presente, List<string> interessi)
    {
        Contatto nuovoContatto=new Contatto
        {
            Id=lastIdController.GetNextId(),
            Nome=nome,
            Cognome=cognome,
            Email=email,
            Telefono=telefono,
            Presente=presente,
            Interessi=interessi
        };
        contatti.Add(nuovoContatto);
        Salva();
    }

    public void ModificaContatto(int id, string nome, string cognome, string email, string telefono, bool presente, List<string> interessi)
    {
        Contatto contattoEsistente=null;
        foreach(Contatto contatto in contatti)
        {
            if(contatto.Id==id)
            {
                contattoEsistente=contatto;
                break;
            }
        }
        if(contattoEsistente!=null)
        {
            contattoEsistente.Nome=nome;
            contattoEsistente.Cognome=cognome;
            contattoEsistente.Email=email;
            contattoEsistente.Telefono=telefono;
            contattoEsistente.Presente=presente;
            contattoEsistente.Interessi=interessi;
            Salva();
        }
    }

    public void EliminaContatto(int id)
    {
        Contatto contattoEsistente=null;
        foreach(Contatto contatto in contatti)
        {
            if(contatto.Id==id)
            {
                contattoEsistente=contatto;
                break;
            }
            if(contattoEsistente!=null)
            {
                contatti.Remove(contattoEsistente);
                Salva();
            }
        }
    }

    public Contatto VisualizzaContatto(int id)
    {
        Contatto contattoEsistente=null;
        foreach(Contatto contatto in contatti)
        {
            if(contatto.Id==id)
            {
                contattoEsistente=contatto;
                break;
            }

        }
        if(contattoEsistente==null)
        {
            throw new Exception($"Contatto con id {id} non trovato");
        }
        return contattoEsistente;
    }
}

public class Identificatore
{
    [Range(0, int.MaxValue, ErrorMessage = "L'ID deve essere un numero intero positivo.")]
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
