using TurboWiredPlugin.Core;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Actions
{
    public class FurnitureWiredActionToggleFurniStateLogic : FurnitureWiredActionLogic
    {
        public override bool CanTrigger(IWiredArguments wiredArguments = null)
        {
            if (!base.CanTrigger(wiredArguments)) return false;

            return true;
        }
    }
}
