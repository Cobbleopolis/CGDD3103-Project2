using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class IdleAi : AiAction {

        public Transform IdleLocation;

        protected override void OnActivate() {
            if (!NavMeshAgent) return;
            NavMeshAgent.stoppingDistance = 0f;
            NavMeshAgent.SetDestination(IdleLocation.position);
        }

        protected override void OnDeactivate() {
            if (!NavMeshAgent) return;
            NavMeshAgent.ResetPath();
        }

        public override void Call() {
            
        }
    }
}