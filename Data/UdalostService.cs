using Azure.Data.Tables;
using Azure.Identity;

namespace Marek.Prihlasenie.Data;

public class UdalostService
{
    private TableClient client;
    private static readonly string[] terminy = new[]
    {
        "termin_1", "termin_2", "termin_3", "termin_4"
    };

    public UdalostService(string url)
    {
        client = new TableClient(new Uri(url), "prihlasovanie", new DefaultAzureCredential());
    }
    private async Task<Udalost> GetUdalost(string rowKey)
    {
        return await client.GetEntityAsync<Udalost>("main", rowKey);
    }

    public async Task<Udalost[]> GetAllAsync()
    {
        return await Task.WhenAll(terminy.Select(async item => await GetUdalost(item)));
    }

    public async Task UpdateAsync(Udalost udalost)
    {
        await client.UpsertEntityAsync<Udalost>(udalost);
    }
}
