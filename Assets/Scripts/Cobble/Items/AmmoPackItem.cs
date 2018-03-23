using Cobble.Entity;
using Cobble.Lib;
using Cobble.Player;
using UnityEngine;

namespace Cobble.Items {
    
    [CreateAssetMenu (menuName = "Items/Ammo Pack Item")]
    public class AmmoPackItem : Item {

        [Tooltip("The amount of ammo added when used by a GameObject with an AmmoInventory.")]
        public int AmmoAmount;
        
        public override void UseItem(GameObject usingGameObject) {
            var ammoInv = usingGameObject.GetComponent<AmmoInventory>();
            if (ammoInv)
                ammoInv.AddAmmo(AmmoAmount);
        }
    }
}