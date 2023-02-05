using OPRRosterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Commands
{
    public class ReplaceAllCommand : IModifyCommand
    {
        public Unit AssignedUnit { get; set; }
        public Modifier AssignedModifier { get; set; }
        public ModifierOption AssignedOption { get; set; }

        public ReplaceAllCommand(Unit unit, Modifier mod, ModifierOption option)
        {
            this.AssignedUnit = unit;
            this.AssignedModifier = mod;
            this.AssignedOption = option;
        }

        public bool execute()
        {
            //Checks if the gear the option is replacing is either not present or has an upgrade attached.
            //Also checks how much of the gear can be replaced (if a unit has 5x pistols and 3x ccws and we're replacing pistols and ccws, 
            //it will only replace 3 pairs.)
            (string, int, int) targetValue;
            int NumberOfReplacedGear = -1;
            foreach ((string, string, int) target in AssignedModifier.Target)
            {
                if (!AssignedUnit.CurrentGear.TryGetValue(target.Item1, out targetValue) || targetValue.Item3>0)
                {
                    return false;
                }
                else
                {
                    if(NumberOfReplacedGear == -1)
                    {
                        NumberOfReplacedGear = targetValue.Item2;
                    }else if (targetValue.Item2 < NumberOfReplacedGear)
                    {
                        NumberOfReplacedGear = targetValue.Item2;
                    }
                }
            }

            if (AssignedModifier.CurrentNum >= AssignedModifier.TargetNum)
            {
                return false;
            }


            foreach ((string, string, int) target in AssignedModifier.Target)
            {

                //Decrements the target gear by 1 if it's being replaced.
                (string, int, int) dictValue;
                AssignedUnit.CurrentGear.TryGetValue(target.Item1, out dictValue);
                dictValue.Item2 -= NumberOfReplacedGear;
                if (dictValue.Item2 == 0)
                {
                    AssignedUnit.CurrentGear.Remove(target.Item1);
                }
                else
                {
                    AssignedUnit.CurrentGear.Remove(target.Item1);
                    AssignedUnit.CurrentGear.Add(target.Item1, dictValue);
                }

            }

            foreach ((string, string, int, int) gear in AssignedOption.OptionGear)
            {
                if (!AssignedUnit.CurrentGear.TryAdd(gear.Item1, (gear.Item2, NumberOfReplacedGear, 0)))
                {
                    (string, int, int) dictValue;
                    AssignedUnit.CurrentGear.TryGetValue(gear.Item1, out dictValue);
                    dictValue.Item2+= NumberOfReplacedGear;

                    AssignedUnit.CurrentGear.Remove(gear.Item1);
                    AssignedUnit.CurrentGear.Add(gear.Item1, dictValue);
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
            //Essentially resets the unit to its default state.
            AssignedUnit.CurrentGear.Clear();
            
            foreach(string gearName in AssignedUnit.StartingGear.Keys)
            {
                (string, int, int) dictValue;
                AssignedUnit.StartingGear.TryGetValue(gearName, out dictValue);
                AssignedUnit.CurrentGear.Add(gearName, dictValue);
            }

            //For some reason AssignedOption.Item3 = true throws an error.
            ModifierOption temp = AssignedOption;
            temp.OptionPicked = false;
            AssignedOption = temp;

            AssignedUnit.Points = AssignedUnit.OriginalPoints;
            AssignedModifier.CurrentNum--;//No need to check against target?

            return true;
        }
    }
}
