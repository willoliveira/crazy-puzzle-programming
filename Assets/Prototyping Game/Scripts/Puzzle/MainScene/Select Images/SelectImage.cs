using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using PrototypingGame;

public class SelectImage : MonoBehaviour {
	#region PUBLIC VARS
	public GameManager mGameManager;
	
	public GameObject DefaultImageContainer;
	public GameObject InternetImageContainer;
	public GameObject LocalImageContainer;
	#endregion

	#region PRIVATE VARS
	private GameObject ImageSelect;
	//
	private GameObject PreviousImageSelect = null;
	private GameObject ActualImageSelect = null;
	#endregion

	// Use this for initialization
	void OnEnable () {
		//sempre que habiltar a tela, zera		
		PreviousImageSelect = null;
		ActualImageSelect = null;
		//seta o tipo do modo
		SetActiveImageContainerMode(mGameManager.mImageMode);
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="imageMode"></param>
	private void SetActiveImageContainerMode(ImageMode imageMode)
	{
		Debug.Log(imageMode);
		if (imageMode == ImageMode.Local)
		{
			LocalImageContainer.SetActive(true);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(false);
			//se ouver uma imagem selecionada
			if (ImageSelect != null)
			{
				//quando for local, seta como selecao atual
				ActualImageSelect = ImageSelect;
			}
		}
		else if (imageMode == ImageMode.Internet)
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(true);
			DefaultImageContainer.SetActive(false);
			//zera quando for da net, faz escolher de novo
			mGameManager.ImageSelect = null;
			//desabilita o botao
			GameObject.Find("btNext").GetComponent<Button>().interactable = false;
		}
		else if (imageMode == ImageMode.Default)
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(true);
		}
	}
	/// <summary>
	/// 
	/// TODO: Conversar com o Lucas sobre qual sera o estado de checkado
	/// </summary>
	/// <param name="mObjectImage"></param>
	public void SetImageChoice(GameObject imageSelect)
	{
		//pega o object image do game object
		ObjectImage mObjectImage = imageSelect.GetComponent<ObjectImage>();
		//guarda o anterior clicado
		if (ActualImageSelect != null && ActualImageSelect != imageSelect)
		{
			PreviousImageSelect = ActualImageSelect;
		}
		
		if (ActualImageSelect == imageSelect)
		{
			//desabilita o botao
			GameObject.Find("btNext").GetComponent<Button>().interactable = false;
			//
			ActualImageSelect.GetComponent<Image>().color = Color.white;
			//
			ActualImageSelect = null;
			PreviousImageSelect = null;
			//
			mGameManager.ImageSelect = null;
		}
		else
		{
			//habilita o botao
			GameObject.Find("btNext").GetComponent<Button>().interactable = true;
			//seta o clicado como atual
			ActualImageSelect = imageSelect;
			Debug.Log(ActualImageSelect);
			imageSelect.GetComponent<Image>().color = Color.black;
			//salva a imagem escolhida
			mGameManager.ImageSelect = mObjectImage.ImageTexture;
		}
		//se houver um anterior, volta a cor dele
		if (PreviousImageSelect != null)
		{
			PreviousImageSelect.GetComponent<Image>().color = Color.white;
		}
	}
}
