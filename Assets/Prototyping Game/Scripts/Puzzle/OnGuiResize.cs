using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	public class OnGuiResize : MonoBehaviour {

		int ScreenHeight;
		int ScreenWidth;

		// Use this for initialization
		void Start() {
			ScreenHeight = Screen.height;
			ScreenWidth = Screen.width;
			Debug.Log("H: " + Screen.height + " - W: " + Screen.width);
		}

		// Update is called once per frame
		void Update() {
			if (ScreenHeight != Screen.height || ScreenWidth != Screen.width)
			{
				Debug.Log("Change Resolution");
				//
				RectTransform BoardContainer = GameObject.Find("BoardContainer").GetComponent<RectTransform>();
				//RectTransform BoardContainer =  .Find("BoardContainer").GetComponent<RectTransform>();
				//
				Debug.Log(Screen.width - 20);
				Debug.Log((Screen.width - 20) / 300);
				float scale = (Screen.width - 20) / 300;

				Debug.Log(scale);
				BoardContainer.localScale = new Vector2((Screen.width - 20f) / 300f, (Screen.width - 20f) / 300f);
				BoardContainer.localPosition = new Vector2((Screen.width / 2) - 10, 150f);
				//BoardContainer.rect = new Rect(new Vector2(Screen.width - 20, BoardContainer.rect.y), new Vector2(BoardContainer.rect.width, BoardContainer.rect.height));

				ScreenHeight = Screen.height;
				ScreenWidth = Screen.width;
			}
		}
	}
}
