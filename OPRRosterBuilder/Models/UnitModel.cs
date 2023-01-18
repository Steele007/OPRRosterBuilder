using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public class UnitModel
    {
        public Dictionary<string, (string, int)> Gear { get; set; }
    }
}
