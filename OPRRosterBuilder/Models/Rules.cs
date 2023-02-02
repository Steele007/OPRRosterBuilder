using Newtonsoft.Json;
using System.Collections.Generic;

namespace OPRRosterBuilder.Models
{

    public struct Rule
    {
        public string Name { get; set; }
        public string text { get; set; }
    }

    public class Rules
    {
        public string RulesName { get; set; }
        public List<Rule> RulesList { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
