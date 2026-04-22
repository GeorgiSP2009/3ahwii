using BlazorApp1.Models;
using System.Net.Http.Json;

namespace BlazorApp1.Services;

public class NumVerifyService(HttpClient httpClient, IConfiguration configuration)
{
    public async Task<NumVerifyResult?> ValidateAsync(string phoneNumber)
    {
        var apiKey = configuration["NumVerify:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("NumVerify API key is missing. Set NumVerify:ApiKey in appsettings.Development.json.");
        }

        var requestUri = $"http://apilayer.net/api/validate?access_key={Uri.EscapeDataString(apiKey)}&number={Uri.EscapeDataString(phoneNumber)}";
        return await httpClient.GetFromJsonAsync<NumVerifyResult>(requestUri);
    }
}
