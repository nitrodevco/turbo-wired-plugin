using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Constants;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Conditions
{
    public class FurnitureWiredConditionFurniHasAvatars : FurnitureWiredConditionLogic
    {
        protected static int _anyFurni = 0;
        protected static int _allFurni = 1;

        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            foreach (var floorObject in _selectedFloorObjects)
            {
                var roomTiles = floorObject.Logic.GetCurrentTiles();

                if (roomTiles.Count == 0) continue;

                foreach (var roomTile in roomTiles)
                {
                    if (roomTile == null || roomTile.Avatars.Count == 0) return false;
                }
            }

            return true;
        }

        public override int WiredKey => (int)FurnitureWiredConditionType.FurniHasAvatars;
    }
}