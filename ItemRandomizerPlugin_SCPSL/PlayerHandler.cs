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
            if (ev.Player.CurrentRoom.Type == Scp173RoomType) {
                var itemTypes = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
                itemTypes.Remove(ItemType.MicroHID);
                var randomItemType = itemTypes[Random.Next(itemTypes.Count)];
                if (ev.Item?.Base != null) {
                    ev.Player.Inventory.ServerRemoveItem(ev.Item.Base.ItemSerial, ev.Item.Base.PickupDropModel);
                    if (Random.Next(10) < 5) {
                        ev.Player.AddItem(randomItemType);
                        Log.Info($"Item {randomItemType} added");
                    }
                    else {
                        Log.Info("Item destroyed");
                    }
                }
            }
        }
    }
}