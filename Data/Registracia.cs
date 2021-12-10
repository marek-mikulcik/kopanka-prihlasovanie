using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Data.Tables;

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
    public DateTime Datum { get; set; }
}
