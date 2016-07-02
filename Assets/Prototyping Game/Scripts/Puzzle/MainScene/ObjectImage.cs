using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectImage : MonoBehaviour, IPointerDownHandler
{
	private int webformatWidth;
	private int webformatHeight;
	private string imageURL;
	private Texture2D imageTexture;

	private SelectImage mSelectImages;
	

	public string ImageURL
	{
		get { return imageURL; }
		set { imageURL = value; }
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