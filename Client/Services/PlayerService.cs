
using System.Text;
using System.Text.Json;
using Client.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Client.Services
{
    internal class PlayerService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        internal PlayerService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7266/");
        }

        internal async Task<string?> GetPlayer()
        {
            var obj = new
            {
                phoneNumber = "123",
            };
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/Player/findPlayer", content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Player? player = JsonSerializer.Deserialize<Player>(responseBody);
                return player?.FullName;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}
