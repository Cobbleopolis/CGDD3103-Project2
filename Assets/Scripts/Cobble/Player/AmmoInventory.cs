using UnityEngine;

namespace Cobble.Player {
    public class AmmoInventory : MonoBehaviour {

        [SerializeField]
        private int _currentAmmoCount;

        public int MaxAmmoCount = 40;

        public int CurrentAmmoCount {
            get { return _currentAmmoCount; }
        }

        public void AddAmmo(int amount) {
            _currentAmmoCount = Mathf.Clamp(_currentAmmoCount + amount, 0, MaxAmmoCount);
        }

        public void RemoveAmmo(int amount = 1) {
            _currentAmmoCount = Mathf.Clamp(_currentAmmoCount - amount, 0, MaxAmmoCount);
        }

        public bool IsEmpty() {
            return CurrentAmmoCount == 0;
        }
    
        public bool IsFull() {
            return CurrentAmmoCount == MaxAmmoCount;
        }
    }
}