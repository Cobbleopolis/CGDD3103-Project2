using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavTarget : MonoBehaviour {

        public Transform Target;

        private NavMeshAgent _navMeshAgent;

        private void Start() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.SetDestination(Target.position);
        }
    }
}