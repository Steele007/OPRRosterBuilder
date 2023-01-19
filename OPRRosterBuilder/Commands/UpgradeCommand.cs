using OPRRosterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Commands
{
    public class UpgradeCommand : IModifyCommand
    {
        public Unit AssignedUnit { get; set; }
        public Modifier AssignedModifier { get; set; }
        public ModifierOption AssignedOption { get; set; }

        public UpgradeCommand(Unit unit, Modifier mod, ModifierOption option)
        {
            this.AssignedUnit = unit;
            this.AssignedModifier = mod;
            this.AssignedOption = option;
        }

        public bool execute()
        {
            //Checks if the gear the option is ugrading is either present or not.          
            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                if (!AssignedUnit.StartingGear.ContainsKey(target.Item1))
                {
                    return false;
                }
            }

            if (AssignedModifier.CurrentNum >= AssignedModifier.TargetNum)
            {
               return false; 
            }

            

            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(target.Item1, out dictValue);
                dictValue.Item3 = true;
                AssignedUnit.StartingGear.Remove(target.Item1);
                AssignedUnit.StartingGear.Add(target.Item1, dictValue);
            }

            foreach ((string, string, int, bool) gear in AssignedOption.OptionGear)
            {
              
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(gear.Item1, out dictValue);
                dictValue.Item2++;

                AssignedUnit.StartingGear.Remove(gear.Item1);
                AssignedUnit.StartingGear.Add(gear.Item1, dictValue);
                
            }

                //For some reason AssignedOption.Item3 = true throws an error.
            ModifierOption temp = AssignedOption;
            temp.OptionPicked = true;
            AssignedOption = temp;

            AssignedUnit.Points += AssignedOption.OptionPoints;
            AssignedModifier.CurrentNum++;

            return true;

        }

        public bool undo()
        {
            //Checks if the gear the option is ugrading is either present or not.          
            foreach ((string, string, int, bool) target in AssignedOption.OptionGear)
            {
                if (!AssignedUnit.StartingGear.ContainsKey(target.Item1))
                {
                    return false;
                }
            }

            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(target.Item1, out dictValue);
                dictValue.Item3 = false;
                AssignedUnit.StartingGear.Remove(target.Item1);
                AssignedUnit.StartingGear.Add(target.Item1, dictValue);
            }

            foreach ((string, string, int, bool) gear in AssignedOption.OptionGear)
            {
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(gear.Item1, out dictValue);
                dictValue.Item2--;
                if (dictValue.Item2 == 0)
                {
                    AssignedUnit.StartingGear.Remove(gear.Item1);
                }
                else
                {
                    AssignedUnit.StartingGear.Remove(gear.Item1);
                    AssignedUnit.StartingGear.Add(gear.Item1, dictValue);
                }
                
            }

            //For some reason AssignedOption.Item3 = true throws an error.
            ModifierOption temp = AssignedOption;
            temp.OptionPicked = false;
            AssignedOption = temp;

            AssignedUnit.Points -= AssignedOption.OptionPoints;
            AssignedModifier.CurrentNum--;

            return true;
        }
    }
}
