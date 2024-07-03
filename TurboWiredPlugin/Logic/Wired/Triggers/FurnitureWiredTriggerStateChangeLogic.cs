using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Constants;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Triggers
{
    public class FurnitureWiredTriggerStateChangeLogic : FurnitureWiredTriggerLogic
    {
        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            if (wiredArguments.FurnitureObject == null) return false;

            if (!WiredData.SelectionIds.Contains(wiredArguments.FurnitureObject.Id)) return false;

            return true;
        }

        public override int WiredKey => (int)FurnitureWiredTriggerType.ToggleFurni;
    }
}
