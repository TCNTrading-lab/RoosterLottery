﻿
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Client.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

            HttpResponseMessage response = await _client.GetAsync("/Player/findPlayer");

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
