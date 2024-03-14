using System.Numerics;
using System.Text.Json;
using Client.Models;
using static System.Windows.Forms.Design.AxImporter;

namespace Client.Services
{
    internal class GreetingService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        internal GreetingService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7266/");
        }

        internal async Task<string?> GetGreetingMessage()
        {
            HttpResponseMessage response = await _client.GetAsync("/Greeting");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                Greeting? greeting = JsonSerializer.Deserialize<Greeting>(responseBody, options);
                return greeting?.Message;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}
