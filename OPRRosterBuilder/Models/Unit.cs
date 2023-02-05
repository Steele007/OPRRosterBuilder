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
        //Gear is as follows: <name, (rules text, number, upgrade status i.e. how many upgrades does this piece of gear have)>
        public Dictionary<string, (string, int, int)> StartingGear { get; set; }
        public Dictionary<string, (string, int, int)> CurrentGear { get; set; }
        //List of modification options for each unit.
        public List<Modifier> Modifiers { get; set; }
        public List<string> SpecialRules { get; set; }       
        public override string ToString() => JsonConvert.SerializeObject(this);
        
    }
}
