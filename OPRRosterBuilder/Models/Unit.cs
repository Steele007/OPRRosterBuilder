using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public class Unit
    {
        public string UnitName { get; set; }
        public int UnitSize { get; set; }
        public int OriginalPoints { get; set; }
        public int Points { get; set; }
        public int Quality { get; set; }
        public int Defense { get; set; }
        //Key is the item name, the value is (rules text, point cost and whether or the item has been upgraded)
        public Dictionary<string, (string, int, int)> StartingGear { get; set; }
        public Dictionary<string, (string, int, int)> CurrentGear { get; set; }
        public Modifier[] Modifiers { get; set; }
        public List<string> SpecialRules { get; set; }
        //Add an array of Models?
        public override string ToString() => JsonConvert.SerializeObject(this);
        
    }
}
