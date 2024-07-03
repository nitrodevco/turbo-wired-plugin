using System.Collections.Generic;
using Turbo.Core.Game.Rooms.Object;
using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Core.Data
{
    public interface IWiredData
    {
        public bool SetFromMessage(IMessageEvent update);
        public bool SetRoomObject(IRoomObjectFloor roomObject);
        public int Id { get; }
        public int SpriteId { get; }
        public int WiredType { get; }
        public bool SelectionEnabled { get; }
        public int SelectionLimit { get; }
        public IList<int> SelectionIds { get; set; }
        public string StringParameter { get; set; }
        public IList<int> IntParameters { get; set; }
    }
}
