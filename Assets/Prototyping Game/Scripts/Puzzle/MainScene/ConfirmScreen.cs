using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using PrototypingGame;

/// <summary>
/// TODO: quando for imagem de internet, colocar um loading
/// TODO: nao carregar a imagem de novo quando ela for a mesma a url carregada anteriormente, para não puxar muita banda da internet
/// TODO: desabilitar o botao iniciar ate a imagem da internet ser carregada
/// </summary>
public class ConfirmScreen : MonoBehaviour {

	#region PUBLIC VARS
	//referece de game manager
	public GameManager mGameManager;
	public ImageServiceAPI mImageServiceAPI;

	public Image ImageCrop;

	public GameObject ButtonStart;
	#endregion

	#region PRIVATE VARS
	private string urlImage;
	#endregion

	// Use this for initialization
	void OnEnable () {
		//zera a imagem alterior
		ImageCrop.sprite = null;
		//Debug.Log(mGameManager.ImageURL + " != " + urlImage);
		if (mGameManager.mImageMode == ImageMode.Internet && mGameManager.ImageURL != urlImage)
		{
			//desabilita o botao ate a imagem carregar
			ButtonStart.GetComponent<Button>().interactable = false;
			//guarda um cache da ultima imagem salva
			urlImage = mGameManager.ImageURL;
			//puxa a imagem da net
			StartCoroutine(GetImage(mGameManager.ImageURL));
		}
		else
		{
			//seta crop image no centro
			SetImageCropImage();
		}
	}


	#region PUBLIC METHODS
	#endregion

	#region PRIVATE METHODS

	private IEnumerator GetImage(string UrlImage)
	{
		Texture2D TextureImageSelect = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImage(UrlImage, value => TextureImageSelect = value));
		//Carrega a imagem 
		mGameManager.ImageSelect = TextureImageSelect;
		//habilita o botam depois da imagem ter carregado
		ButtonStart.GetComponent<Button>().interactable = true;
		//
		SetImageCropImage();
	}

	private void SetImageCropImage()
	{
		//retangulo de recorte
		Rect CropRect;
		//tamanho do crop
		int CropSize;
		//posicao do recorte
		float PosX = 0f, PosY = 0f;
		if (mGameManager.ImageSelect.width > mGameManager.ImageSelect.height)
		{
			CropSize = mGameManager.ImageSelect.height;
			PosX = (mGameManager.ImageSelect.width - CropSize) / 2;
		} 
		else
		{
			CropSize = mGameManager.ImageSelect.width;
			PosY = (mGameManager.ImageSelect.height - CropSize) / 2; // * -1 // talvez é negativo as coordenadas
		}
		//retangulo de recorte
		CropRect = new Rect(new Vector2(PosX, PosY), new Vector2(CropSize, CropSize));
		//printa na tela o recorte
		ImageCrop.sprite = Sprite.Create(mGameManager.ImageSelect, CropRect, new Vector2(0.5f, 0.5f));

		Debug.Log("ImageCrop.sprite.textureRect: " + ImageCrop.sprite.textureRect + " | ImageCrop.sprite.texture width: " + ImageCrop.sprite.texture.width + " height: " + ImageCrop.sprite.texture.height);

		//
		mGameManager.ImageCropSelect = ImageCrop.sprite.texture;
		mGameManager.ImageCropRect = ImageCrop.sprite.textureRect;
	}
	#endregion
}
