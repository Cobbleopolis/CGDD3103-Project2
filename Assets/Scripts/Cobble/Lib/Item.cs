using Cobble.Core;
using UnityEngine;

namespace Cobble.Lib {
    public abstract class Item {
        
        public abstract string ItemId { get; }

        public abstract string Name { get; }

        protected abstract string SpritePath { get; }

        private Sprite _itemSprite;

        public Sprite ItemSprite {
            get {
                if (!_itemSprite)
                    _itemSprite = Resources.Load<Sprite>(SpritePath);
                return _itemSprite;
            }
        }

        public abstract void UseItem(GameObject usingGameObject);
    }
}