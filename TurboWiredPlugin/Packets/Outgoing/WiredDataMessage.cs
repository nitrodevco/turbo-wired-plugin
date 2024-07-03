using Turbo.Core.Packets.Messages;
using TurboWiredPlugin.Core.Data;

namespace TurboWiredPlugin.Packets.Outgoing
{
    public record WiredDataMessage : IComposer
    {
        public IWiredData WiredData { get; init; }
    }
}
