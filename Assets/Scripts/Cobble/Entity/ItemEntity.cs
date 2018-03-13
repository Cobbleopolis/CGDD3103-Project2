using Cobble.Items;
using UnityEngine;

namespace Cobble.Entity {
    public class ItemEntity : MonoBehaviour {

        public string ItemId;
        
        private ItemSpawner _itemSpawner;

        private void Start() {
            _itemSpawner = GetComponentInParent<ItemSpawner>();
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            
            var playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            if (playerInventory)
                playerInventory.AddItem(ItemId);
            
            if (_itemSpawner)
                _itemSpawner.ItemTaken();
            else
                Destroy(gameObject);
        }
    }
}