using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using System;
using System.Linq;

namespace ItemRandomizerPlugin {
    public class PlayerHandler {
        private static readonly RoomType Scp173RoomType = RoomType.Lcz173;
        private static readonly Random Random = new Random();

        public void OnItemDropped(DroppingItemEventArgs ev) {
            if (ev.Player.CurrentRoom.Type == Scp173RoomType && ev.Item.Type == ItemType.Coin) {
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
        public void CoinSpawn() {
            Log.Info("noujef");
            foreach (var player in Player.List) {
                player.Inventory.ServerAddItem(ItemType.Coin);
            }
        }
    }
}