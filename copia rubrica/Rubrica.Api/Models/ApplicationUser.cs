using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrica.Api.Models;

[Table("users")]
public class ApplicationUser : IdentityUser
{
    //IdentityUser ha già
    //Id,Username,Email,PasswordHash,PhoneNumber, ecc

    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;} = string.Empty;
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public List<Interest> Interests {get;set;} = new List<Interest>();

}