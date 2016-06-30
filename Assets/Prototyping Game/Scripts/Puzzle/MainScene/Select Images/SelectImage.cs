using UnityEngine;
using System.Collections;

using PrototypingGame;

public class SelectImage : MonoBehaviour {

	public GameManager mGameManager;
	
	public GameObject DefaultImageContainer;
	public GameObject InternetImageContainer;
	public GameObject LocalImageContainer;

	// Use this for initialization
	void OnEnable () {
		//
		SetActiveImageContainerMode(mGameManager.mImageMode);
	}
	
	private void SetActiveImageContainerMode(ImageMode imageMode)
	{
		Debug.Log(imageMode);
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

	public void SetImageChoice(ObjectImage mObjectImage)
	{

	}
}
