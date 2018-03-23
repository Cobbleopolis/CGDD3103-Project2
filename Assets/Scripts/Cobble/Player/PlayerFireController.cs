using Cobble.Projectile;
using UnityEngine;

namespace Cobble.Player {
    public class PlayerFireController : MonoBehaviour {

        [SerializeField]
        private GunProjectileSpawn _gunProjectileSpawn;

        private void Update() {
            if (!Input.GetButtonDown("Fire1")) return;

            if (!_gunProjectileSpawn) {
                Debug.LogError("Gun Projectile Span no set!");
                return;
            }

            _gunProjectileSpawn.Fire();
        }
    }
}