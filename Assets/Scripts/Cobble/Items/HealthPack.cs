using Cobble.Lib;

namespace Cobble.Items {
    public class HealthPack : Item {
        private const string ItemIdValue = "healthPack";
        private const string NameValue = "Health Pack";
        private const string PrefabPathValue = "Prefabs/Items/Health Pack";

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