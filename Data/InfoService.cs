using Azure.Data.Tables;
using Azure.Identity;
using System.Text.Json;

namespace Marek.Prihlasenie.Data;

public class InfoService
{
    private TableClient client;
    public InfoService(string url)
    {
        client = new TableClient(new Uri(url), "prihlasovanie", new DefaultAzureCredential());
    }
    public async Task<Info> GetInfo()
    {
        return await client.GetEntityAsync<Info>("main", "main");
    }
}
