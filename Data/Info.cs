using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Data.Tables;

namespace Marek.Prihlasenie.Data;

public class Info: ITableEntity
{
    public string Nadpis { get; set; } = "Vianoce 2021";
    public DateTime Zaciatok { get; set; }
    public DateTime Koniec { get; set; }
    public string Terminy { get; set; } = "termin_1,termin_2";

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
