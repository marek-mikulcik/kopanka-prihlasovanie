using System.ComponentModel.DataAnnotations;

namespace Marek.Prihlasenie.Data;

public class Registracia
{
    [Required]
    public string Meno { get; set; }
    [Required]
    public string Telefon { get; set; }
    [Required]
    public int pocet { get; set; } = 1;
    public string RowKey { get; set; }
    public bool DoCakarne { get; set; }
    public DateTime Datum { get; set; }

    [Required, Range(typeof(bool),"true","true")]
    public bool OP { get; set; }
}
