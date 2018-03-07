using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cobble.Core {
	[DisallowMultipleComponent]
	public class GameManager : MonoBehaviour {

		public static bool IsPaused;

		public bool IsPaused1;

		private void Awake() {
			GuiManager.TrapMouse();
		}

		private void Start() { }

		private void Update() {
			IsPaused1 = IsPaused;
		}

		public static void PauseGame() {
			IsPaused = true;
			Time.timeScale = 0;
		}

		public static void UnpauseGame() {
			IsPaused = false;
			Time.timeScale = 1;
		}

		public static void ExitGame() {
			Application.Quit();
		}
	}
}