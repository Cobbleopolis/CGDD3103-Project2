using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowAi : MonoBehaviour {

        public Transform FollowTarget;

        public float FollowDistance = 5f;

        private NavMeshAgent _navMeshAgent;
        
        private void Start() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            if (!FollowTarget) return;
            _navMeshAgent.isStopped = _navMeshAgent.remainingDistance <= FollowDistance;
            _navMeshAgent.SetDestination(FollowTarget.position);
        }
    }
}