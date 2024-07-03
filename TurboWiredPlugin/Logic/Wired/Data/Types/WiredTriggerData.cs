using System.Collections.Generic;
using System.Text.Json.Serialization;
using TurboWiredPlugin.Core.Data;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Data.Types
{
    public class WiredTriggerData : WiredDataBase, IWiredTriggerData
    {
        [JsonIgnore]
        public IList<int> Conflicts { get; protected set; }

        public WiredTriggerData() : base()
        {
            Conflicts = new List<int>();
        }
    }
}
