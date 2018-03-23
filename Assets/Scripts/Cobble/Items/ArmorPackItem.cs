using Cobble.Entity;
using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    
    [CreateAssetMenu (menuName = "Items/Armor Pack Item")]
    public class ArmorPackItem : Item {
        
        [Tooltip("The amount of armor to added when used by an ArmoredLivingEntity. If it is less than or equal to 0 then the ArmoredLivingEntity's armor is filled.")]
        public float ArmorAmount;
        
        public override void UseItem(GameObject usingGameObject) {
            var armoredLivingEntity = usingGameObject.GetComponent<ArmoredLivingEntity>();
            if (armoredLivingEntity)
                armoredLivingEntity.SetArmor(ArmorAmount <= 0 ? armoredLivingEntity.MaxArmor : ArmorAmount);
        }
    }
}