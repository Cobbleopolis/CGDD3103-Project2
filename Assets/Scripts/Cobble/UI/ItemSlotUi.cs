using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    public class ItemSlotUi : MonoBehaviour {

        public PlayerInventory PlayerInventory;
        
        public int SlotNumber;

        private ItemStack _itemStack;

        [SerializeField]
        private Text _itemText;

        [SerializeField]
        private Image _itemImage;


        private void Start() {
            
        }

        public void UpdateInfo() {
            if (!PlayerInventory)
                PlayerInventory = FindObjectOfType<PlayerInventory>();
            _itemStack = PlayerInventory.GetSlot(SlotNumber);
            if (PlayerInventory.IsSlotEmpty(SlotNumber)) {
                _itemImage.enabled = false;
                _itemImage.overrideSprite = null;
                _itemText.text = "";
            } else {
                _itemImage.enabled = true;
                _itemImage.overrideSprite = _itemStack.Item.ItemSprite;
                _itemText.text = _itemStack.Item.MaxStack > 1 ? _itemStack.Amount.ToString() : "";
            }
        }

        public void UseItem() {
            PlayerInventory.UseItem(SlotNumber);
        }
    }
}