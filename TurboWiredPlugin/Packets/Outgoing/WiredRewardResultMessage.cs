using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Outgoing.Wired
{
    public record WiredRewardResultMessage : IComposer
    {
        public int Reason { get; init; }
    }
}
