using UnityEngine;
using System.Collections;

public class BoardResponsive : MonoBehaviour {

	int ScreenWidth;
	int ScreenHeight;

	void Start()
	{		
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
	}

	void Update()
	{
		if (ScreenWidth != Screen.width || ScreenHeight != Screen.height)
		{
			Debug.Log("Resolution Changed");
			transform.localScale = new Vector3(1, 1, 1);

			Transform t = GameObject.Find("BoardTransform").GetComponent<Transform>();
			SpriteRenderer sr = GameObject.Find("BoardTransform").GetComponent<SpriteRenderer>();
			float width = sr.sprite.bounds.size.x;
			float height = sr.sprite.bounds.size.y;

			double worldScreenHeight = Camera.main.orthographicSize * 2.0;
			double worldScreenWidth = worldScreenHeight / (Screen.height * Screen.width);

			t.localScale = new Vector2((float) worldScreenWidth /width, (float) worldScreenHeight /height );

			ScreenWidth = Screen.width;
			ScreenHeight = Screen.height;

			//=======================================================

			float height = Camera.main.orthographicSize * 2;
			float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

			Sprite s = spriteRenderer.sprite;
			float unitWidth = s.textureRect.width / s.pixelsPerUnit;
			float unitHeight = s.textureRect.height / s.pixelsPerUnit;

			spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
		}
	}

}