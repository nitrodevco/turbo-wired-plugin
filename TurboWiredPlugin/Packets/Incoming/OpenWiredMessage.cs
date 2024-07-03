using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Incoming.Wired
{
    public record OpenWiredMessage : IMessageEvent
    {
        public int ItemId { get; init; }
    }
}
