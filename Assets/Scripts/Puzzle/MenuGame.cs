using UnityEngine;
using System.Collections;

public class MenuGame : MonoBehaviour {
	//
	public GameObject MainMenu;
	public GameObject ModeSelect;
	public GameObject DifficultySelect;
	//
	public GameObject buttonBack;
	//
	public GameObject gameManagerObject;
	private GameManager gameManager;
	
	private enum MenuScreen
	{
		MainMenu,
		SelectMode,
		DiffilcultyMode
	}

	private MenuScreen mMenuScreen;

	// Use this for initialization
	void Start () {
		//
		gameManager = gameManagerObject.GetComponent<GameManager>();
		//
		mMenuScreen = MenuScreen.MainMenu;
		//
		buttonBack.SetActive(false);
		//
		MainMenu.SetActive(true);
		ModeSelect.SetActive(false);
		DifficultySelect.SetActive(false);
	}

	public void btJogar()
	{
		MainMenu.SetActive(false);
		ModeSelect.SetActive(true);
		//
		mMenuScreen = MenuScreen.SelectMode;
		buttonBack.SetActive(true);
	}

	public void btSelectMode(int mode)
	{
		//atribui o modo de jogo selecionado ao GameManager
		gameManager.mSelectMode = (SelectMode)mode;

		ModeSelect.SetActive(false);
		DifficultySelect.SetActive(true);
		//
		mMenuScreen = MenuScreen.DiffilcultyMode;
		buttonBack.SetActive(true);
	}

	public void btSelectDifficulty(int mode)
	{
		//atribui o modo de jogo selecionado ao GameManager
		gameManager.mDiffilcultyMode = (DiffilcultyMode)mode;
		//
		DifficultySelect.SetActive(false);
	}

	public void btBack()
	{
		if (mMenuScreen == MenuScreen.SelectMode)
		{
			MainMenu.SetActive(true);
			ModeSelect.SetActive(false);
			DifficultySelect.SetActive(false);
			//
			mMenuScreen = MenuScreen.MainMenu;
			buttonBack.SetActive(false);
		}
		else if (mMenuScreen == MenuScreen.DiffilcultyMode)
		{
			MainMenu.SetActive(false);
			ModeSelect.SetActive(true);
			DifficultySelect.SetActive(false);
			//
			mMenuScreen = MenuScreen.SelectMode;
		}
	}

}