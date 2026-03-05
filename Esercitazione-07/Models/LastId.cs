using System.ComponentModel.DataAnnotations;

public class LastId
{
    [Range(0, int.MaxValue, ErrorMessage = "L'ID deve essere un numero intero positivo.")]
    public int Id {get;set;}

/*
    public void Validate()
    {
        // context indica l'oggetto da validare, in questo caso lastId
        ValidationContext context = new ValidationContext(Id);

        try
        {
            // validate object restituisce un'eccezione se l'oggetto non è valido, altrimenti non restituisce nulla
            Validator.ValidateObject(Id, context, true);
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    */
}