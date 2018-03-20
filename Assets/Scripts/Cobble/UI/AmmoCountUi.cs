using Cobble.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    [RequireComponent(typeof(Text))]
    public class AmmoCountUi : MonoBehaviour {

        public PlayerAmmoInventory PlayerAmmoInventory;

        private Text _text;

        private void Start() {
            _text = GetComponent<Text>();
        }

        private void Update() {
            _text.text = PlayerAmmoInventory == null ? "N/A" : PlayerAmmoInventory.CurrentAmmoCount + "/" + PlayerAmmoInventory.MaxAmmoCount;
        }
    }
}