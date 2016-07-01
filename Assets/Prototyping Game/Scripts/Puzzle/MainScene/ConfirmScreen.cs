using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using PrototypingGame;

public class ConfirmScreen : MonoBehaviour {

	#region PUBLIC VARS
	//referece de game manager
	public GameManager mGameManager;

	public Image ImageCrop;
	#endregion

	#region PRIVATE VARS
	#endregion

	// Use this for initialization
	void OnEnable () {
		//
		SetImageCropImage();
	}


	#region PUBLIC METHODS
	#endregion

	#region PRIVATE METHODS
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
