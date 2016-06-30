using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectImage : MonoBehaviour
{
	private int webformatWidth;
	private int webformatHeight;
	private string webformatURL;
	private Sprite imageSprite;

	private SelectImage mSelectImages;

	public int WebformatWidth
	{
		get { return webformatWidth; }
		set { webformatWidth = value; }
	}

	public int WebformatHeight
	{
		get { return webformatHeight; }
		set { webformatHeight = value; }
	}

	public string WebformatURL
	{
		get { return webformatURL; }
		set { webformatURL = value; }
	}

	public Sprite ImageSprite
	{
		get { return imageSprite; }
		set { imageSprite = value; }
	}


	public void Start() {
		//guarda referencia do select images
		mSelectImages = GameObject.Find("Select Images").GetComponent<SelectImage>();
	}

	public void OnPointerDown(PointerEventData data)
	{
		Debug.Log("OnPointerDown");
		mSelectImages.SetImageChoice(gameObject.GetComponent<ObjectImage>());
	}

}