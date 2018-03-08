using UnityEngine;

namespace Cobble.Entity {
    public class LivingEntity : MonoBehaviour {
        [Header("Health")] [Tooltip("The maximum health that the entity can have.")]
        public float MaxHealth = 100f;

        [Tooltip("The current health that the entity has.")] [SerializeField]
        private float _currentHealth = 100f;

        public float CurrentHealth {
            get { return _currentHealth; }
            private set { _currentHealth = value; }
        }

        public virtual void Damage(float amount) {
            CurrentHealth = Mathf.Max(CurrentHealth - Mathf.Abs(amount), 0);
        }

        public virtual void Heal(float amount) {
            CurrentHealth = Mathf.Min(CurrentHealth + Mathf.Abs(amount), MaxHealth);
        }

        public bool IsDead() {
            return CurrentHealth <= 0;
        }

        public float GetHealthPercent() {
            return CurrentHealth / MaxHealth;
        }
    }
}