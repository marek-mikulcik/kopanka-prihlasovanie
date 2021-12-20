using Azure.Data.Tables;
using Azure.Identity;
using System.Text.Json;

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
    public async Task<Udalost> GetUdalost(string rowKey)
    {
        return await client.GetEntityAsync<Udalost>("main", rowKey);
    }

    public async Task<Udalost[]> GetUdalostiAsync(string[] terminy)
    {
        return await Task.WhenAll(terminy.Select(async item => await GetUdalost(item)));
    }
    public async Task<Udalost[]> GetAllAsync()
    {
        return await Task.WhenAll(terminy.Select(async item => await GetUdalost(item)));
    }

    public async Task UpdateAsync(Udalost udalost)
    {
        await client.UpdateEntityAsync<Udalost>(udalost, udalost.ETag, TableUpdateMode.Replace);
    }

    public async Task<bool> Registruj(string rowKey, Registracia registracia)
    {
        var udalostResponse = await client.GetEntityAsync<Udalost>("main", rowKey);
        var udalost = udalostResponse.Value;
        udalost.pocet += registracia.pocet;
        if (udalost.pocet > udalost.max)
            throw new ApplicationException("Prekroceny max");
        var registracie = new List<Registracia>();
        if (!string.IsNullOrEmpty(udalost.registracie))
            registracie = JsonSerializer.Deserialize<List<Registracia>>(udalost.registracie);
        registracie?.Add(registracia);
        udalost.registracie = JsonSerializer.Serialize(registracie);
        await client.UpdateEntityAsync<Udalost>(udalost, udalost.ETag, TableUpdateMode.Replace);

        return true;
    }
}
