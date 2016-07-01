using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectImage : MonoBehaviour, IPointerDownHandler
{
	private int webformatWidth;
	private int webformatHeight;
	private string webformatURL;
	private Texture2D imageTexture;

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

	public Texture2D ImageTexture
	{
		get { return imageTexture; }
		set { imageTexture = value; }
	}


	public void Start() {
		//guarda referencia do select images
		mSelectImages = GameObject.Find("Select Images").GetComponent<SelectImage>();
	}

	/// <summary>
	/// TODO: Fazer o tratamento de quando for imagem da net, ele baixar imagem grande
	/// </summary>
	/// <param name="data"></param>
	public void OnPointerDown(PointerEventData data)
	{
		Debug.Log("OnPointerDown");
		mSelectImages.SetImageChoice(gameObject);
	}

}