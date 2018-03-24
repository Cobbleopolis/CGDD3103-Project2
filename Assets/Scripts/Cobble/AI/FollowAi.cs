using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowAi : AiAction {

        public Transform FollowTarget;

        public float FollowDistance;
        
//        private void OnDisable() {
//            if (!NavMeshAgent || !NavMeshAgent.isActiveAndEnabled || !NavMeshAgent.isOnNavMesh || !NavMeshAgent.hasPath) return;
//            NavMeshAgent.ResetPath();
//        }

        protected override void OnActivate() {
            if (!NavMeshAgent) return;
            NavMeshAgent.stoppingDistance = FollowDistance;
            NavMeshAgent.SetDestination(FollowTarget.position);
        }
        
        public override void Call() {
            if (!FollowTarget) return;
            NavMeshAgent.SetDestination(FollowTarget.position);
        }

        protected override void OnDeactivate() {
            if (NavMeshAgent)
                NavMeshAgent.ResetPath();
        }

    }
}