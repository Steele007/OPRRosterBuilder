using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Models
{
    public enum ModTypes { Replace, Replace_All, Replace_Any, Upgrade, Upgrade_Any, Upgrade_All, Replace_Up_To, Upgrade_All_Any, Upgrade_One_With, Upgrade_Any_With, Add}//Put this inside?
    public class Modifier
    {     
        public ModTypes ModType { get; set; }
        //The gear being modified or replaced. Also used when undoing a modification.
        public List<(string, string, int)> Target { get; set; }
        //List of individual options for each modifier.
        public List<ModifierOption> Options { get; set; }
        public int TargetNum { get; set; }
        public int CurrentNum { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
