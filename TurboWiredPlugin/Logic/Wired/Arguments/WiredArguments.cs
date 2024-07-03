using Turbo.Core.Game.Rooms.Object;
using TurboWiredPlugin.Core;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Arguments
{
    public class WiredArguments : IWiredArguments
    {
        public IRoomObjectAvatar UserObject { get; set; }
        public IRoomObjectFloor FurnitureObject { get; set; }
    }
}
