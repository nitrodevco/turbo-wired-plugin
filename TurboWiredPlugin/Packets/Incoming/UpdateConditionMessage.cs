using System.Collections.Generic;
using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Incoming.Wired
{
    public record UpdateConditionMessage : UpdateWired, IMessageEvent
    {
    }
}
