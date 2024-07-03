using System.Collections.Generic;
using System.Text.Json.Serialization;
using Turbo.Core.Game.Rooms.Object;
using Turbo.Core.Game;
using Turbo.Core.Packets.Messages;
using TurboWiredPlugin.Core;
using TurboWiredPlugin.Core.Data;
using TurboWiredPlugin.Packets.Incoming.Wired;

namespace TurboWiredPlugin.Logic.Furniture.Wired.Data
{
    public class WiredDataBase : IWiredData
    {
        [JsonIgnore]
        public int Id { get; protected set; }

        [JsonIgnore]
        public int SpriteId { get; protected set; }

        [JsonIgnore]
        public int WiredType { get; protected set; }

        [JsonIgnore]
        public bool SelectionEnabled { get; protected set; }

        [JsonIgnore]
        public int SelectionLimit { get; protected set; }

        [JsonPropertyName("Ids")]
        public IList<int> SelectionIds { get; set; }
        [JsonPropertyName("String")]
        public string StringParameter { get; set; }
        [JsonPropertyName("Ints")]
        public IList<int> IntParameters { get; set; }

        public WiredDataBase()
        {
            SelectionLimit = TurboWiredSettings.WiredFurniSelectionLimit;
            SelectionIds = new List<int>();
            StringParameter = "";
            IntParameters = new List<int>();
        }

        public virtual bool SetFromMessage(IMessageEvent update)
        {
            if (update is UpdateWired wiredUpdate)
            {
                SelectionIds = wiredUpdate.SelectedItemIds;
                StringParameter = wiredUpdate.StringParam;
                IntParameters = wiredUpdate.IntegerParams;

                return true;
            }

            return false;
        }

        public virtual bool SetRoomObject(IRoomObjectFloor roomObject)
        {
            if (roomObject.Logic is not IFurnitureWiredLogic wiredLogic) return false;

            Id = roomObject.Id;
            SpriteId = wiredLogic.FurnitureDefinition.SpriteId;
            WiredType = wiredLogic.WiredKey;

            return true;
        }
    }
}
