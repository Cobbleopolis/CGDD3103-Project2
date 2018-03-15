using System;
using System.Linq;
using Cobble.Core;
using Cobble.Lib;
using Cobble.UI;
using UnityEngine;

namespace Cobble.Entity {
    [DisallowMultipleComponent]
    public class PlayerInventory : MonoBehaviour {

        [SerializeField]
        private InventoryUi _inventoryUi;
        
        private ItemStack[] _inventory = new ItemStack[12];

        private bool _isUiDirty;

        public int Size {
            get { return _inventory.Length; }
        }

        public ItemStack GetSlot(int slotNum) {
            return _inventory[slotNum];
        }

        public bool IsSlotEmpty(int slotNum) {
            var itemStack = GetSlot(slotNum);
            return itemStack == null || itemStack.IsEmpty;
        }

        public void AddItem(Item item) {
            for (var i = 0; i < _inventory.Length; i++)
                if (_inventory[i] == null) {
                    _inventory[i] = new ItemStack(item, 1);
                    _isUiDirty = true;
                    break;
                } else if (_inventory[i].Item == item && _inventory[i].Amount < item.MaxStack) {
                    _inventory[i].Amount++;
                    _isUiDirty = true;
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

        private void LateUpdate() {
            UpdateDirtyUi();
        }

        public void UseItem(int slotNum) {
            if (slotNum < 0 || slotNum >= _inventory.Length) return;
            var itemStack = _inventory[slotNum];
            if (itemStack == null || itemStack.Item == null) return;
            _isUiDirty = true;
            itemStack.Item.UseItem(gameObject);
            itemStack.Amount--;
            if (!itemStack.IsEmpty) return;
            _inventory[slotNum] = null;
            _inventory = _inventory.OrderBy(slot => slot == null || slot.IsEmpty).ToArray();
        }

        private void UpdateDirtyUi() {
            if (!_isUiDirty) return;
            _inventoryUi.UpdateItemSlots();
            _isUiDirty = false;
        }
    }
}