using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PrototypingGame
{
	public class OnGuiResize : MonoBehaviour {

		int ScreenHeight;
		int ScreenWidth;

		// Use this for initialization
		void Start() {
			ScreenHeight = 0;
			ScreenWidth = 0;
		}

		// Update is called once per frame
		void Update() {
			if (ScreenHeight != Screen.height || ScreenWidth != Screen.width)
			{
				Debug.Log("Change Resolution");
				//
				RectTransform BoardContainer = GameObject.Find("UIBoardTransform").GetComponent<RectTransform>();
				float scale = (Screen.width - 20) / 300; // esse -20 é a borda da tela. do lado esquerdo e direito

				BoardContainer.localScale = new Vector2((Screen.width - 20f) / 300f, (Screen.width - 20f) / 300f);// esse -20 é a borda da tela. do lado esquerdo e direito
				BoardContainer.localPosition = new Vector2((Screen.width / 2) - 10, 250f); // esse -10 é a borda da tela. do lado esquerdo só

				ScreenHeight = Screen.height;
				ScreenWidth = Screen.width;
			}
		}
	}
}
