using System.Collections;
using Cobble.Core;
using Cobble.Lib;
using Cobble.Player;
using UnityEngine;

namespace Cobble.Items {
    public class ItemSpawner : MonoBehaviour {
        public Item Item;

        public float ItemRespawnTime = 5.0f;

        public bool StartWithItem = true;

        [SerializeField] private Transform _itemContainer;

        [SerializeField] private Renderer _emmitterRenderer;

        private GameObject _itemGameObject;

        private void Start() {
            _itemGameObject = Instantiate(Item.ItemPrefab, _itemContainer);
            _itemGameObject.SetActive(StartWithItem);
            if (!StartWithItem)
                StartCoroutine(DelayItemSpawn(ItemRespawnTime));
        }

        public void ItemTaken(GameObject recivingGameObject) {
            var recivingItemInventory = recivingGameObject.GetComponent<ItemInventory>();
            if (recivingItemInventory)
                recivingItemInventory.AddItem(Item);
            _itemGameObject.SetActive(false);
            _emmitterRenderer.material.DisableKeyword("_EMISSION");
            StartCoroutine(DelayItemSpawn(ItemRespawnTime));
        }

        private IEnumerator DelayItemSpawn(float delay) {
            yield return new WaitForSeconds(delay);
            _emmitterRenderer.material.EnableKeyword("_EMISSION");
            _itemGameObject.SetActive(true);
        }
    }
}