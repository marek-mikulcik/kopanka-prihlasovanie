using Azure.Data.Tables;
using Azure.Identity;
using System.Text.Json;

namespace Marek.Prihlasenie.Data;

public class InfoService
{
    private TableClient client;
    public InfoService(string url)
    {
        #if DEBUG
        {
            client = new TableClient("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;", "prihlasovanie");
        }
        #else
        {
            client = new TableClient(new Uri(url), "prihlasovanie", new DefaultAzureCredential());
        }
        #endif
    }
    public async Task<Info> GetInfo(string tenant)
    {
        return await client.GetEntityAsync<Info>(tenant.ToLower(), "main");
    }
}
