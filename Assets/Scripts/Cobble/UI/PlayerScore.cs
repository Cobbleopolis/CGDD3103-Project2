using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    
    [RequireComponent(typeof(Text))]
    [DisallowMultipleComponent]
    public class PlayerScore : MonoBehaviour {

        private int _score;

        private Text _text;

        private void Start() {
            _text = GetComponent<Text>();
        }

        public void AddScore(int points) {
            _score += points;
            _text.text = _score.ToString();
        }

        public void SubtractScore(int points) {
            _score -= points;
            _text.text = _score.ToString();
        }

    }
}