using Microsoft.Extensions.Logging;
using Turbo.Core.Events;
using Turbo.Core.Game.Rooms.Object.Logic;
using Turbo.Core.Plugins;
using Turbo.Core.Packets;
using Turbo.Core.Networking.Game.Clients;
using Turbo.Core.Packets.Messages;
using TurboWiredPlugin.Core.Constants;
using TurboWiredPlugin.Logic.Furniture.Wired.Triggers;
using TurboWiredPlugin.Logic.Furniture.Wired.Conditions;
using TurboWiredPlugin.Core;
using TurboWiredPlugin.Packets.Incoming.Wired;
using Turbo.Events.Game.Rooms.Avatar;
using TurboWiredPlugin.Packets.Outgoing.Wired;

namespace TurboWiredPlugin
{
    public class TurboWiredPlugin : ITurboPlugin
    {
        private readonly ILogger<TurboWiredPlugin> _logger;
        private readonly ITurboEventHub _eventHub;
        private readonly IPacketMessageHub _messageHub;
        private readonly IRoomObjectLogicFactory _logicFactory;

        public string PluginName => "Turbo Wired Plugin";

        public string PluginAuthor => "Billsonnn";

        public TurboWiredPlugin(
            ILogger<TurboWiredPlugin> logger,
            ITurboEventHub eventHub,
            IPacketMessageHub messageHub,
            IRoomObjectLogicFactory logicFactory)
        {
            _logger = logger;
            _eventHub = eventHub;
            _messageHub = messageHub;
            _logicFactory = logicFactory;

            _messageHub.Subscribe<ApplySnapshotMessage>(this, OnApplySnapshotMessage);
            _messageHub.Subscribe<OpenWiredMessage>(this, OnOpenWiredMessage);
            _messageHub.Subscribe<UpdateActionMessage>(this, OnUpdateActionMessage);
            _messageHub.Subscribe<UpdateConditionMessage>(this, OnUpdateConditionMessage);
            _messageHub.Subscribe<UpdateTriggerMessage>(this, OnUpdateTriggerMessage);

            _eventHub.Subscribe<AvatarEnterRoomEvent>(this, OnAvatarEnterRoomEvent);
            _eventHub.Subscribe<AvatarEnterFloorFurnitureEvent>(this, OnAvatarEnterFloorFurnitureEvent);
            _eventHub.Subscribe<AvatarLeaveFloorFurnitureEvent>(this, OnAvatarLeaveFloorFurnitureEvent);
            _eventHub.Subscribe<AvatarInteractFloorFurnitureEvent>(this, OnAvatarInteractFloorFurnitureEvent);

            /*if (RoomObject.Logic is IFurnitureWiredLogic wiredLogic)
            {
                if (wiredLogic.WiredData != null)
                {
                    FurnitureEntity.WiredData = JsonSerializer.Serialize(wiredLogic.WiredData, wiredLogic.WiredData.GetType());
                }
            }*/
        }

        private void RegisterLogics()
        {
            #region Wired Triggers
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredTriggerEnterRoom, typeof(FurnitureWiredTriggerEnterRoomLogic));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredTriggerWalksOnFurni, typeof(FurnitureWiredTriggerWalksOnFurni));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredTriggerWalksOffFurni, typeof(FurnitureWiredTriggerWalksOffFurni));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredTriggerStateChanged, typeof(FurnitureWiredTriggerStateChangeLogic));
            #endregion

            #region Wired Conditions
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionActorIsWearingBadge, typeof(FurnitureWiredConditionActorIsWearingBadge));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionNotActorWearsBadge, typeof(FurnitureWiredConditionNotActorWearsBadge));

            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionHasStackedFurnis, typeof(FurnitureWiredConditionHasStackedFurnis));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionNotHasStackedFurnis, typeof(FurnitureWiredConditionNotHasStackedFurnis));

            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionTriggererOnFurni, typeof(FurnitureWiredConditionTriggererOnFurni));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionNotTriggererOnFurni, typeof(FurnitureWiredConditionNotTriggererOnFurni));

            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionFurniHasAvatars, typeof(FurnitureWiredConditionFurniHasAvatars));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionNotFurniHasAvatars, typeof(FurnitureWiredConditionNotFurniHasAvatars));

            _logicFactory.Logics.Add(WiredLogicType.FurnitrueWiredConditionUserCountIn, typeof(FurnitureWiredConditionUserCountIn));
            _logicFactory.Logics.Add(WiredLogicType.FurnitureWiredConditionNotUserCountIn, typeof(FurnitureWiredConditionNotUserCountIn));
            #endregion
        }

        protected virtual void OnApplySnapshotMessage(ApplySnapshotMessage message, ISession session)
        {
            if (session.Player == null) return;
        }

        protected virtual void OnOpenWiredMessage(OpenWiredMessage message, ISession session)
        {
            if (session.Player == null) return;
        }

        protected virtual void OnUpdateActionMessage(UpdateActionMessage message, ISession session)
        {
            UpdateWired(message, session);
        }

        protected virtual void OnUpdateConditionMessage(UpdateConditionMessage message, ISession session)
        {
            UpdateWired(message, session);
        }

        protected virtual void OnUpdateTriggerMessage(UpdateTriggerMessage message, ISession session)
        {
            UpdateWired(message, session);
        }

        protected virtual void UpdateWired(UpdateWired message, ISession session)
        {
            if (session.Player == null) return;

            var floorFurniture = session.Player.RoomObject?.Room?.RoomFurnitureManager?.GetFloorFurniture(message.ItemId);

            if (floorFurniture == null || floorFurniture.RoomObject == null || floorFurniture.RoomObject.Logic is not IFurnitureWiredLogic wiredLogic) return;

            var wiredData = wiredLogic.CreateWiredDataFromJson(null);

            if (wiredData == null) return;

            wiredData.SetFromMessage((IMessageEvent)message);

            if (wiredLogic.SaveWiredData(session.Player.RoomObject, wiredData))
            {
                session.Send(new WiredSavedMessage());
            }
        }

        public void OnAvatarEnterRoomEvent(AvatarEnterRoomEvent message)
        {
            _logger.LogInformation("avatar entered room");
            /*RoomWiredManager.ProcessTriggers(RoomObjectLogicType.FurnitureWiredTriggerEnterRoom, new WiredArguments
            {
                UserObject = roomObject
            });*/
        }

        public void OnAvatarEnterFloorFurnitureEvent(AvatarEnterFloorFurnitureEvent message)
        {
            /*RoomObject.Room.RoomWiredManager.ProcessTriggers(RoomObjectLogicType.FurnitureWiredTriggerWalksOnFurni, new WiredArguments
            {
                UserObject = avatar,
                FurnitureObject = RoomObject
            });*/
        }

        public void OnAvatarLeaveFloorFurnitureEvent(AvatarLeaveFloorFurnitureEvent message)
        {
            /*RoomObject.Room.RoomWiredManager.ProcessTriggers(RoomObjectLogicType.FurnitureWiredTriggerWalksOffFurni, new WiredArguments
            {
                UserObject = avatar,
                FurnitureObject = RoomObject
            });*/
        }

        public void OnAvatarInteractFloorFurnitureEvent(AvatarInteractFloorFurnitureEvent message)
        {
           /* RoomObject.Room.RoomWiredManager.ProcessTriggers(RoomObjectLogicType.FurnitureWiredTriggerStateChanged, new WiredArguments
            {
                UserObject = avatar,
                FurnitureObject = RoomObject
            });*/
        }
    }
}
