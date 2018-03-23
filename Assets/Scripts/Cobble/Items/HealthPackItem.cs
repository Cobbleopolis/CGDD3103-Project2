using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    
    [CreateAssetMenu (menuName = "Items/Health Pack Item")]
    public class HealthPackItem : Item {

        [Tooltip("The amount of health to heal when used by a Living Entity")]
        public float HealAmount;
        
        public override void UseItem(GameObject usingGameObject) {
            var livingEntity = usingGameObject.GetComponent<LivingEntity>();
            if (livingEntity)
                livingEntity.Heal(HealAmount);
        }
    }
}