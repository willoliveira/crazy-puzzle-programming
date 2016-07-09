using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MenuGame : MonoBehaviour
{
	#region PUBLIC VARS
	//Menus
	public GameObject MainMenu;
	public GameObject SelectMode;
	public GameObject SelectImages;
	public GameObject ConfirmationScreen;
	//Botao boltar
	public GameObject buttonBack;
	public GameObject buttonNext;
	#endregion

	#region PRIVATE VARS
	//private GameManager mGameManager;
	/// <summary>
	/// Enum para as telas do menu
	/// </summary>
	private enum MenuScreen
	{
		MainMenu,
		SelectMode,
		SelectImages,
		ConfirmationScreen
	}
	/// <summary>
	/// 
	/// </summary>
	private MenuScreen mMenuScreen;	
	#endregion


	/// <summary>
	/// Da o setup inicial do menu
	/// </summary>
	void Start()
	{
		//Referencia de qual tela o usuario esta
		mMenuScreen = MenuScreen.MainMenu;
		//Seta o botao avancar e voltar como inativo no inicio
		buttonBack.SetActive(false);
		buttonNext.SetActive(false);
		//Seta a tela Main como ativa no inicio
		MainMenu.SetActive(true);
		SelectMode.SetActive(false);
		SelectImages.SetActive(false);
		ConfirmationScreen.SetActive(false);
	}

	#region PUBLIC METHODS
	/// <summary>
	/// Botao jogar no menu principal
	/// </summary>
	public void btJogar()
	{
		MainMenu.SetActive(false);
		SelectMode.SetActive(true);
		//
		mMenuScreen = MenuScreen.SelectMode;

		buttonBack.SetActive(true);
		buttonNext.SetActive(true);
	}
	/// <summary>
	/// Botao voltar do menu principal do jogo
	/// </summary>
	public void btBack()
	{
		if (mMenuScreen == MenuScreen.SelectMode)
		{
			//desativa os botoes
			buttonBack.SetActive(false);
			buttonNext.SetActive(false);
			//
			MainMenu.SetActive(true);
			SelectMode.SetActive(false);
			SelectImages.SetActive(false);
			ConfirmationScreen.SetActive(false);
			//
			mMenuScreen = MenuScreen.MainMenu;
		}
		else if (mMenuScreen == MenuScreen.SelectImages)
		{
			//ativa os botoes
			buttonBack.SetActive(true);
			buttonNext.SetActive(true);
			//ativa/desativa as telas
			MainMenu.SetActive(false);
			SelectMode.SetActive(true);
			SelectImages.SetActive(false);
			ConfirmationScreen.SetActive(false);
			//
			mMenuScreen = MenuScreen.SelectMode;
		}
		else if (mMenuScreen == MenuScreen.ConfirmationScreen)
		{
			//ativa os botoes
			buttonBack.SetActive(true);
			buttonNext.SetActive(true);
			//ativa/desativa as telas
			MainMenu.SetActive(false);
			SelectMode.SetActive(false);
			SelectImages.SetActive(true);
			ConfirmationScreen.SetActive(false);
			//
			mMenuScreen = MenuScreen.SelectImages;
		}
	}
	/// <summary>
	/// Botao avancar do menu principal do jogo
	/// </summary>
	public void btNext()
	{
		if (mMenuScreen == MenuScreen.SelectMode)
		{
			//ativa os botoes
			buttonBack.SetActive(true);
			buttonNext.SetActive(true);
			//ativa/desativa as telas
			MainMenu.SetActive(false);
			SelectMode.SetActive(false);
			SelectImages.SetActive(true);
			ConfirmationScreen.SetActive(false);
			//
			mMenuScreen = MenuScreen.SelectImages;

		}
		else if (mMenuScreen == MenuScreen.SelectImages)
		{
			//ativa/desativa os botoes
			buttonBack.SetActive(true);
			buttonNext.SetActive(false);
			//ativa/desativa as telas
			MainMenu.SetActive(false);
			SelectMode.SetActive(false);
			SelectImages.SetActive(false);
			ConfirmationScreen.SetActive(true);
			//
			mMenuScreen = MenuScreen.ConfirmationScreen;

		}
	}
	#endregion
}