using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class HealthPackTier2 : Item {
        private const string ItemIdValue = "healthPackTier2";
        private const string NameValue = "<color=lime>Tier 2 Health Pack</color>";
        private const int ItemMaxStack = 3;
        private const string ItemSpritePath = "Images/Items/Health Pack Tier 2";

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
                livingEntity.Heal(40.0f);
        }
    }
}