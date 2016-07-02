using UnityEngine;
using UnityEngine.UI;
using System;
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
		//AreaCrop.rect.width / 2 isso foi por mudei o pivot para x0,5 y0,5 - antes era x0 y1
		//Porcentagem relativa do rectTransform sobre a o RectTransform da image para conseguir as coordenadas em pixesl da imagem
		float porcentX = (float)Math.Round(AreaCrop.anchoredPosition.x - AreaCrop.rect.width / 2, 2) / (float)Math.Round((imageUI.rectTransform.rect.width), 2),
			  porcentY = ((float)Math.Round((AreaCrop.anchoredPosition.y*-1) - AreaCrop.rect.height / 2, 2)) / (float)Math.Round(imageUI.rectTransform.rect.height, 2), //+ AreaCrop.rect.height / 2,
			  porcentWidth = AreaCrop.rect.width / imageUI.rectTransform.rect.width,
			  porcentHeight = AreaCrop.rect.height / imageUI.rectTransform.rect.height;
		//Construção do rect de recorte		
		Rect cropRect = new Rect();		
		cropRect.x = (porcentX * spriteTexture.width);
		//tamanho da image - tamanho da area de crop - posicao do recorte
		//invertendo o Y da razao e proporcao da area de crop para a imagem, pois no unity, o ponto de registro da imagem é left bottom, e sobe negativo
		//          Daqui         Pra ca         
		//  ^    X_________      _________
		//  |    |         |    |         |
		//  | -Y |         |    |         |
		//  |    |_________|    X_________|
		//       0              0
		cropRect.y = spriteTexture.height - (porcentHeight * spriteTexture.height) - (porcentY * spriteTexture.height);
		cropRect.width = porcentWidth * spriteTexture.width;
		cropRect.height = porcentHeight * spriteTexture.height;

		//Debug.Log("Screen.width: " + Screen.width);
		////rect
		//Debug.Log("Image.rect.width: " + Image.rect.width + " | Image.rect.height: " + Image.rect.height);
		////image
		//Debug.Log("spriteTexture.width : " + spriteTexture.width + " | spriteTexture.height: " + spriteTexture.height);
		

		//Cria o sprite de crop
		ImageCrop.GetComponent<Image>().sprite = Sprite.Create(spriteTexture, cropRect, new Vector2(0, 0));
	}

	public void AdjustRect()
	{
		//porcentagem para ser aplicado lado desproporcional do retangulo
		float ImageRatio = ((float)Image.GetComponent<Image>().sprite.texture.width / (float)Image.GetComponent<Image>().sprite.texture.height);
		//aplica, usando razao e proporcao
		Image.sizeDelta = new Vector2(Image.sizeDelta.x, Image.sizeDelta.x / ImageRatio);

		//Debug.Log(Image.GetComponent<Image>().sprite.texture.width + " / " + Image.GetComponent<Image>().sprite.texture.height);
		//Debug.Log("Image.sizeDelta.x: " + Image.sizeDelta.x + " | Image.sizeDelta.y: " + Image.sizeDelta.y + " | ImageRatio: " + ImageRatio + " | sum: " + (float)Image.rect.height / ImageRatio + " | formula: " + Image.rect.height + " / " + "( " + Image.GetComponent<Image>().sprite.texture.width + " / " + Image.GetComponent<Image>().sprite.texture.height + ")");
	}
}