using System.Collections;
using System.Collections.Generic;
using Cobble.Player;
using UnityEngine;

namespace Cobble.UI {
    public class InventoryUi : MonoBehaviour {
        public ItemInventory ItemInventory;

        [SerializeField] private GameObject _inventoryBody;

        [SerializeField] private GameObject _itemSlotPrefab;

        private ItemSlotUi[] _itemSlots;

        // Use this for initialization
        private void Start() {
            _itemSlots = new ItemSlotUi[ItemInventory.Size];

            for (var i = 0; i < _itemSlots.Length; i++) {
                var itemSlotUi = Instantiate(_itemSlotPrefab, _inventoryBody.transform).GetComponent<ItemSlotUi>();
                itemSlotUi.ItemInventory = ItemInventory;
                itemSlotUi.SlotNumber = i;
                itemSlotUi.gameObject.name = "Item Slot " + i;
                itemSlotUi.UpdateInfo();
                _itemSlots[i] = itemSlotUi;
            }
        }

        // Update is called once per frame
        private void Update() { }

        public void UpdateItemSlots() {
            if (_itemSlots == null) return;
            foreach (var itemSlot in _itemSlots)
                itemSlot.UpdateInfo();
        }
    }
}