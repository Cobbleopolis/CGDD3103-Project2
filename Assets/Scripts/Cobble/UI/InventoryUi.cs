using System.Collections;
using System.Collections.Generic;
using Cobble.Entity;
using UnityEngine;

namespace Cobble.UI {
    public class InventoryUi : MonoBehaviour {

        public PlayerInventory PlayerInventory;

        [SerializeField]
        private GameObject _inventoryBody;

        [SerializeField]
        private GameObject _itemSlotPrefab;

        private ItemSlotUi[] _itemSlots;
        
        // Use this for initialization
        private void Start() {
            if (!PlayerInventory)
                PlayerInventory = FindObjectOfType<PlayerInventory>();
            
            _itemSlots = new ItemSlotUi[PlayerInventory.Size];

            for (var i = 0; i < _itemSlots.Length; i++) {
                var itemSlotUi = Instantiate(_itemSlotPrefab, _inventoryBody.transform).GetComponent<ItemSlotUi>();
                itemSlotUi.PlayerInventory = PlayerInventory;
                itemSlotUi.SlotNumber = i;
                itemSlotUi.gameObject.SetActive(!PlayerInventory.IsSlotEmpty(i));
                itemSlotUi.gameObject.name = "Item Slot " + i;
                itemSlotUi.UpdateInfo();
                _itemSlots[i] = itemSlotUi;
            }
        }

        // Update is called once per frame
        private void Update() { }

        public void UpdateItemSlots() {
            if (_itemSlots == null) return;
            foreach (var itemSlot in _itemSlots) {
                itemSlot.gameObject.SetActive(!itemSlot.PlayerInventory.IsSlotEmpty(itemSlot.SlotNumber));
                itemSlot.UpdateInfo();
            }

        }
    }
}