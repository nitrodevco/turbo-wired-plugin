using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Constants;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Conditions
{
    public class FurnitureWiredConditionStuffTypeMatches : FurnitureWiredConditionLogic
    {
        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            foreach (var floorObject in _selectedFloorObjects)
            {
                if (wiredArguments.FurnitureObject == floorObject) return true;

                if (floorObject.Logic.FurnitureDefinition != wiredArguments.FurnitureObject.Logic.FurnitureDefinition) continue;

                return true;
            }

            return false;
        }

        public override bool RequiresAvatar => true;

        public override bool RequiresFurni => true;

        public override int WiredKey => (int)FurnitureWiredConditionType.StuffTypeMatches;
    }
}