using Cobble.Core;
using UnityEngine;

namespace Cobble.Lib {
    public abstract class Item {

        private GameObject _prefab;
        
        public abstract string ItemId { get; }

        public abstract string Name { get; }
        
        public abstract string PrefabPath { get; }

        public GameObject GetPrefabGameObject() {
            if (!_prefab)
                _prefab = Resources.Load(PrefabPath, typeof(GameObject)) as GameObject;

            return _prefab;
        }
    }
}