using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Turbo.Core.Networking.Game.Clients;
using TurboWiredPlugin.Core.Data;
using TurboWiredPlugin.Logic.Furniture.Wired.Data.Types;
using TurboWiredPlugin.Packets.Outgoing.Wired;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Triggers
{
    public class FurnitureWiredTriggerLogic : FurnitureWiredLogic
    {
        public override IWiredData CreateWiredDataFromJson(string jsonString = null)
        {
            if (jsonString == null) return new WiredTriggerData();

            return JsonSerializer.Deserialize<WiredTriggerData>(jsonString);
        }

        public override void SendConfigToSession(ISession session)
        {
            if (session == null) return;

            session.Send(new WiredTriggerDataMessage
            {
                WiredData = WiredData
            });
        }

        public override IWiredTriggerData WiredData => (IWiredTriggerData)_wiredData;
    }
}
