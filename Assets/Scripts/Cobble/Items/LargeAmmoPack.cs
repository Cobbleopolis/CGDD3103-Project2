using Cobble.Lib;
using Cobble.Player;
using UnityEngine;

namespace Cobble.Items {
    public class LargeAmmoPack : Item {
        private const string ItemIdValue = "largeAmmoPack";
        private const string NameValue = "<color=lime>Large Ammo Pack</color>";
        private const int ItemMaxStack = 1;
        private const string ItemSpritePath = "Images/Items/Large Ammo Pack";

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