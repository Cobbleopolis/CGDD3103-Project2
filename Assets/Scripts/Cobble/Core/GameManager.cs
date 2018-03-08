using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.Core {
	[DisallowMultipleComponent]
	public class GameManager : MonoBehaviour {

		public static bool IsPaused;

		private void Awake() {
			GuiManager.TrapMouse();
			ItemRegistry.RegisterItems();
		}

		private void Start() {

		}

		private void Update() { }

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