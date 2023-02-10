using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class GameRoot
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("sport_key")]
        public string SportKey { get; set; }

        [JsonPropertyName("sport_title")]
        public string SportTitle { get; set; }

        [JsonPropertyName("commence_time")]
        public DateTime CommenceTime { get; set; }

        //[JsonPropertyName("home_team")]
        public string home_team { get; set; }

        //[JsonPropertyName("away_team")]
        public string away_team { get; set; }

        [JsonPropertyName("bookmakers")]
        public List<Bookmaker> Bookmakers { get; set; }
    }
}
