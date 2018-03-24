using Cobble.Player;
using Cobble.UI;
using UnityEngine;

namespace Cobble.Projectile {
    public class GunProjectileSpawn : MonoBehaviour {
        public AmmoInventory AmmoInventory;

        public Vector3 ProjectileSpawnForce = new Vector3(0f, 0f, 50f);

        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private PlayerScore _playerScore;

        private void Start() {
            if (!AmmoInventory)
                AmmoInventory = GetComponentInParent<AmmoInventory>();
            if (!_playerScore)
                _playerScore = FindObjectOfType<PlayerScore>();
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
            var bullet = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletBehavior>().PlayerScore = _playerScore;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(ProjectileSpawnForce, ForceMode.VelocityChange);
        }
    }
}