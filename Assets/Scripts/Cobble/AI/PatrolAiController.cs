using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PatrolAi))]
    [RequireComponent(typeof(FollowAi))]
    public class PatrolAiController : AiController {
        public Transform TargetTransform;

        [SerializeField] private Transform _headLocation;

        public float FollowDistance;

        private PatrolAi _patrolAi;

        private FollowAi _followAi;

        private bool _isNear;

        private void Start() {
            _patrolAi = GetComponent<PatrolAi>();
            _followAi = GetComponent<FollowAi>();
        }

        protected override AiAction GetCurrentState() {
            if (!_isNear) return _patrolAi;
            var dist = Vector3.Distance(_headLocation.position, TargetTransform.position);
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