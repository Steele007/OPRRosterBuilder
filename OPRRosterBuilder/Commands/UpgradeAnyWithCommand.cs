using OPRRosterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Commands
{
    public class UpgradeAnyWithCommand : IModifyCommand
    {
        public Unit AssignedUnit { get; set; }
        public Modifier AssignedModifier { get; set; }
        public ModifierOption AssignedOption { get; set; }

        public UpgradeAnyWithCommand(Unit unit, Modifier mod, ModifierOption option)
        {
            this.AssignedUnit = unit;
            this.AssignedModifier = mod;
            this.AssignedOption = option;
        }

        public bool execute()
        {
            //Checks if the gear the option is upgrading is either present or not.          
            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                (string, int, int) targetValue;
                if ((!AssignedUnit.CurrentGear.TryGetValue(target.Item1, out targetValue) && !target.Item1.Equals("self"))  || AssignedModifier.CurrentNum == targetValue.Item2)
                {
                    return false;
                }
            }

            //Checks if the maximum number of choices for the modifier has already been made.
            if (AssignedModifier.CurrentNum >= AssignedModifier.TargetNum)
            {
                return false;
            }



            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                if (!target.Item1.Equals("self"))
                {
                    (string, int, int) dictValue;
                    AssignedUnit.CurrentGear.TryGetValue(target.Item1, out dictValue);
                    dictValue.Item3++;
                    AssignedUnit.CurrentGear.Remove(target.Item1);
                    AssignedUnit.CurrentGear.Add(target.Item1, dictValue);
                }

            }

            foreach ((string, string, int, int) gear in AssignedOption.OptionGear)
            {
                (string, int, int) dictValue;
                if (AssignedUnit.CurrentGear.TryGetValue(gear.Item1, out dictValue))
                {
                    dictValue.Item2++;

                    AssignedUnit.CurrentGear.Remove(gear.Item1);
                    AssignedUnit.CurrentGear.Add(gear.Item1, dictValue);

                }
                else
                {
                    AssignedUnit.CurrentGear.Add(gear.Item1, (gear.Item2, gear.Item3, gear.Item4));
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
            //Checks if the gear the option is upgrading is either present or not.
            (string, int, int) targetValue;
            foreach ((string, string, int, int) target in AssignedOption.OptionGear)
            {
                if (!AssignedUnit.CurrentGear.TryGetValue(target.Item1, out targetValue))
                {
                    return false;
                }
            }

            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                if (!target.Item1.Equals("self"))
                {

                    (string, int, int) dictValue;
                    AssignedUnit.CurrentGear.TryGetValue(target.Item1, out dictValue);
                    if (dictValue.Item3 == 0)
                    {
                        return false;
                    }
                    dictValue.Item3--;
                    AssignedUnit.CurrentGear.Remove(target.Item1);
                    AssignedUnit.CurrentGear.Add(target.Item1, dictValue);
                }

            }

            foreach ((string, string, int, int) gear in AssignedOption.OptionGear)
            {
                (string, int, int) dictValue;
                AssignedUnit.CurrentGear.TryGetValue(gear.Item1, out dictValue);
                dictValue.Item2--;
                if (dictValue.Item2 == 0)
                {
                    AssignedUnit.CurrentGear.Remove(gear.Item1);
                }
                else
                {
                    AssignedUnit.CurrentGear.Remove(gear.Item1);
                    AssignedUnit.CurrentGear.Add(gear.Item1, dictValue);
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
