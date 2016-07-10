using UnityEngine;
using System.Collections;

public class QuickStart : MonoBehaviour {

	public SearchImagesDefault mSearchImagesDefault;

	private GameManager mGameManager;
	private Texture2D mTexture2D;

	void Start()
	{
		mGameManager = GameManager.instance;
	}
	/// <summary>
	/// 
	/// </summary>
	public void btQuickStart()
	{
		int randomIndex = Random.Range(0, mSearchImagesDefault.LocalImages.Length - 1);
		//Modos Default
		mGameManager.mGameMode = GameMode.Classic;
		mGameManager.mImageMode = ImageMode.Default;
		//coloca uma imagem aleatória
		mGameManager.ImageSelect = mSearchImagesDefault.LocalImages[randomIndex].texture;
		//seta o crop
		mGameManager.ImageCropRect = ReturnRectCropImageCenter();
	}
	/// <summary>
	/// 
	/// </summary>
	private Rect ReturnRectCropImageCenter()
	{
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
			PosY = (mGameManager.ImageSelect.height - CropSize) / 2;
		}
		//retangulo de recorte
		return new Rect(new Vector2(PosX, PosY), new Vector2(CropSize, CropSize));
	}

}
