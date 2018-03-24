using Cobble.Util;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(IdleAi))]
    [RequireComponent(typeof(AttackAi))]
    public class IdleAiController : AiController {
        
        public Transform TargetTransform;

        public float AttackDistance = 10f;

        private IdleAi _idleAi;

        private AttackAi _attackAi;
        
        private bool _isNear;
        
        private NavMeshAgent _navMeshAgent;
        
        private NavMeshPath _pathToTarget;
        
        private void Start() {
            _idleAi = GetComponent<IdleAi>();
            _attackAi = GetComponent<AttackAi>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _pathToTarget = new NavMeshPath();
        }

        protected override AiAction GetCurrentState() {
            if (!_isNear) return _idleAi;
            float dist;
            if (CurrentState == _idleAi) {
                var pathFound = NavMesh.CalculatePath(transform.position, TargetTransform.position, NavMesh.AllAreas,
                    _pathToTarget);
                if (!pathFound)
                    return _idleAi;
                dist = NavUtils.GetPathLength(_pathToTarget);
            } else
                dist = _navMeshAgent.remainingDistance;
            
            if (dist <= AttackDistance)
                return _attackAi;
            return _idleAi;
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