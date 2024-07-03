using System.Collections.Generic;

namespace TurboWiredPlugin.Core.Data
{
    public interface IWiredActionData : IWiredData
    {
        public int Delay { get; }
        public IList<int> Conflicts { get; }
    }
}