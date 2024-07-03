using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Incoming.Wired
{
    public record ApplySnapshotMessage : IMessageEvent
    {
        public int ItemId { get; init; }
    }
}
