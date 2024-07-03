using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboWiredPlugin.Core
{
    public interface IRoomWiredManager
    {
        public bool ProcessTriggers(string type, IWiredArguments wiredArguments = null);
    }
}
