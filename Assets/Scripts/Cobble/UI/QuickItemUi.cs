using Cobble.Core;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.UI {
    public class QuickItemUi : MonoBehaviour {
        
        private ItemSlotUi[] _itemSlots;

        [SerializeField]
        private GuiManager _guiManager;

        private void Start() {
            _itemSlots = GetComponentsInChildren<ItemSlotUi>();
            if (!_guiManager)
                _guiManager = FindObjectOfType<GuiManager>();
        }

        private void Update() {
            if (_guiManager.GetCurrentGuiScreen() != GuiScreen.None) return;
            if (Input.GetButtonDown("Quick Item 1"))
                UseQuickItem(0);
            if (Input.GetButtonDown("Quick Item 2"))
                UseQuickItem(1);
            if (Input.GetButtonDown("Quick Item 3"))
                UseQuickItem(2);
            if (Input.GetButtonDown("Quick Item 4"))
                UseQuickItem(3);
        }

        public void UpdateItemSlots() {
            if (_itemSlots == null) return;
            foreach (var itemSlot in _itemSlots)
                itemSlot.UpdateInfo();
        }

        private void UseQuickItem(int quickSlotNum) {
            if (quickSlotNum >= _itemSlots.Length || _itemSlots[quickSlotNum] == null) return;
            _itemSlots[quickSlotNum].UseItem();
        }

        public void SetQuickSlotInventoryNumber(int quickSlotIndex, int inventorySlotIndex) {
            var itemSlot = _itemSlots[quickSlotIndex];
            if (!itemSlot) return;
            itemSlot.SlotNumber = inventorySlotIndex;
            itemSlot.UpdateInfo();
        }
        
    }
}