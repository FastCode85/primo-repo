using System.ComponentModel.DataAnnotations;

public class Contatto
{
    public int Id {get;set;}
    [Required]
    [StringLength(100)]
    public string NomeCompleto {get;set;}=string.Empty;
    [Required]
    [StringLength(30)]
    public string Telefono {get;set;}=string.Empty;
    //lista delle competenze del contatto.
    //in sqlite la salveremo come testo nel DbContext.
    public List<string> Competenze {get;set;} = new();
    public bool IsActive {get;set;} =true;
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    //foreign key: ogni contatto appartiene ad un utente
    public int UserId {get;set;}
    //proprietà di navigazione: EF Core usa questa proprietà
    //per collegare il contatto al suo utente
    public User User {get;set;} =null!;

}