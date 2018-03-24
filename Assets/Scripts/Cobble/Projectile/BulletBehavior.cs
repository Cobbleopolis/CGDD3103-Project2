using Cobble.Entity;
using UnityEngine;

namespace Cobble.Projectile {
    public class BulletBehavior : MonoBehaviour {

        public float MaxLifetime = 10f;

        public float DamageAmount = 10f;

        private void Start() {
            Destroy(gameObject, MaxLifetime);
        }

        private void OnCollisionEnter(Collision other) {
            var livingEntity = other.gameObject.GetComponent<LivingEntity>();
            if (livingEntity)
                livingEntity.Damage(DamageAmount);
            Destroy(gameObject);
        }
    }
}