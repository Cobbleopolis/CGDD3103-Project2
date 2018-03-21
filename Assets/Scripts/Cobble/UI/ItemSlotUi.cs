using Cobble.Player;
using Cobble.Lib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cobble.UI {
    public class ItemSlotUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public PlayerInventory PlayerInventory;

        public int SlotNumber;

        private ItemStack _itemStack;

        [SerializeField] private Text _itemText;

        [SerializeField] private Image _itemImage;

        private TooltipUi _tooltip;

        private bool _isTooltipShown;

        private void Start() {
            _tooltip = FindObjectOfType<TooltipUi>();
        }

        public void UpdateInfo() {
            if (!PlayerInventory)
                PlayerInventory = FindObjectOfType<PlayerInventory>();
            _itemStack = PlayerInventory.GetSlot(SlotNumber);
            if (PlayerInventory.IsSlotEmpty(SlotNumber)) {
                _itemImage.enabled = false;
                _itemImage.overrideSprite = null;
                _itemText.text = "";
                if (!_isTooltipShown || !_tooltip) return;
                _isTooltipShown = false;
                _tooltip.Hide();
            } else {
                _itemImage.enabled = true;
                _itemImage.overrideSprite = _itemStack.Item.ItemSprite;
                _itemText.text = _itemStack.Item.MaxStack > 1 ? _itemStack.Amount.ToString() : "";
                if (_isTooltipShown)
                    SetTooltipText();
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (_tooltip == null || PlayerInventory.IsSlotEmpty(SlotNumber)) return;
            _isTooltipShown = true;
            _tooltip.Show();
            SetTooltipText();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _tooltip.Hide();
            _isTooltipShown = false;
        }

        private void SetTooltipText() {
            if (!_tooltip) return;
            _tooltip.SetText((_itemStack == null || _itemStack.Item == null) ? "" : _itemStack.Item.Name);
        }

        public void UseItem() {
            PlayerInventory.UseItem(SlotNumber);
        }
    }
}