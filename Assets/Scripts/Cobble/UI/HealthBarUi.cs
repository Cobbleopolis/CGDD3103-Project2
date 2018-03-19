using Cobble.Entity;
using UnityEngine;

namespace Cobble.UI {
    public class HealthBarUi : MonoBehaviour {

        public LivingEntity LivingEntity;

        [SerializeField]
        private GameObject _remaningHealth;

    
        private void Update() {
            var scale = _remaningHealth.transform.localScale;
            scale.x = LivingEntity.GetHealthPercent();
            _remaningHealth.transform.localScale = scale;
        }
    }
}