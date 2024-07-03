using System.Collections.Generic;
using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Incoming.Wired
{
    public record UpdateActionMessage : UpdateWired, IMessageEvent
    {
        public int Delay { get; init; }
    }
}
