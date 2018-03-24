using System.Collections;
using Cobble.Entity;
using Cobble.UI;
using UnityEngine;

namespace Cobble.Player {
    
    [RequireComponent(typeof(LivingEntity))]
    public class PlayerHealthScoreChecker : MonoBehaviour {

        public int DecrementAmount = 10;

        public float DecrementInterval = 1f;

        [SerializeField]
        private LivingEntity _livingEntity;

        [SerializeField]
        private PlayerScore _playerScore;

        private bool _wasDead;

        private void Start() {
            if (!_livingEntity)
                _livingEntity = GetComponent<LivingEntity>();
            if (!_playerScore)
                _playerScore = FindObjectOfType<PlayerScore>();
            _wasDead = _livingEntity.IsDead();
        }

        private void Update() {
            if (!_wasDead && _livingEntity.IsDead())
                StartCoroutine("DecreaseScore");
            _wasDead = _livingEntity.IsDead();
        }

        private IEnumerator DecreaseScore() {
            while (_livingEntity.IsDead()) {
                _playerScore.SubtractScore(DecrementAmount);    
                yield return new WaitForSeconds(DecrementInterval);
            }
        }
    }
}