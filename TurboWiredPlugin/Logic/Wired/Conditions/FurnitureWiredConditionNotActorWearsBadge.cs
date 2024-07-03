using Turbo.Core.Game.Players;
using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Constants;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Conditions
{
    public class FurnitureWiredConditionNotActorWearsBadge : FurnitureWiredConditionLogic
    {
        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            if (_wiredData.StringParameter == null || _wiredData.StringParameter.Length == 0) return true;

            if (wiredArguments.UserObject.RoomObjectHolder is IPlayer player)
            {
                var activeBadges = player.PlayerInventory?.BadgeInventory?.ActiveBadges;

                if (activeBadges == null || activeBadges.Count == 0) return true;

                foreach (var activeBadge in activeBadges)
                {
                    if (activeBadge.BadgeCode.ToLower().Equals(_wiredData.StringParameter.ToLower())) return false;
                }
            }

            return true;
        }

        public override bool RequiresAvatar => true;

        public override int WiredKey => (int)FurnitureWiredConditionType.ActorIsWearingBadge;
    }
}