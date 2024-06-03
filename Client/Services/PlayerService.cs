
using System.Configuration;
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
            string configFilePath = @".\App.config";
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFilePath
            };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            string host = config.AppSettings.Settings["Host"]?.Value ?? "localhost";
            string port = config.AppSettings.Settings["Port"]?.Value ?? "5000";
            _client = new HttpClient();
            string baseAddress = $"http://{host}:{port}/";
            _client.BaseAddress = new Uri(baseAddress);
        }

        internal async Task<Player?> GetPlayer(string phoneNum)
        {
            var obj = new
            {
                phoneNumber = phoneNum,
            };
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/Player/findPlayer", content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                List<Player>? player = JsonSerializer.Deserialize<List<Player>>(responseBody);
                if (player.Count != 0)
                    return player[0];
                else
                    return null;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        internal async Task<bool> CreatePlayer(Player player)
        {

            var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/Player/createPlayer", content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                ResponseCreatePlayer? msg = JsonSerializer.Deserialize<ResponseCreatePlayer>(responseBody);
                if (msg.status)
                    return true;
                else
                    return false;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        internal async Task<bool> PlayerBet(int userId, int betNumber)
        {
            var obj = new
            {
                userId = userId,
                betNumber = betNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/Player/bet", content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                ResponseCreatePlayer? msg = JsonSerializer.Deserialize<ResponseCreatePlayer>(responseBody);
                return true;
            }
            else
            {
                //response.RequestMessage;
                return false;
            }
        }

        internal async Task<List<BoardBet>>  LoadBoardBet(int playerId){
  
            HttpResponseMessage response = await _client.GetAsync("/Player/GetBoardBet?playerId=" + playerId);
            string responseBody = await response.Content.ReadAsStringAsync();
            List<BoardBet> bb = JsonSerializer.Deserialize<List<BoardBet>>(responseBody);
            return bb;
        }

        public class ResponseCreatePlayer
        {

            public string message { get; set; }
            public bool status { get; set; }

        }
        public class BoardBet
        {

            public int id { get; set; }
            public string fullName { get; set; }
            public DateTime dateOfBirth { get; set; }
            public string phoneNumber { get; set; }
            public int betID { get; set; }
            public DateTime drawTime { get; set; }
            public int? betNumber { get; set; }
            public int? resultNumber { get; set; }
            public bool? isWinner { get; set; }

        }


    }
}
