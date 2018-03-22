using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class HealthPack : Item {
        private const string ItemIdValue = "healthPack";
        private const string NameValue = "Health Pack";
        private const int ItemMaxStack = 3;
        private const string ItemSpritePath = "Images/Items/Health Pack";

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