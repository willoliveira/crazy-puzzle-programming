using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// TODO: quando for imagem de internet, colocar um loading
/// TODO: nao carregar a imagem de novo quando ela for a mesma a url carregada anteriormente, para não puxar muita banda da internet
/// TODO: desabilitar o botao iniciar ate a imagem da internet ser carregada
/// </summary>
public class ConfirmScreen : MonoBehaviour {

	#region PUBLIC VARS	
	public ImageServiceAPI mImageServiceAPI;
	//
	public Image ImageCrop;
	//
	public GameObject ButtonStart;
	//
	public GameObject PopUpImageCrop;
	public CropImage mCropImage;
	#endregion

	#region PRIVATE VARS
	//referece de game manager
	private GameManager mGameManager;
	private string urlImage;
	#endregion
	

	void Awake()
	{
		mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

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
			SetImageCropImageCenter();
		}
	}


	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	public void OpenPopUpImageCrop()
	{
		//ativa a pop up
		//TODO: depois colocar uma animacao de entrada para a pop up
		PopUpImageCrop.SetActive(true);
		mCropImage.Image.GetComponent<Image>().sprite = Sprite.Create(mGameManager.ImageSelect, new Rect(new Vector2(0, 0), new Vector2(mGameManager.ImageSelect.width, mGameManager.ImageSelect.height)), new Vector2(0, 0));
		//auto ajusta imagem
		mCropImage.AdjustRect();
	}
	/// <summary>
	/// 
	/// </summary>
	public void ClosePopUpImageCrop()
	{
		PopUpImageCrop.SetActive(false);
	}
	public void OnConfirm()
	{
		//nova imagem cropada
		Image newCropedImage = mCropImage.ImageCrop.GetComponent<Image>();
		//seta a nova imagem cropada
		SetImageCropImage(newCropedImage.sprite);
		
		//fecha a popup
		PopUpImageCrop.SetActive(false);
	}
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="UrlImage"></param>
	/// <returns></returns>
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
		SetImageCropImageCenter();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="ImageCrop"></param>
	public void SetImageCropImage(Sprite ImageCropSprite)
	{
		//mGameManager.ImageSelect = ImageCropSprite.texture;
		mGameManager.ImageCropRect = ImageCropSprite.textureRect;
		//atualiza o sprite
		ImageCrop.sprite = Sprite.Create(mGameManager.ImageSelect, mGameManager.ImageCropRect, new Vector2(0, 0));
	}
	/// <summary>
	/// 
	/// </summary>
	private void SetImageCropImageCenter()
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
			PosY = (mGameManager.ImageSelect.height - CropSize) / 2; // * -1; // talvez é negativo as coordenadas
		}
		//retangulo de recorte
		CropRect = new Rect(new Vector2(PosX, PosY), new Vector2(CropSize, CropSize));
		//printa na tela o recorte
		Sprite ImageCropSprite = Sprite.Create(mGameManager.ImageSelect, CropRect, new Vector2(0.5f, 0.5f));
		//Salva o crop
		SetImageCropImage(ImageCropSprite);
	}
	#endregion
}
