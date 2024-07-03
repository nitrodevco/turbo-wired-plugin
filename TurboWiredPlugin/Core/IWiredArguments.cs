using Turbo.Core.Game.Rooms.Object;

namespace TurboWiredPlugin.Core
{
    public interface IWiredArguments
    {
        public IRoomObjectAvatar UserObject { get; set; }
        public IRoomObjectFloor FurnitureObject { get; set; }
    }
}
