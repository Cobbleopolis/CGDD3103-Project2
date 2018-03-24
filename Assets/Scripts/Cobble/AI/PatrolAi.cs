using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class PatrolAi : AiAction {

        public Transform[] Checkpoints;

        [SerializeField]
        private float _distanceThreshold = 0.1f;
        
        private int _currentCheckpoint;

        protected override void OnActivate() {
            if (!NavMeshAgent) return;
            NavMeshAgent.stoppingDistance = 0f;
            NavMeshAgent.SetDestination(Checkpoints[_currentCheckpoint].position);
        }
        
        public override void Call() {
            if (!NavMeshAgent.pathPending && NavMeshAgent.remainingDistance <= _distanceThreshold + NavMeshAgent.stoppingDistance)
                GoToNextCheckPoint();
        }

        protected override void OnDeactivate() {
            if (NavMeshAgent)
                NavMeshAgent.ResetPath();
        }

        private void GoToNextCheckPoint() {
            _currentCheckpoint++;
            if (_currentCheckpoint >= Checkpoints.Length)
                _currentCheckpoint = 0;
            NavMeshAgent.SetDestination(Checkpoints[_currentCheckpoint].position);
        }
        
        
        
    }
}