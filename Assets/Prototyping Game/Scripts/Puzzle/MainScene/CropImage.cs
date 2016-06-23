using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CropImage : MonoBehaviour
{
	public Texture2D ImageTexture;

	public RectTransform Image;
	public RectTransform AreaCrop;
	public RectTransform ImageCrop;

	private Image img;
	private Image imgCrop;

	// Use this for initialization
	void Start()
	{
		img = Image.GetComponent<Image>();
		imgCrop = AreaCrop.GetComponent<Image>();
	}

	public void OnCrop()
	{
		//Debug.Log("Image: " + Camera.main.ViewportToWorldPoint(Image.transform.position) + " - Border" + Camera.main.ViewportToWorldPoint(AreaCrop.transform.position));
		Debug.Log("ImagelocalPosition: " + Image.transform.localPosition+ " - BorderlocalPosition" + Camera.main.ViewportToWorldPoint(AreaCrop.transform.localPosition));




		//Image img = Image.GetComponent<Image>();
		//Texture2D texture = Image.GetComponent<Image>().mainTexture as Texture2D; //.GetComponent<Texture2D>();

		//Color[] pixels = texture.GetPixels(0, 0, 100, 100);

		//Texture2D textureCrop = new Texture2D(100, 100);
		//textureCrop.SetPixels(pixels);
		//textureCrop.wrapMode = TextureWrapMode.Clamp;
		//textureCrop.Apply();

		//ImageCrop.GetComponent<Image>().sprite = Sprite.Create(textureCrop, new Rect(0, 0, textureCrop.width, textureCrop.height), new Vector2(0, 0));
	}
}