using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace PrototypingGame
{
	public class MenuGame : MonoBehaviour
	{
		//Menus
		public GameObject MainMenu;
		public GameObject ModeSelect;
		public GameObject DifficultySelect;
		public GameObject ImageModeSelect;


		//Botao boltar
		public GameObject buttonBack;
		//Game Manager
		public GameObject gameManagerObject;
		private GameManager gameManager;
		/// <summary>
		/// Enum para as telas do menu
		/// </summary>
		private enum MenuScreen
		{
			MainMenu,
			SelectMode,
			DiffilcultyMode,
			ImageMode
		}
		private MenuScreen mMenuScreen;
		/// <summary>
		/// Da o setup inicial do menu
		/// </summary>
		void Start()
		{
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
			ImageModeSelect.SetActive(false);
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

			DifficultySelect.SetActive(false);
			ImageModeSelect.SetActive(true);
			//
			mMenuScreen = MenuScreen.ImageMode;
			DifficultySelect.SetActive(false);
		}
		/// <summary>
		/// Botao de selecao do tipo de modo de imagem
		/// </summary>
		/// <param name="mode"></param>
		public void btImageMode(int mode)
		{
			//atribui o modo de jogo selecionado ao GameManager
			gameManager.mImageMode = (ImageMode)mode;
			//
			//ImageModeSelect.SetActive(false);
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
				ImageModeSelect.SetActive(false);
				//
				mMenuScreen = MenuScreen.MainMenu;
				buttonBack.SetActive(false);
			}
			else if (mMenuScreen == MenuScreen.DiffilcultyMode)
			{
				MainMenu.SetActive(false);
				ModeSelect.SetActive(true);
				DifficultySelect.SetActive(false);
				ImageModeSelect.SetActive(false);
				//
				mMenuScreen = MenuScreen.SelectMode;
			}
			else if (mMenuScreen == MenuScreen.ImageMode)
			{
				MainMenu.SetActive(false);
				ModeSelect.SetActive(false);
				DifficultySelect.SetActive(true);
				ImageModeSelect.SetActive(false);
				//
				mMenuScreen = MenuScreen.SelectMode;
			}

		}
	}
}