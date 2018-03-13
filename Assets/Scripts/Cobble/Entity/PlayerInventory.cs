using Cobble.Core;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Entity {
    public class PlayerInventory : MonoBehaviour {
        private Item[] _inventory = new Item[16];

        public void AddItem(Item item) {
            for (var i = 0; i < _inventory.Length; i++)
                if (_inventory[i] == null) {
                    _inventory[i] = item;
                    break;
                }
        }

        public void AddItem(string itemId) {
            AddItem(ItemRegistry.GetItem(itemId));
        }
    }
}