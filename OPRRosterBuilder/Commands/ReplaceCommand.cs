using OPRRosterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Commands
{
    public class ReplaceCommand : IModifyCommand
    {
        public Unit AssignedUnit { get; set; }
        public Modifier AssignedModifier { get; set; }
        public ModifierOption AssignedOption { get; set; }

        public ReplaceCommand(Unit unit, Modifier mod, ModifierOption option)
        {
            this.AssignedUnit = unit;
            this.AssignedModifier = mod;
            this.AssignedOption = option;
        }

        public bool execute() 
        {
            //Checks if the gear the option is replacing is either not present or has an upgrade attached.
            (string, int, bool) targetValue;
            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                if (!AssignedUnit.StartingGear.TryGetValue(target.Item1, out targetValue) || targetValue.Item3)
                {
                    return false;
                }
            }

            if(AssignedModifier.CurrentNum >= AssignedModifier.TargetNum)
            {
                return false;
            }

            
            foreach ((string, string, int) target in AssignedModifier.Target)
            {

                //Decrements the target gear by 1 if it's being replaced
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(target.Item1, out dictValue);
                dictValue.Item2--;
                if (dictValue.Item2 == 0)
                {
                    AssignedUnit.StartingGear.Remove(target.Item1);
                }
                else
                {
                    AssignedUnit.StartingGear.Remove(target.Item1);
                    AssignedUnit.StartingGear.Add(target.Item1, dictValue);
                }

            }

            foreach ((string, string, int, bool) gear in AssignedOption.OptionGear)
            {
                if (!AssignedUnit.StartingGear.TryAdd(gear.Item1, (gear.Item2, gear.Item3, false)))
                {
                    (string, int, bool) dictValue;
                    AssignedUnit.StartingGear.TryGetValue(gear.Item1, out dictValue);
                    dictValue.Item2++;

                    AssignedUnit.StartingGear.Remove(gear.Item1);
                    AssignedUnit.StartingGear.Add(gear.Item1, dictValue);
                }
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
            //Checks if the gear the option is replacing is either not present or has an upgrade attached.
            (string, int, bool) targetValue;
            foreach ((string, string, int, bool) target in AssignedOption.OptionGear)
            {
                if (!AssignedUnit.StartingGear.TryGetValue(target.Item1, out targetValue) || targetValue.Item3)
                {
                    return false;
                }
            }

            
            foreach ((string, string, int, bool) target in AssignedOption.OptionGear)
            {

                //Decrements the target gear by 1 if it's being replaced
                (string, int, bool) dictValue;
                AssignedUnit.StartingGear.TryGetValue(target.Item1, out dictValue);
                dictValue.Item2--;
                if (dictValue.Item2 == 0)
                {
                    AssignedUnit.StartingGear.Remove(target.Item1);
                }
                else
                {
                    AssignedUnit.StartingGear.Remove(target.Item1);
                    AssignedUnit.StartingGear.Add(target.Item1, dictValue);
                }

            }

            foreach ((string, string, int) gear in AssignedModifier.Target)
            {

                if (!AssignedUnit.StartingGear.TryAdd(gear.Item1, (gear.Item2, gear.Item3, false)))
                {

                    (string, int, bool) dictValue;
                    AssignedUnit.StartingGear.TryGetValue(gear.Item1, out dictValue);
                    dictValue.Item2++;

                    AssignedUnit.StartingGear.Remove(gear.Item1);
                    AssignedUnit.StartingGear.Add(gear.Item1, dictValue);
                }
            }

                //For some reason AssignedOption.Item3 = true throws an error.
            ModifierOption temp = AssignedOption;
            temp.OptionPicked = false;
            AssignedOption = temp;

            AssignedUnit.Points -= AssignedOption.OptionPoints;
            AssignedModifier.CurrentNum--;//No need to check against target?

            return true;         
        }

    }
}
