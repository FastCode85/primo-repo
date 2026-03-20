namespace Rubrica.Api.Dtos;

public class AuthResponseDto
{
    public string Token {get;set;} = string.Empty;
    public string UserId {get;set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string NomeCompleto {get;set;} = string.Empty;
    public bool NumeroInternazionale {get;set;} =false;
    public DateTime DataNascita {get;set;} =DateTime.UtcNow;
}