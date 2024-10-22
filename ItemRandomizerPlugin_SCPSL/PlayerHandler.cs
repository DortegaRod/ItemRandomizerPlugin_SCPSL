﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using ItemRandomizerPlugin_SCPSL.RoomPoints;
using PluginAPI.Core.Zones.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ItemRandomizerPlugin {
    public class PlayerHandler {
        private static readonly RoomType Scp173RoomType = RoomType.Lcz173;
        private static readonly Random Random = new Random();
        private static readonly Vector3 DropLocation = new Vector3(257.3519f, 13.14142f, 127.7134f);
        private static List<ItemType> allowedItems = AddAllowedItems();

        public void OnItemDropped(DroppingItemEventArgs ev) {
            if (ev.Player.CurrentRoom.Type == Scp173RoomType && ev.Item.Type == ItemType.Coin && IsNearDropLocation(ev.Player)) {
                var randomItemType = allowedItems[Random.Next(allowedItems.Count)];

                if (ev.Item?.Base != null) {
                    ev.Player.Inventory.ServerRemoveItem(ev.Item.Base.ItemSerial, ev.Item.Base.PickupDropModel);
                    ev.Player.AddItem(randomItemType);
                    Log.Info($"Item {randomItemType} added");
                }
            }
        }

        

        public void CoinSpawn() {
            foreach (var player in Player.List) {
                player.Inventory.ServerAddItem(ItemType.Coin);
            }
        }

        public void OnFlip(FlippingCoinEventArgs ev)
        {
            List<Player> bannedFromFlip = new List<Player>();
            if(ev.Player.CurrentRoom.Zone== ZoneType.LightContainment)
            {
                if (!ev.IsTails)
                {
                    if (bannedFromFlip.Contains(ev.Player))
                    {
                        ev.IsAllowed = false;
                    } else
                    {
                        ev.Player.Teleport(GetRandomLCZRoom());
                        bannedFromFlip.Add(ev.Player);
                    }
                }
            }
        }


        private bool IsNearDropLocation(Player player) {
 
            var RequiredDropLocation = new RoomPointObject(Room.Get(Scp173RoomType).GameObject.transform.TransformPoint(DropLocation.x, DropLocation.y, DropLocation.z));
          

            Log.Info(Vector3.Distance(player.Position,
                new Vector3(RequiredDropLocation.relativePosition.X, RequiredDropLocation.relativePosition.Y, RequiredDropLocation.relativePosition.Z)));

            if(Vector3.Distance(player.Position, 
                new Vector3(RequiredDropLocation.relativePosition.X, RequiredDropLocation.relativePosition.Y, RequiredDropLocation.relativePosition.Z)) <= 274.0f) {

                return true;

            }
            return false;
        }



        private RoomType GetRandomLCZRoom()
        {
            Random random = new Random();
            int index = random.Next(lczRooms.Count);
            return lczRooms[index];
        }

        private static List<ItemType> AddAllowedItems()
        {
            Log.Info("AllowedItems");
            var itemTypes = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
            var allowedItems = itemTypes.GetRange(1, 15);
            allowedItems.AddRange(itemTypes.GetRange(17, 2));
            allowedItems.AddRange(itemTypes.GetRange(25, 2));
            allowedItems.AddRange(itemTypes.GetRange(29, 7));
            allowedItems.AddRange(itemTypes.GetRange(42, 5));
            allowedItems.AddRange(itemTypes.GetRange(48, 2));
            allowedItems.AddRange(itemTypes.GetRange(51, 1));
            allowedItems.AddRange(itemTypes.GetRange(54, 1));
            Log.Info(allowedItems.Count);
            return allowedItems;
        }

        private List<RoomType> lczRooms = new List<RoomType>
        {
        RoomType.LczArmory,
        RoomType.LczCurve,
        RoomType.LczStraight,
        RoomType.Lcz914,
        RoomType.LczCrossing,
        RoomType.LczTCross,
        RoomType.LczCafe,
        RoomType.LczPlants,
        RoomType.LczToilets,
        RoomType.LczAirlock,
        RoomType.Lcz173,
        RoomType.LczClassDSpawn,
        RoomType.LczCheckpointB,
        RoomType.LczGlassBox,
        RoomType.LczCheckpointA,
        RoomType.Lcz330
        };
    }
}