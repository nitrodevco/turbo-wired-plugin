using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Outgoing.Wired
{
    public record OpenMessage : IComposer
    {
        public int ItemId { get; init; }
    }
}
