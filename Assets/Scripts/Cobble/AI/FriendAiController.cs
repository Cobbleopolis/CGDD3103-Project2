using UnityEngine;

namespace Cobble.AI {
    
    [RequireComponent(typeof(FollowAi))]
    public class FriendAiController : AiController {

        private FollowAi _followAi;
        
        private void Start() {
            _followAi = GetComponent<FollowAi>();
        }

        protected override AiAction GetCurrentState() {
            return _followAi;
        }
    }
}