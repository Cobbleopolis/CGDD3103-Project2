using UnityEngine;

namespace Cobble.UI {
    public class QuickItemUi : MonoBehaviour {
        
        private ItemSlotUi[] _itemSlots;

        private void Start() {
            _itemSlots = GetComponentsInChildren<ItemSlotUi>();
        }

        public void UpdateItemSlots() {
            if (_itemSlots == null) return;
            foreach (var itemSlot in _itemSlots)
                itemSlot.UpdateInfo();
        }
        
    }
}