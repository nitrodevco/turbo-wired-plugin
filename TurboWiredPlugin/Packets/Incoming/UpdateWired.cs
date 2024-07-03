using System.Collections.Generic;

namespace TurboWiredPlugin.Packets.Incoming.Wired
{
    public record UpdateWired
    {
        public int ItemId { get; init; }
        public IList<int> IntegerParams { get; init; }
        public string StringParam { get; init; }
        public IList<int> SelectedItemIds { get; init; }
        public int SelectionCode { get; init; }
    }
}