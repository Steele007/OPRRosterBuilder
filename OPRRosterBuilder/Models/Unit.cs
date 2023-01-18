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
        public int Points { get; set; }
        public int Quality { get; set; }
        public int Defense { get; set; }
        public Dictionary<string, (string, int)> StartingGear { get; set; }
        public Modifier[] Modifiers { get; set; }
        public string[] SpecialRules { get; set; }
        //Add an array of Models?
        public override string ToString() => JsonConvert.SerializeObject(this);
        
    }
}
