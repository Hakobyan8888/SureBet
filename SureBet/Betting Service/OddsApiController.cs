using Newtonsoft.Json;
using SureBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureBet.Betting_Service
{
    public class OddsApiController
    {
        private const string API_KEY = "96a45c050eec7ead4ae31e382e4c14a7";//"84d5a94a19726b5585b8b291a293f3c2";//"81007d08eee416c8f64dc910fcf61ac0";
        private const string BASE_URL = "https://api.the-odds-api.com/v4/sports/";
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Get all sports
        /// </summary>
        /// <returns></returns>
        public async Task<List<SportModel>> GetSportsAsync()
        {
            string fullUri = $"{BASE_URL}?apiKey={API_KEY}";
            var response = await client.GetAsync(fullUri);
            var content = await response.Content.ReadAsStringAsync();
            var sportModels = JsonConvert.DeserializeObject<List<SportModel>>(value: content);
            return sportModels;
        }

        /// <summary>
        /// Get the odds on specific sport from different bookmakers
        /// </summary>
        /// <param name="sport">Name of the competition(SportModel.key)</param>
        /// <param name="region">bookmaker regions(us, uk, au, eu, )</param>
        /// <param name="markets"></param>
        /// <param name="oddsFormat"></param>
        /// <returns></returns>
        public async Task<List<GameRoot>> GetOddsAsync(string sport, string region = "us", string markets = "h2h", string oddsFormat = "decimal")
        {
            string fullUri = $"{BASE_URL}/{sport}/odds/?apiKey={API_KEY}&regions={region}&markets={markets}&oddsFormat={oddsFormat}";
            var response = await client.GetAsync(fullUri);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                return null;
            var gameRoot = JsonConvert.DeserializeObject<List<GameRoot>>(content);
            return gameRoot;
        }
    }
}
