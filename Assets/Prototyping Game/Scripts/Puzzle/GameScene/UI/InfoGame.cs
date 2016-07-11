using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoGame : MonoBehaviour {

	public Image InfoImageSelect;

	// Use this for initialization
	void Start () {
		setImageSelect();
	}

	public void OnFechar()
	{
		gameObject.SetActive(false);
	}

	private void setImageSelect()
	{
		InfoImageSelect.sprite = Sprite.Create(GameManager.instance.ImageSelect, GameManager.instance.ImageCropRect, new Vector2(0, 0));
	}
}
