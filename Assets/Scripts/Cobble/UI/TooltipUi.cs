using Cobble.Core;
using Cobble.Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.UI {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class TooltipUi : MonoBehaviour {
        
        public Vector3 Offset = new Vector3(5f, 0f, 0f);

        private GuiManager _guiManager;

        private CanvasGroup _canvasGroup;

        private Text _tooltipText;

        public bool IsShown {
            get { return _canvasGroup.alpha > float.Epsilon; }
        }

        private void Start() {
            _guiManager = FindObjectOfType<GuiManager>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _tooltipText = GetComponentInChildren<Text>();
        }

        private void Update() {
            if (_guiManager == null || _guiManager.GetCurrentGuiScreen() == GuiScreen.None || !IsShown) return;
            transform.position = Input.mousePosition + Offset;
        }

        public void SetText(string tooltipText) {
            if (!_tooltipText) return;
            _tooltipText.text = tooltipText;
        }

        public void Show() {
            _canvasGroup.alpha = 1;
            transform.position = Input.mousePosition;
        }

        public void Hide() {
            _canvasGroup.alpha = 0;
        }
        
    }
}