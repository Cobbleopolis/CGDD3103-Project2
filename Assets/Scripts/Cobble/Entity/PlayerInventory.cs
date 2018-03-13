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

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //TODO use input manager
                UseItem(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) //TODO use input manager
                UseItem(1);
        }

        public void UseItem(int slotNum) {
            if (slotNum < 0 || slotNum >= _inventory.Length) return;
            var item = _inventory[slotNum];
            if (item == null) return;
            item.UseItem(gameObject);
        }
    }
}