using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Constants;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Conditions
{
    public class FurnitureWiredConditionNotTriggererOnFurni : FurnitureWiredConditionLogic
    {
        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            var roomTile = wiredArguments.UserObject?.Logic?.GetCurrentTile();

            if (roomTile == null) return false;

            foreach (var floorObject in _selectedFloorObjects)
            {
                if (roomTile.HighestObject == floorObject) return false;
            }

            return true;
        }

        public override bool RequiresAvatar => true;

        public override int WiredKey => (int)FurnitureWiredConditionType.NotTriggererOnFurni;
    }
}