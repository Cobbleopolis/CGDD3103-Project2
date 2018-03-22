using Cobble.Entity;
using UnityEngine;

namespace Cobble.UI {
    public class ArmorBarUi : MonoBehaviour {
        public ArmoredLivingEntity ArmoredLivingEntity;

        private void Update() {
            var scale = transform.localScale;
            scale.x = ArmoredLivingEntity.GetArmorPercent();
            transform.localScale = scale;
        }
    }
}