using System.Text.Json;
using Turbo.Core.Networking.Game.Clients;
using TurboWiredPlugin.Core.Data;
using TurboWiredPlugin.Logic.Furniture.Wired.Data.Types;
using TurboWiredPlugin.Packets.Outgoing.Wired;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Actions
{
    public class FurnitureWiredActionLogic : FurnitureWiredLogic
    {
        public override IWiredData CreateWiredDataFromJson(string jsonString = null)
        {
            if (jsonString == null) return new WiredActionData();

            return JsonSerializer.Deserialize<WiredActionData>(jsonString);
        }

        public override void SendConfigToSession(ISession session)
        {
            if (session == null) return;

            session.Send(new WiredEffectDataMessage
            {
                WiredData = WiredData
            });
        }

        public override IWiredActionData WiredData => (IWiredActionData)_wiredData;
    }
}
