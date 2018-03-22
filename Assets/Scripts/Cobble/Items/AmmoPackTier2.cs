using Cobble.Lib;
using Cobble.Player;
using UnityEngine;

namespace Cobble.Items {
    public class AmmoPackTier2 : Item {
        private const string ItemIdValue = "ammoPackTier2";
        private const string NameValue = "<color=lime>Tier 2 Ammo Pack</color>";
        private const int ItemMaxStack = 1;
        private const string ItemSpritePath = "Images/Items/Ammo Pack Tier 2";

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
            var ammoInv = usingGameObject.GetComponent<PlayerAmmoInventory>();
            if (ammoInv)
                ammoInv.AddAmmo(20);
        }
    }
}