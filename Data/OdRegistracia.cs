using System.ComponentModel.DataAnnotations;

namespace Marek.Prihlasenie.Data;

public class OdRegistracia
{
    [Required]
    public int Id { get; set; }

    [Required, MinLength(13), MaxLength(13), RegularExpression("^[\\+421][0-9]*$")]
    [Phone]
    [Compare("OldTelefon", ErrorMessage = "Telefónne číslo musí byť zhodné ako zaregistrované")]
    public string Telefon { get; set; } = "+421";

    public string OldTelefon { get; set; }

     public string RowKey { get; set; }
     public int pocet { get; set; }
}
