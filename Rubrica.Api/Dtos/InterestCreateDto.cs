using System.ComponentModel.DataAnnotations;

namespace Rubrica.Api.Dtos;

public class InterestCreateDto
{
    [Required]
    [StringLength(100)]
    public string Nome {get;set;} = string.Empty;
}