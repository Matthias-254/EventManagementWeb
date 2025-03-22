using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5173/api/")
        };
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var loginModel = new
        {
            Name = username,
            Password = password
        };

        var response = await _httpClient.PutAsJsonAsync("accounts/login", loginModel);
        return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
    }
}

