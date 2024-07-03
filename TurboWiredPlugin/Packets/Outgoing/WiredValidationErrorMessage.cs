using Turbo.Core.Packets.Messages;

namespace TurboWiredPlugin.Packets.Outgoing.Wired
{
    public record WiredValidationErrorMessage : IComposer
    {
        public string Info { get; init; }
    }
}
