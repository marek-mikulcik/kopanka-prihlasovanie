using System.ComponentModel.DataAnnotations;

namespace Marek.Prihlasenie.Data;

public class Registracia
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Meno { get; set; }
    [Required, MinLength(13), MaxLength(13), RegularExpression("^[\\+421][0-9]*$")]
    [Phone]
    public string Telefon { get; set; } = "+421";
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public int pocet { get; set; } = 1;

    [Required]
    public string Rocnik {get; set; } 
    public string RowKey { get; set; }
    public bool DoCakarne { get; set; }
    public DateTime Datum { get; set; }

}
