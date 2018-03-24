using Cobble.Core;
using Cobble.Player;
using Cobble.Lib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cobble.UI {
    public class ItemSlotUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public ItemInventory ItemInventory;

        public int SlotNumber;

        private ItemStack _itemStack;

        [SerializeField] private Text _itemText;

        [SerializeField] private Image _itemImage;

        private TooltipUi _tooltip;

        [SerializeField]
        private bool _isHovered;
        
        [SerializeField]
        private GuiManager _guiManager;

        private void Start() {
            _tooltip = FindObjectOfType<TooltipUi>();
            
            if (!_guiManager)
                _guiManager = FindObjectOfType<GuiManager>();
        }

        private void Update() {
            if (_guiManager.GetCurrentGuiScreen() != GuiScreen.Inventory || !_isHovered) return;
            if (Input.GetButtonDown("Quick Item 1"))
                ItemInventory.QuickItemUi.SetQuickSlotInventoryNumber(0, SlotNumber);
            if (Input.GetButtonDown("Quick Item 2"))
                ItemInventory.QuickItemUi.SetQuickSlotInventoryNumber(1, SlotNumber);
            if (Input.GetButtonDown("Quick Item 3"))
                ItemInventory.QuickItemUi.SetQuickSlotInventoryNumber(2, SlotNumber);
            if (Input.GetButtonDown("Quick Item 4"))
                ItemInventory.QuickItemUi.SetQuickSlotInventoryNumber(3, SlotNumber);
        }

        public void UpdateInfo() {
            if (!ItemInventory) {
                Debug.LogError("Slot Ui does not have a set ItemInventory field");
                return;
            }
            _itemStack = ItemInventory.GetSlot(SlotNumber);
            if (ItemInventory.IsSlotEmpty(SlotNumber)) {
                _itemImage.enabled = false;
                _itemImage.overrideSprite = null;
                _itemText.text = "";
                if (!_isHovered || !_tooltip) return;
                _isHovered = false;
                _tooltip.Hide();
            } else {
                _itemImage.enabled = true;
                _itemImage.overrideSprite = _itemStack.Item.ItemSprite;
                _itemText.text = _itemStack.Item.MaxStack > 1 ? _itemStack.Amount.ToString() : "";
                if (_isHovered)
                    SetTooltipText();
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (_tooltip == null || ItemInventory.IsSlotEmpty(SlotNumber)) return;
            _isHovered = true;
            _tooltip.Show();
            SetTooltipText();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _tooltip.Hide();
            _isHovered = false;
        }

        private void SetTooltipText() {
            if (!_tooltip) return;
            _tooltip.SetText((_itemStack == null || _itemStack.Item == null) ? "" : _itemStack.Item.Name);
        }

        public void UseItem() {
            ItemInventory.UseItem(SlotNumber);
        }
    }
}