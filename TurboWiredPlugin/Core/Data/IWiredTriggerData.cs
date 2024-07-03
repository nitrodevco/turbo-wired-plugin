using System.Collections.Generic;

namespace TurboWiredPlugin.Core.Data
{
    public interface IWiredTriggerData : IWiredData
    {
        public IList<int> Conflicts { get; }
    }
}