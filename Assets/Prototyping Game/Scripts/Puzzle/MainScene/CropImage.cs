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

	void Update()
	{
		OnCrop();
	}

	public void OnCrop()
	{
		//Referencia da image,
		Image imageUI = Image.GetComponent<Image>();
		//Textura da imagem
		Sprite spriteToCropSprite = imageUI.sprite;
		//Textura da area do crop
		Texture2D spriteTexture = spriteToCropSprite.texture;
		//Porcentagem relativa do rectTransform sobre a o RectTransform da image para conseguir as coordenadas em pixesl da imagem
		float porcentX = AreaCrop.anchoredPosition.x / imageUI.rectTransform.rect.width,
			  porcentY = (AreaCrop.anchoredPosition.y) / imageUI.rectTransform.rect.height,
			  porcentWidth = AreaCrop.rect.width / imageUI.rectTransform.rect.width,
			  porcentHeight = AreaCrop.rect.height / imageUI.rectTransform.rect.height;
		//Construção do rect de recorte
		Rect cropRect = new Rect();
		cropRect.x = porcentX * spriteTexture.width;
		cropRect.y = spriteTexture.height + (porcentY * spriteTexture.height) - (porcentHeight * spriteTexture.height);
		cropRect.width = porcentWidth * spriteTexture.width;
		cropRect.height = porcentHeight * spriteTexture.height;
		//Cria o sprite de crop
		ImageCrop.GetComponent<Image>().sprite = Sprite.Create(spriteTexture, cropRect, new Vector2(0, 0));
	}
}