using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectImage : MonoBehaviour {

	#region PUBLIC VARS
	public static SelectImage instance;
	public MenuGame mMenuGame;

	public GameObject DefaultImageContainer;
	public GameObject InternetImageContainer;
	public GameObject LocalImageContainer;
	#endregion

	#region PRIVATE VARS
	private GameManager mGameManager;
	//
	//private GameObject PreviousImageSelect = null;
	//private GameObject ActualImageSelect = null;
	#endregion
	/// <summary>
	/// 
	/// </summary>
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		mGameManager = GameManager.instance;
	}

	/// <summary>
	/// 
	/// </summary>
	void OnEnable()
	{
		mGameManager = GameManager.instance;
		//if (ActualImageSelect == null)
		//{
		//	btNext.GetComponent<Button>().interactable = false;
		//}
		//seta o tipo do modo
		SetActiveImageContainerMode(mGameManager.mImageMode);
	}

	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="mObjectImage"></param>
	public void SetImageChoice(GameObject imageSelect)
	{
		//pega o object image do game object
		ObjectImage mObjectImage = imageSelect.GetComponent<ObjectImage>();
		//guarda o anterior clicado
		//if (ActualImageSelect != null && ActualImageSelect != imageSelect)
		//{
		//	PreviousImageSelect = ActualImageSelect;
		//}
		//if (ActualImageSelect == imageSelect)
		//{
		//	//desabilita o botao
		//	//btNext.GetComponent<Button>().interactable = false;
		//	//
		//	ActualImageSelect.GetComponent<Image>().color = Color.white;
		//	//zera
		//	ActualImageSelect = null;
		//	PreviousImageSelect = null;
			
		//	mGameManager.ImageSelect = null;
		//	mGameManager.ImageURL = null;
		//}
		//else
		//{
		//habilita o botao
		//btNext.GetComponent<Button>().interactable = true;
		//seta o clicado como atual
		//ActualImageSelect = imageSelect;
		//imageSelect.GetComponent<Image>().color = Color.black;
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
		mMenuGame.btNext();
		//}
		//se houver um anterior, volta a cor dele
		//if (PreviousImageSelect != null)
		//{
		//	PreviousImageSelect.GetComponent<Image>().color = Color.white;
		//}
	}	
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="imageMode"></param>
	private void SetActiveImageContainerMode(ImageMode imageMode)
	{
		//if (imageMode == ImageMode.Local)
		//{
		//	LocalImageContainer.SetActive(true);
		//	InternetImageContainer.SetActive(false);
		//	DefaultImageContainer.SetActive(false);
		//}
		//else 
		if (imageMode == ImageMode.Internet)
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
