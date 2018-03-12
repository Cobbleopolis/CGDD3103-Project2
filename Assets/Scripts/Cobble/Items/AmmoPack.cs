using Cobble.Lib;

namespace Cobble.Items {
    public class AmmoPack : Item {
        private const string ItemIdValue = "ammoPack";
        private const string NameValue = "Ammo Pack";
        private const string PrefabPathValue = "Prefabs/Items/Ammo Pack";

        public override string ItemId {
            get { return ItemIdValue; }
        }

        public override string Name {
            get { return NameValue; }
        }

        public override string PrefabPath {
            get { return PrefabPathValue; }
        }
    }
}