using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
public class LastIdController
{
    // private per il percorso in modo che non sia accessibile da altre parti del programma
    // readonly per indicare che il valore non può essere modificato dopo l'inizializzazione
    private readonly string path = "lastId.json";
    // private per l'oggetto lastIdObj in modo che non sia accessibile da altre parti del programma
    private LastId lastId;

    // questo è il costruttore della classe LastIdController, che viene chiamato quando viene creata un'istanza della classe
    // viene definito pubblico per permettere la creazione di istanze della classe da altre parti del programma
    public LastIdController()
    {
        if (!File.Exists(path))
        {
            lastId = new LastId { Id = 0 };
            Salva();
        }
        else
        {
            string json = File.ReadAllText(path);
            // ?? è un operatore di coalescenza nulla
            // restituisce il valore a sinistra se non è null, altrimenti restituisce il valore a destra
            lastId = JsonConvert.DeserializeObject<LastId>(json) ?? new LastId { Id = 0 };
        }
    }

    public int GetNextId()
    {
        lastId.Id++;
        lastId.Id=-5;
        var context = new ValidationContext(lastId);
        try
        {
            // validate object restituisce un'eccezione se l'oggetto non è valido, altrimenti non restituisce nulla
            Validator.ValidateObject(lastId, context, true);
            Console.WriteLine($"Validazione GetNextId(), valore di ID: {lastId.Id}");
            Salva();
            
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        return lastId.Id;
    }

    private void Salva()
    {
        string json = JsonConvert.SerializeObject(lastId, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}