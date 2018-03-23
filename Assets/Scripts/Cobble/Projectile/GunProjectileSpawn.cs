using Cobble.Player;
using UnityEngine;

namespace Cobble.Projectile {
    public class GunProjectileSpawn : MonoBehaviour {
        
        public AmmoInventory AmmoInventory;

        public Vector3 ProjectileSpawnForce = new Vector3(0f, 0f, 50f);

        [SerializeField] private GameObject _projectilePrefab;

        private void Start() {
            if (!AmmoInventory)
                AmmoInventory = GetComponentInParent<AmmoInventory>();
        }

        public void Fire() {
            if (AmmoInventory) {
                if (AmmoInventory.CurrentAmmoCount <= 0) return;
                AmmoInventory.RemoveAmmo();
                SpawnProjectile();
            } else
                SpawnProjectile();
        }

        private void SpawnProjectile() {
            var bulletRigidbody = Instantiate(_projectilePrefab, transform.position, transform.rotation).GetComponent<Rigidbody>();
            bulletRigidbody.AddRelativeForce(ProjectileSpawnForce, ForceMode.VelocityChange);
        }
    }
}