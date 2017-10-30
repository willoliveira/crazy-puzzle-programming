using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using DoozyUI;

namespace CountingSheeps.NewOldPuzzle
{
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
		//public GameObject buttonNext;
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
			mMenuScreen = MenuScreen.MainMenu;
			buttonBack.SetActive(false);
		}

		#region PUBLIC METHODS
		/// <summary>
		/// Botao voltar do menu principal do jogo
		/// </summary>
		public void btBack()
		{
			Debug.Log(mMenuScreen);

			if (mMenuScreen == MenuScreen.SelectMode)
			{
				buttonBack.SetActive(false);
				mMenuScreen = MenuScreen.MainMenu;

				SelectMode.GetComponent<UIElement>().Hide(false);

				MainMenu.SetActive(true);
				MainMenu.GetComponent<UIElement>().Show(false);
			}
			else if (mMenuScreen == MenuScreen.SelectImages)
			{
				mMenuScreen = MenuScreen.SelectMode;
				
				SelectImages.GetComponent<UIElement>().Hide(false);

				SelectMode.SetActive(true);
				SelectMode.GetComponent<UIElement>().Show(false);
			}
			else if (mMenuScreen == MenuScreen.ConfirmationScreen)
			{
				buttonBack.SetActive(true);
				mMenuScreen = MenuScreen.SelectImages;

				ConfirmationScreen.GetComponent<UIElement>().Hide(false);

				SelectImages.SetActive(true);
				SelectImages.GetComponent<UIElement>().Show(false);
			}
		}
		/// <summary>
		/// Botao avancar do menu principal do jogo
		/// </summary>
		public void btNext()
		{
			Debug.Log(mMenuScreen);
			if (mMenuScreen == MenuScreen.MainMenu)
			{

				buttonBack.SetActive(true);

				mMenuScreen = MenuScreen.SelectMode;
			}
			else if (mMenuScreen == MenuScreen.SelectMode)
			{
				mMenuScreen = MenuScreen.SelectImages;
			}
			else if (mMenuScreen == MenuScreen.SelectImages)
			{
				mMenuScreen = MenuScreen.ConfirmationScreen;

				SelectImages.GetComponent<UIElement>().Hide(false);
				
				ConfirmationScreen.SetActive(true);
				ConfirmationScreen.GetComponent<UIElement>().Show(false);
			}
		}
		#endregion
	}
}