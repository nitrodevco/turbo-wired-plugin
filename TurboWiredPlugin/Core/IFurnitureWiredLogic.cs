using Turbo.Core.Game.Rooms.Object;
using Turbo.Core.Game.Rooms.Object.Logic;
using TurboWiredPlugin.Core.Data;

namespace TurboWiredPlugin.Core
{
    public interface IFurnitureWiredLogic : IFurnitureFloorLogic
    {
        public void SetupWiredData(string jsonString = null);
        public IWiredData CreateWiredDataFromJson(string jsonString = null);
        public bool SaveWiredData(IRoomObjectAvatar avatar, IWiredData wiredData);

        public bool CanTrigger(IWiredArguments wiredArguments = null);
        public void OnTriggered(IWiredArguments wiredArguments = null);

        public IWiredData WiredData { get; }
        public int WiredKey { get; }
    }
}
