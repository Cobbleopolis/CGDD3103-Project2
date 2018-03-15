﻿using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    public class ItemSlotUi : MonoBehaviour {

        public PlayerInventory PlayerInventory;
        
        public int SlotNumber;

        private ItemStack _itemStack;

        [SerializeField]
        private Image _itemImage;

        [SerializeField]
        private Text _itemText;

        private void Start() {
            
        }

        public void UpdateInfo() {
            if (!PlayerInventory)
                PlayerInventory = FindObjectOfType<PlayerInventory>();
            _itemStack = PlayerInventory.GetSlot(SlotNumber);
            if (PlayerInventory.IsSlotEmpty(SlotNumber)) {
                _itemText.text = "Item Name x 0";
                _itemImage.sprite = null;
                return;
            }
            _itemText.text = _itemStack.Item.Name;
            if (_itemStack.Item.MaxStack > 1)
                _itemText.text += " x " + _itemStack.Amount;
            _itemImage.sprite = _itemStack.Item.ItemSprite;
        }

        public void UseItem() {
            PlayerInventory.UseItem(SlotNumber);
        }
    }
}