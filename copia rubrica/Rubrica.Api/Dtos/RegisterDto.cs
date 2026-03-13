using System.ComponentModel.DataAnnotations;

namespace Rubrica.Api.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = string.Empty;

    [Required]
    [StringLength(100, MinimumLength=6)]
    public string Password {get;set;} = string.Empty;

    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;} = string.Empty;

    public string PhoneNumber {get;set;} = string.Empty;

}