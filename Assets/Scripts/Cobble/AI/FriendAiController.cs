using Cobble.Util;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    
    [RequireComponent(typeof(FollowAi))]
    [RequireComponent(typeof(AttackAi))]
    public class FriendAiController : AiController {

        public Transform PlayerTransform;

        public Transform[] Enemies;

        public float AttackDistance = 5f;
        
        private FollowAi _followAi;

        private AttackAi _attackAi;

        private NavMeshPath _navMeshPath;
        
        private void Start() {
            _followAi = GetComponent<FollowAi>();
            _attackAi = GetComponent<AttackAi>();
            _navMeshPath = new NavMeshPath();
        }

        protected override AiAction GetCurrentState() {
            foreach (var enemy in Enemies) {
                var pathFound = NavMesh.CalculatePath(enemy.position, PlayerTransform.position, NavMesh.AllAreas, _navMeshPath);
                if (!pathFound || NavUtils.GetPathLength(_navMeshPath) > AttackDistance) continue;
                _attackAi.TargetTransform = enemy;
                return _attackAi;
            }
            return _followAi;
        }
    }
}