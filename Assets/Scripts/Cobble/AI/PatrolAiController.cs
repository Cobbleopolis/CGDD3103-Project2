using Cobble.Util;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PatrolAi))]
    [RequireComponent(typeof(FollowAi))]
    public class PatrolAiController : AiController {
        public Transform TargetTransform;

        public float FollowDistance;

        private PatrolAi _patrolAi;

        private FollowAi _followAi;

        private NavMeshAgent _navMeshAgent;

        private bool _isNear;

        private NavMeshPath _pathToTarget;

        private void Start() {
            _patrolAi = GetComponent<PatrolAi>();
            _followAi = GetComponent<FollowAi>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _pathToTarget = new NavMeshPath();
        }

        protected override AiAction GetCurrentState() {
            if (!_isNear) return _patrolAi;
            float dist;
            if (CurrentState == _patrolAi) {
                var pathFound = NavMesh.CalculatePath(transform.position, TargetTransform.position, NavMesh.AllAreas,
                    _pathToTarget);
                if (!pathFound)
                    return _patrolAi;
                dist = NavUtils.GetPathLenght(_pathToTarget);
            } else
                dist = _navMeshAgent.remainingDistance;
            
            if (dist <= FollowDistance)
                return _followAi;
            return _patrolAi;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.transform == TargetTransform)
                _isNear = true;
        }

        private void OnTriggerExit(Collider other) {
            if (other.transform == TargetTransform)
                _isNear = false;
        }
    }
}