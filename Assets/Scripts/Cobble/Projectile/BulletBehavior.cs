using Cobble.Entity;
using Cobble.UI;
using UnityEngine;

namespace Cobble.Projectile {
    public class BulletBehavior : MonoBehaviour {

        public float MaxLifetime = 10f;

        public float DamageAmount = 10f;

        public int EnemyPointsWorth = 20;

        public PlayerScore PlayerScore;

        private void Start() {
            Destroy(gameObject, MaxLifetime);
        }

        private void OnCollisionEnter(Collision other) {
            var livingEntity = other.gameObject.GetComponent<LivingEntity>();
            if (livingEntity)
                livingEntity.Damage(DamageAmount);
            if (PlayerScore && other.gameObject.CompareTag("Enemy"))
                PlayerScore.AddScore(EnemyPointsWorth);
            Destroy(gameObject);
        }
    }
}