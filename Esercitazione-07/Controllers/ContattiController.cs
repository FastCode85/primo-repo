using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
public class ContattiController
{
    // queste sono le variabili di istanza della classe ContattiController
    // cioè le variabili che appartengono a ogni istanza della classe
    // e possono essere utilizzate in tutti i metodi della classe
    private readonly string path = "contatti.json";
    private List<Contatto> contatti;
    private LastIdController lastIdController;

    public ContattiController()
    {
        // creo un'istanza della classe LastIdController per poter utilizzare i metodi di quella classe
        // in particolare GetNextId per generare nuovi ID per i contatti
        lastIdController = new LastIdController();
        if (!File.Exists(path))
        {
            // se il file non esiste, creo una lista vuota di contatti e la salvo su file
            contatti = new List<Contatto>();
            Salva();
        }
        else
        {
            string json = File.ReadAllText(path);
            // deserializzo il file JSON in una lista di contatti, se il file è vuoto o non contiene dati validi, creo una lista vuota
            // con l'operatore di coalescenza nulla che restituisce la lista deserializzata se non è null
            // altrimenti restituisce una nuova lista vuota
            contatti = JsonConvert.DeserializeObject<List<Contatto>>(json) ?? new List<Contatto>();
        }
    }

    public List<Contatto> GetContatti()
    {
        return contatti;
    }

    private void Salva()
    {
        string json = JsonConvert.SerializeObject(contatti, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void AggiungiContatto(string nome, string cognome, string telefono, bool presente, List<string> interessi)
    {
        Contatto nuovoContatto = new Contatto
        {
            Id = lastIdController.GetNextId(),
            Nome = nome,
            Cognome = cognome,
            Telefono = telefono,
            Presente = presente,
            Interessi = interessi
        };

        var context = new ValidationContext(nuovoContatto);
        try 
        {
            // validate object restituisce un'eccezione se l'oggetto non è valido, altrimenti non restituisce nulla
            Validator.ValidateObject(nuovoContatto, context, true);
            contatti.Add(nuovoContatto);
            Salva();
            Console.WriteLine($"Validazione GetNextId(), valore di ID: {nuovoContatto.Id}");
            
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void ModificaContatto(int id, string nome, string cognome, string telefono, bool presente, List<string> interessi)
    {
        Contatto contattoEsistente = null;
        foreach (var contatto in contatti)
        {
            if (contatto.Id == id)
            {
                contattoEsistente = contatto;
                break;
            }
        }
        if (contattoEsistente != null)
        {
            contattoEsistente.Nome = nome;
            contattoEsistente.Cognome = cognome;
            contattoEsistente.Telefono = telefono;
            contattoEsistente.Presente = presente;
            contattoEsistente.Interessi = interessi;
            Salva();
        }
    }

    public void EliminaContatto(int id)
    {
        Contatto contattoEsistente = null;
        foreach (var contatto in contatti)
        {
            if (contatto.Id == id)
            {
                contattoEsistente = contatto;
                break;
            }
        }
        if (contattoEsistente != null)
        {
            contatti.Remove(contattoEsistente);
            Salva();
        }
    }

    public Contatto VisualizzaContatto(int id)
    {
        Contatto? contattoEsistente = null;
        foreach (var contatto in contatti)
        {
            if (contatto.Id == id)
            {
                contattoEsistente = contatto;
                break;
            }
        }
        if (contattoEsistente == null)
        {
            throw new Exception($"Contatto con ID {id} non trovato.");
        }
        return contattoEsistente;
    }

    public Contatto GetContattoById(int id)
    {
        foreach(Contatto contatto in contatti)
            if(contatto.Id==id)
                return contatto;
        throw new Exception($"Contatto con id {id} non trovato.");
    }
}