using Cobble.Entity;
using UnityEngine;

namespace Cobble.Items {
    public class HealthPackBehaviour : MonoBehaviour {

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            other.gameObject.GetComponent<LivingEntity>().Heal(100.0f);
        }
    }
}