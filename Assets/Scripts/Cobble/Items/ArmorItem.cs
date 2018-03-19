using Cobble.Lib;
using UnityEngine;

namespace Cobble.Items {
    public class ArmorItem : Item {
        private const string ItemIdValue = "armor";
        private const string NameValue = "Armor";
        private const int ItemMaxStack = 2;
        private const string ItemSpritePath = "Images/Items/Armor Item";

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
            Debug.Log("Using Armor"); //TODO actually make this do something
        }
    }
}