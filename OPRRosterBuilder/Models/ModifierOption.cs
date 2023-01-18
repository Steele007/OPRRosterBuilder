using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public class ModifierOption
    {
        [JsonProperty("Item1")]
        public List<(string, string, int)> OptionGear { get; set; }

        [JsonProperty("Item2")]
        public int OptionPoints { get; set; }

        [JsonProperty("Item3")]
        public bool OptionPicked { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
