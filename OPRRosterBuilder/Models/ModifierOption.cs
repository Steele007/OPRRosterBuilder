using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public class ModifierOption
    {   //List of gear added (name, rules text, number, upgrade status)
        public List<(string, string, int, int)> OptionGear { get; set; }
        public int OptionPoints { get; set; }

        public bool OptionPicked { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
