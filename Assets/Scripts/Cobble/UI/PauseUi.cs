using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cobble.Core;
using Cobble.Lib;

namespace Cobble.UI {
	public class PauseUi : MonoBehaviour {

		private GuiManager _guiManager;

		// Use this for initialization
		void Start () {
			_guiManager = FindObjectOfType<GuiManager>();
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		public void OpenInventory() {
			_guiManager.Open(GuiScreen.Inventory);
		}

		public void ExitGame() {
			GameManager.ExitGame();
		}
	}
}

