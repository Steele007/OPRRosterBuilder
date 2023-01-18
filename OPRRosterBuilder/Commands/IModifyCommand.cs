using OPRRosterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Commands
{
    interface IModifyCommand
    {
        public Unit AssignedUnit { get; set; }
        public Modifier AssignedModifier { get; set; }
        public ModifierOption AssignedOption { get; set; }
        public bool execute();
        public bool undo();
    }
}
