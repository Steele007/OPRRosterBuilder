using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public enum ModTypes { Replace, Replace_All, Replace_Any, Upgrade, Upgrade_Any, Ugrade_All, Add}//Put this inside?
    public class Modifier
    {     
        public ModTypes ModType { get; set; }
        public List<(string, string, int)> Target { get; set; }
        public List<ModifierOption> Options { get; set; }
        public int TargetNum { get; set; }
        public int CurrentNum { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
