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
        
        // Use this for initialization
        private void Start() {
            if (!PlayerInventory)
                PlayerInventory = FindObjectOfType<PlayerInventory>();
        }

        // Update is called once per frame
        private void Update() { }

        public void UpdateItemSlots() {
            var existingItemSlots = GetComponentsInChildren<ItemSlotUi>();
            for (var i = 0; i < PlayerInventory.Size; i++) {
                var itemStack = PlayerInventory.GetSlot(i);
                if (itemStack == null || itemStack.IsEmpty) continue;
                ItemSlotUi itemSlotUi;
                if (i < existingItemSlots.Length) {
                    itemSlotUi = existingItemSlots[i];
                } else {
                    itemSlotUi = Instantiate(_itemSlotPrefab, _inventoryBody.transform).GetComponent<ItemSlotUi>();
                    itemSlotUi.SlotNumber = i;
                }
                
                if (!itemSlotUi.PlayerInventory)
                    itemSlotUi.PlayerInventory = PlayerInventory;
                itemSlotUi.UpdateInfo();
            }
        }
    }
}