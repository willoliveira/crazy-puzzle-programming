using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectImage : MonoBehaviour {

	#region PUBLIC VARS
	public static SelectImage instance;

	public GameObject btNext;

	public GameObject DefaultImageContainer;
	public GameObject InternetImageContainer;
	public GameObject LocalImageContainer;
	#endregion

	#region PRIVATE VARS
	private GameManager mGameManager;
	//
	private GameObject PreviousImageSelect = null;
	private GameObject ActualImageSelect = null;
	#endregion

	void Awake()
	{
		instance = this;
		mGameManager = GameManager.instance;
	}
	
	// Use this for initialization
	void OnEnable () {

		if (ActualImageSelect == null)
		{
			btNext.GetComponent<Button>().interactable = false;
		}
		//seta o tipo do modo
		SetActiveImageContainerMode(mGameManager.mImageMode);
	}
	void Start()
	{
		
	}

	void OnDisable()
	{
		//volta o botao de proximo
		btNext.GetComponent<Button>().interactable = true;
	}

	#region PUBLIC METHODS
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
			btNext.GetComponent<Button>().interactable = false;
			//
			ActualImageSelect.GetComponent<Image>().color = Color.white;
			//zera
			ActualImageSelect = null;
			PreviousImageSelect = null;
			
			mGameManager.ImageSelect = null;
			mGameManager.ImageURL = null;
		}
		else
		{
			//habilita o botao
			btNext.GetComponent<Button>().interactable = true;
			//seta o clicado como atual
			ActualImageSelect = imageSelect;
			imageSelect.GetComponent<Image>().color = Color.black;
			//se houver url
			if (string.IsNullOrEmpty(mObjectImage.ImageURL))
			{
				//salva a imagem escolhida
				mGameManager.ImageSelect = mObjectImage.ImageTexture;
			}
			else
			{
				//Salva a url
				mGameManager.ImageURL = mObjectImage.ImageURL;
			}
		}
		//se houver um anterior, volta a cor dele
		if (PreviousImageSelect != null)
		{
			PreviousImageSelect.GetComponent<Image>().color = Color.white;
		}
	}
	
	public void OnScrollImageContainer()
	{
		Debug.Log("OnScrollImageContainer");
	}
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="imageMode"></param>
	private void SetActiveImageContainerMode(ImageMode imageMode)
	{
		if (imageMode == ImageMode.Local)
		{
			LocalImageContainer.SetActive(true);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(false);
		}
		else if (imageMode == ImageMode.Internet)
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(true);
			DefaultImageContainer.SetActive(false);
		}
		else if (imageMode == ImageMode.Default)
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(true);
		}
	}
	#endregion
	
}
