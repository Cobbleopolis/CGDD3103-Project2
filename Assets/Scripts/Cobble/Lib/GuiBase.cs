using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cobble.Core;

namespace Cobble.Lib {
    public class GuiBase : MonoBehaviour {
        public GuiScreen GuiScreen;

        public bool PausesGame;

        public bool FreeMouse = true;

        private void Awake() { }

        // Use this for initialization
        private void Start() { }

        // Update is called once per frame
        private void Update() { }

        public void OnShow() {
            gameObject.SetActive(true);

            if (PausesGame)
                GameManager.PauseGame();
            else
                GameManager.UnpauseGame();

            if (FreeMouse)
                GuiManager.FreeMouse();
            else
                GuiManager.TrapMouse();
        }

        public void OnHide() {
            gameObject.SetActive(false);
        }
    }
}