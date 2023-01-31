using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public class ArmyList
    {
        public string ArmyName { get; set; }
        public int Points { get; set; }
        public List<Unit> Units { get; set; }
        public ArmyList(List<Unit> units, int points, string armyName)
        {
            this.Units = units;
            this.Points = points;
            this.ArmyName = armyName;
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
