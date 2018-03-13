using System.Collections;
using Cobble.Core;
using UnityEngine;

namespace Cobble.Items {
    public class ItemSpawner : MonoBehaviour {
        public GameObject ItemPrefab;

        public float ItemRespawnTime = 5.0f;

        public bool StartWithItem = true;

        [SerializeField] private Transform _itemContainer;

        [SerializeField] private Renderer _emmitterRenderer;

        private GameObject _itemGameObject;

        private void Start() {
            _itemGameObject = Instantiate(ItemPrefab, _itemContainer);
            _itemGameObject.SetActive(StartWithItem);
            if (!StartWithItem)
                StartCoroutine(DelayItemSpawn(ItemRespawnTime));
        }

        public void ItemTaken() {
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