using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AiAction : MonoBehaviour {

        protected NavMeshAgent NavMeshAgent;

        public bool IsAiActive;

        protected virtual void Start() {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Activate() {
            if (IsAiActive) return;
            IsAiActive = true;
            OnActivate();
        }

        public void Deactivte() {
            if (!IsAiActive) return;
            IsAiActive = false;
            OnDeactivate();
        }
        

        protected virtual void OnActivate() {
            
        }

        protected virtual void OnDeactivate() {
            
        }

        public abstract void Call();

//        protected abstract bool ShouldBeEnabled();
    }
}