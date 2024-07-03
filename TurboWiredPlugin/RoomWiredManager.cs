﻿using System;
using System.Collections.Generic;
using Turbo.Core.Game.Rooms.Mapping;
using Turbo.Core.Game.Rooms.Object.Logic;
using Turbo.Core.Game.Rooms.Object;
using Turbo.Core.Game.Rooms;
using TurboWiredPlugin.Logic.Furniture.Wired.Conditions;
using TurboWiredPlugin.Logic.Furniture.Wired.Actions;
using TurboWiredPlugin.Core;

namespace TurboWiredPlugin
{
    public class RoomWiredManager(
        IRoom _room,
        IRoomObjectLogicFactory _logicFactory) : IRoomWiredManager
        {
            public bool ProcessTriggers(string type, IWiredArguments wiredArguments = null)
            {
                if (!_logicFactory.Logics.TryGetValue(type, out Type logicType)) return false;

                var roomObjects = _room.RoomFurnitureManager.GetFloorRoomObjectsWithLogic(logicType);

                if (roomObjects.Count == 0) return false;

                bool didTrigger = false;

                foreach (var floorObject in roomObjects)
                {
                    if (!ProcessTrigger(floorObject, wiredArguments)) continue;

                    didTrigger = true;
                }

                return didTrigger;
            }

            public bool ProcessTrigger(IRoomObjectFloor roomObject, IWiredArguments wiredArguments = null)
            {
                if (roomObject.Logic is not IFurnitureWiredLogic wiredLogic) return false;

                var roomTile = wiredLogic.GetCurrentTile();

                if (roomTile == null) return false;

                if (!CanTrigger(roomObject, wiredArguments)) return false;

                if (!ProcessConditionsAtTile(roomTile, wiredArguments)) return false;

                ProcessActionsAtTile(roomTile, wiredArguments);

                return true;
            }

            public bool ProcessConditionsAtTile(IRoomTile roomTile, IWiredArguments wiredArguments = null)
            {
                var roomObjects = GetConditionsAtTile(roomTile);

                if (roomObjects.Count > 0)
                {
                    foreach (var roomObject in roomObjects)
                    {
                        if (!CanTrigger(roomObject, wiredArguments)) return false;
                    }
                }

                return true;
            }

            public bool ProcessActionsAtTile(IRoomTile roomTile, IWiredArguments wiredArguments = null)
            {
                var roomObjects = GetActionsAtTile(roomTile);

                if (roomObjects.Count > 0)
                {
                    foreach (var roomObject in roomObjects)
                    {
                        if (!CanTrigger(roomObject, wiredArguments)) return false;
                    }
                }

                return true;
            }

            public bool CanTrigger(IRoomObjectFloor roomObject, IWiredArguments wiredArguments = null)
            {
                if (roomObject.Logic is not IFurnitureWiredLogic wiredLogic) return false;

                if (!wiredLogic.CanTrigger(wiredArguments)) return false;

                wiredLogic.OnTriggered(wiredArguments);

                return true;
            }

            public IList<IRoomObjectFloor> GetActionsAtTile(IRoomTile roomTile)
            {
                var roomObjects = new List<IRoomObjectFloor>();

                if (roomTile != null && roomTile.Furniture.Count > 0)
                {
                    foreach (var floorObject in roomTile.Furniture)
                    {
                        if (floorObject.Logic is not FurnitureWiredActionLogic) continue;

                        roomObjects.Add(floorObject);
                    }
                }

                return roomObjects;
            }

            public IList<IRoomObjectFloor> GetConditionsAtTile(IRoomTile roomTile)
            {
                var roomObjects = new List<IRoomObjectFloor>();

                if (roomTile != null && roomTile.Furniture.Count > 0)
                {
                    foreach (var floorObject in roomTile.Furniture)
                    {
                        if (floorObject.Logic is not FurnitureWiredConditionLogic) continue;

                        roomObjects.Add(floorObject);
                    }
                }

                return roomObjects;
            }
    }
}
