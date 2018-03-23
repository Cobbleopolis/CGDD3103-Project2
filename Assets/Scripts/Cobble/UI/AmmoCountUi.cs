using Cobble.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    [RequireComponent(typeof(Text))]
    public class AmmoCountUi : MonoBehaviour {

        public AmmoInventory AmmoInventory;

        private Text _text;

        private void Start() {
            _text = GetComponent<Text>();
        }

        private void Update() {
            _text.text = AmmoInventory == null ? "N/A" : AmmoInventory.CurrentAmmoCount + "/" + AmmoInventory.MaxAmmoCount;
        }
    }
}