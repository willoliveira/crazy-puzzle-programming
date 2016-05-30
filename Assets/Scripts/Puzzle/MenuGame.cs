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
	
	void Start () {
		//Manager do jogo
		gameManager = gameManagerObject.GetComponent<GameManager>();
		//Referencia de qual tela o usuario esta
		mMenuScreen = MenuScreen.MainMenu;
		//Seta o botao voltar como inativo no inicio
		buttonBack.SetActive(false);
		//Seta a tela Main como ativa no inicio
		MainMenu.SetActive(true);
		ModeSelect.SetActive(false);
		DifficultySelect.SetActive(false);
	}
	/// <summary>
	/// Botao jogar no menu principal
	/// </summary>
	public void btJogar()
	{
		MainMenu.SetActive(false);
		ModeSelect.SetActive(true);
		//
		mMenuScreen = MenuScreen.SelectMode;
		buttonBack.SetActive(true);
	}
	/// <summary>
	/// Botao de selecao do tipo de jogo
	/// </summary>
	/// <param name="mode"></param>
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
	/// <summary>
	/// Botao de selecao do nivel de dificuldade
	/// </summary>
	/// <param name="mode"></param>
	public void btSelectDifficulty(int mode)
	{
		//atribui o modo de jogo selecionado ao GameManager
		gameManager.mDiffilcultyMode = (DiffilcultyMode)mode;
		//
		DifficultySelect.SetActive(false);
	}

	/// <summary>
	/// Botao voltar do menu principal do jogo
	/// </summary>
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