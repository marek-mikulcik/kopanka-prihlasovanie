using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace Marek.Prihlasenie.Data;

public class Udalost : ITableEntity
{
    public string Nazov { get; set; }
    public int max { get; set; }
    public int pocet { get; set; }
    public string registracie { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    [IgnoreDataMember]
    public bool Expanded { get; set; } = false;

    [IgnoreDataMember]
    public bool Prihlaseny {get; set; } = false;
}