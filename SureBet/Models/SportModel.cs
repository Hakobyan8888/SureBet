using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class SportModel
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("title")]
        public string Title { get;set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("has_outrights")]
        public bool HasOutrights { get; set; }
    }
}
