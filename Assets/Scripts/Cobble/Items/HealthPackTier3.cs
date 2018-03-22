using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class HealthPackTier3 : Item {
        private const string ItemIdValue = "healthPackTier3";
        private const string NameValue = "<color=magenta>Tier 3 Health Pack</color>";
        private const int ItemMaxStack = 2;
        private const string ItemSpritePath = "Images/Items/Health Pack Tier 3";

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
                livingEntity.Heal(60.0f);
        }
    }
}