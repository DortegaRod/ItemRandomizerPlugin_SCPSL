using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using ItemRandomizerPlugin_SCPSL.RoomPoints;
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


        public void OnItemDropped(DroppingItemEventArgs ev) {
            if (ev.Player.CurrentRoom.Type == Scp173RoomType && ev.Item.Type == ItemType.Coin && IsNearDropLocation(ev.Player)) {

                var itemTypes = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
                itemTypes.Remove(ItemType.MicroHID);
                var randomItemType = itemTypes[Random.Next(itemTypes.Count)];

                if (ev.Item?.Base != null) {
                    ev.Player.Inventory.ServerRemoveItem(ev.Item.Base.ItemSerial, ev.Item.Base.PickupDropModel);

                    if (Random.Next(100) < 50) {
                        ev.Player.AddItem(randomItemType);
                        Log.Info($"Item {randomItemType} added");
                    }

                    else {
                        Log.Info("Item destroyed");
                    }
                }
            }
        }

        private List AddAllowedItems(List itemTypes) {
            List allowedItems = itemTypes.GetRange(1, 16);
            allowedItems.AddRange(18, 19);
            allowedItems.AddRange(20, 21);
            allowedItems.AddRange(25, 26);
            allowedItems.AddRange(30, 38);
            allowedItems.AddRange(42, 46);
            allowedItems.AddRange(48, 49);
            allowedItems.AddRange(51, 54);
            allowedItems.Add(ItemType.KeycardChaosInsurgency)
        }

        public void CoinSpawn() {
           
            foreach (var player in Player.List) {
                player.Inventory.ServerAddItem(ItemType.Coin);
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

    }
}