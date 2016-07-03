using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CropImage : MonoBehaviour
{
	public RectTransform ContainerCrop;
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

	//TODO: tentar com OnDrag
	void Update()
	{
		OnCrop();
	}
	/// <summary>
	/// Funcao que faz o crop de image
	/// </summary>
	public void OnCrop()
	{
		//Referencia da image
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

		//Cria o sprite de crop
		ImageCrop.GetComponent<Image>().sprite = Sprite.Create(spriteTexture, cropRect, new Vector2(0, 0));
	}
	/// <summary>
	/// Ajusta o rectTransform da imagem deixando justo, assim, o drag dentro somente deste RecTransform é possivel
	/// </summary>
	public void AdjustRect()
	{
		Image ImageCache = Image.GetComponent<Image>();
		float ImageRatio;
		//volta a imagem
		Image.sizeDelta = new Vector2(ContainerCrop.sizeDelta.x, ContainerCrop.sizeDelta.y);
		//se o width da imagem for maior que o height
		if (ImageCache.sprite.texture.width > ImageCache.sprite.texture.height) {
			//porcentagem para ser aplicado proporcional ao retangulo da imagem
			ImageRatio = ((float)ImageCache.sprite.texture.width / (float)ImageCache.sprite.texture.height);
			//aplica, usando razao e proporcao
			Image.sizeDelta = new Vector2(Image.sizeDelta.x, Image.sizeDelta.x / ImageRatio);
			//deixa a area de crop do tamanho do lado menor
			AreaCrop.sizeDelta = new Vector2(Image.sizeDelta.y - 5, Image.sizeDelta.y - 5);
		}
		//se o height da imagem for maior que o width, ou ate se for igual
		else
		{
			//porcentagem para ser aplicado proporcional ao retangulo da imagem
			ImageRatio = ((float)ImageCache.sprite.texture.height / (float)ImageCache.sprite.texture.width);
			//aplica, usando razao e proporcao
			Image.sizeDelta = new Vector2(Image.sizeDelta.y / ImageRatio, Image.sizeDelta.y);
			//deixa a area de crop do tamanho do lado menor
			AreaCrop.sizeDelta = new Vector2(Image.sizeDelta.x - 5, Image.sizeDelta.x - 5);
		}
		//Coloca a area de crop no meio da imagem de novo
		AreaCrop.anchoredPosition = new Vector2(Image.sizeDelta.x / 2, (Image.sizeDelta.y / 2) * -1);
		//centraliza a imagem de novo
		Image.anchoredPosition = new Vector2(0f, 0f);
	}
}