using UnityEngine;

namespace Cobble.Entity {
    public class ArmoredLivingEntity : LivingEntity {
        
        public float MaxArmor = 40;
        
        [SerializeField]
        private float _currentArmor;

        public float CurrentArmor {
            get { return _currentArmor; }
            private set { _currentArmor = value; }
        }

        public override void Damage(float amount) {
            base.Damage(Mathf.Max(amount - _currentArmor, 0));
            _currentArmor = Mathf.Clamp(_currentArmor - amount, 0, MaxArmor);
        }

        public void SetArmor(float amount) {
            _currentArmor = Mathf.Clamp(amount, 0, MaxArmor);
        }

        public float GetArmorPercent() {
            return _currentArmor / MaxArmor;
        }
    }
}