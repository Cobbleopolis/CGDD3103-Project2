using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class SmallHealthPack : Item {
        private const string ItemIdValue = "smallHealthPack";
        private const string NameValue = "Small Health Pack";
        private const int ItemMaxStack = 3;
        private const string ItemSpritePath = "Images/Items/Small Health";

        public override string ItemId {
            get { return ItemIdValue; }
        }

        public override string Name {
            get { return NameValue; }
        }
        
        public override int MaxStack {
            get { return ItemMaxStack; }
        }
        
        protected override string SpritePath {
            get { return ItemSpritePath; }
        }

        public override void UseItem(GameObject usingGameObject) {
            var livingEntity = usingGameObject.GetComponent<LivingEntity>();
            if (livingEntity)
                livingEntity.Heal(20.0f);
        }
    }
}