using Cobble.Core;
using UnityEngine;

namespace Cobble.Lib {
    public abstract class Item {
        
        public abstract string ItemId { get; }

        public abstract string Name { get; }

        public abstract void UseItem(GameObject usingGameObject);
    }
}