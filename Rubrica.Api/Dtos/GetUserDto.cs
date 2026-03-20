public class GetUserDto
{
    public string Id {get;set;}
    public string NomeCompleto {get;set;}
    public string PhoneNumber {get;set;}
    public DateTime CreatedAt {get;set;}
    public string Email {get;set;}
    public bool NumeroInternazionale {get;set;} =false;
    public DateTime DataNascita {get;set;} =DateTime.UtcNow;
}