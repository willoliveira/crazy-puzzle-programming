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
				
		public GameObject DefaultImageContainer;
		public GameObject InternetImageContainer;
		public GameObject LocalImageContainer;

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
			SelectMode
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
			
			buttonBack.SetActive(true);
		}

		/// <summary>
		/// 
		/// </summary>
		public void btLocalSearchImage()
		{
			Debug.Log("btLocalSearchImage");
			LocalImageContainer.SetActive(true);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(false);
		}
		/// <summary>
		/// 
		/// </summary>
		public void btInternetSearchImage()
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(true);
			DefaultImageContainer.SetActive(false);
		}
		/// <summary>
		/// 
		/// </summary>
		public void btDefaultSearchImage()
		{
			LocalImageContainer.SetActive(false);
			InternetImageContainer.SetActive(false);
			DefaultImageContainer.SetActive(true);
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
				//
				mMenuScreen = MenuScreen.MainMenu;
				buttonBack.SetActive(false);
			}

		}
	}
}