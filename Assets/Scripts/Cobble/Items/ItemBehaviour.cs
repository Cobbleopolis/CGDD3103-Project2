using UnityEngine;

namespace Cobble.Items {
    public class ItemBehaviour : MonoBehaviour {
        private ItemSpawner _itemSpawner;

        private void Start() {
            _itemSpawner = GetComponentInParent<ItemSpawner>();
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            if (_itemSpawner)
                _itemSpawner.ItemTaken();
            else
                Destroy(gameObject);
        }
    }
}