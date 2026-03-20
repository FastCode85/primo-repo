public class UpdateUserDto
{
    public string NomeCompleto {get;set;}= string.Empty;

    public string PhoneNumber {get;set;}= string.Empty;
    public bool NumeroInternazionale {get;set;} =false;
    public DateTime DataNascita {get;set;} =DateTime.UtcNow;
}