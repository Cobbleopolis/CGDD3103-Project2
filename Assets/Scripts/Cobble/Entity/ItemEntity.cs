using Cobble.Items;
using Cobble.Player;
using Cobble.UnityEditor;
using UnityEngine;

namespace Cobble.Entity {
    public class ItemEntity : MonoBehaviour {
        
        private ItemSpawner _itemSpawner;

        private void Start() {
            _itemSpawner = GetComponentInParent<ItemSpawner>();
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            
            if (_itemSpawner)
                _itemSpawner.ItemTaken(other.gameObject);
            else
                Destroy(gameObject);
        }
    }
}