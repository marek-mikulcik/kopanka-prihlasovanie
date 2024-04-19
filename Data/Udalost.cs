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
    public bool Visible { get; set; } = true;
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    [IgnoreDataMember]
    public bool Expanded { get; set; } = false;

    [IgnoreDataMember]
    public bool Prihlaseny { get; set; } = false;
    public int PocetCakaren { get; set; }
    public string RegistracieCakaren { get; set; }
    public string HtmlPopis { get; set; }

    [IgnoreDataMember]
    public List<Registracia> RegList { get; set; }

    [IgnoreDataMember]
    public string RegText { get; set; } = string.Empty;

    [IgnoreDataMember]
    public bool RegResult { get; set; }

    public int LastId { get; set; } = 0;
}
