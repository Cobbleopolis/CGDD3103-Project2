using Cobble.Core;
using Cobble.Lib;
using Cobble.Projectile;
using UnityEngine;

namespace Cobble.Player {
    public class PlayerFireController : MonoBehaviour {

        [SerializeField]
        private GunProjectileSpawn _gunProjectileSpawn;

        [SerializeField] private GuiManager _guiManager;

        private void Start() {
            if (!_guiManager)
                _guiManager = FindObjectOfType<GuiManager>();
        }

        private void Update() {
            if (!Input.GetButtonDown("Fire1") || _guiManager.GetCurrentGuiScreen() != GuiScreen.None) return;

            if (!_gunProjectileSpawn) {
                Debug.LogError("Gun Projectile Span no set!");
                return;
            }

            _gunProjectileSpawn.Fire();
        }
    }
}