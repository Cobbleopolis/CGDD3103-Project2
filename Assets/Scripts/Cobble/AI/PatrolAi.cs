using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class PatrolAi : MonoBehaviour {

        public Transform[] Checkpoints;

        private NavMeshAgent _navMeshAgent;

        private float _distanceThreshold = 0.1f;
        
        private int _currentCheckpoint;

        private void Start() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _currentCheckpoint = -1;
            GoToNextCheckPoint();
        }

        private void Update() {
            if (_navMeshAgent.remainingDistance <= _distanceThreshold)
                GoToNextCheckPoint();
        }

        private void GoToNextCheckPoint() {
            _currentCheckpoint++;
            if (_currentCheckpoint >= Checkpoints.Length)
                _currentCheckpoint = 0;
            _navMeshAgent.SetDestination(Checkpoints[_currentCheckpoint].position);
        }

    }
}