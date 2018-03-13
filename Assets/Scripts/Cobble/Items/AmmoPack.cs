using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class AmmoPack : Item {
        private const string ItemIdValue = "ammoPack";
        private const string NameValue = "Ammo Pack";

        public override string ItemId {
            get { return ItemIdValue; }
        }

        public override string Name {
            get { return NameValue; }
        }

        public override void UseItem(GameObject usingGameObject) {
            Debug.Log("Using Ammo Pack"); //TODO actually make this do something
        }
    }
}