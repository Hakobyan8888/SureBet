using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class Market
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("outcomes")]
        public List<Outcome> Outcomes { get; set; }
    }
}
