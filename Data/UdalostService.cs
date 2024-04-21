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
        udalost.LastId++;
        registracia.Id = udalost.LastId;
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

    public async Task<bool> OdRegistruj(string rowKey, int id, string telefon, int pocet)
    {
        var udalostResponse = await client.GetEntityAsync<Udalost>("main", rowKey);
        var udalost = udalostResponse.Value;
        udalost.pocet -= pocet;

        var registracie = new List<Registracia>();
        if (!string.IsNullOrEmpty(udalost.registracie))
            registracie = JsonSerializer.Deserialize<List<Registracia>>(udalost.registracie);
        var registracia = (registracie?.First(r => r.Id == id && r.Telefon == telefon)) ?? throw new ApplicationException("Zadana registracia neexistuje!");
        registracie.Remove(registracia);
        udalost.registracie = JsonSerializer.Serialize(registracie);
        await client.UpdateEntityAsync<Udalost>(udalost, udalost.ETag, TableUpdateMode.Replace);

        return true;
    }    
}
