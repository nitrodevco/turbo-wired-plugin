using System.Text.Json;
using Turbo.Core.Networking.Game.Clients;
using TurboWiredPlugin.Core.Data;
using TurboWiredPlugin.Logic.Furniture.Wired.Data.Types;
using TurboWiredPlugin.Packets.Outgoing.Wired;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Conditions
{
    public class FurnitureWiredConditionLogic : FurnitureWiredLogic
    {
        public override IWiredData CreateWiredDataFromJson(string jsonString = null)
        {
            if (jsonString == null) return new WiredConditionData();

            return JsonSerializer.Deserialize<WiredConditionData>(jsonString);
        }

        public override void SendConfigToSession(ISession session)
        {
            if (session == null) return;

            session.Send(new WiredConditionDataMessage
            {
                WiredData = WiredData
            });
        }

        public override IWiredConditionData WiredData => (IWiredConditionData)_wiredData;
    }
}
