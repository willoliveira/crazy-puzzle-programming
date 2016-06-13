using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PrototypingGame
{
	public class OnGuiResize : MonoBehaviour {
		#region CONFIG GUI RESIZE
		//Tamanho da borda
		public float BorderBord = 20f;
		#endregion

		//Tamanho da tela
		private int ScreenHeight;
		private int ScreenWidth;
		//Referencia do Board
		private GameObject mBoard;
		private UIBoardManager mBoardManager;

		void Start() {
			ScreenHeight = 0;
			ScreenWidth = 0;
			//Board
			mBoard = GameObject.Find("BoardManager");
			//BoardManager
			mBoardManager = mBoard.GetComponent<UIBoardManager>();
		}

		// Update is called once per frame
		void Update() {
			if (ScreenHeight != Screen.height || ScreenWidth != Screen.width)
			{
				Debug.Log("Change Resolution");
				//Redimensiona o board
				RecizeBoard();
			}
		}
		/// <summary>
		/// TODO: Fazer para telas grandes
		/// </summary>
		void RecizeBoard()
		{
			float scale;
			RectTransform BoardContainer = GameObject.Find("UIBoardTransform").GetComponent<RectTransform>();
			Debug.Log("mBoardManager.BoardSize > Screen.height");
			//landscape
			if (Screen.width > Screen.height)
			{
				// deixando o board a 80 do tamanho da tela em altura
				double scaleLandscape = (Screen.height * 0.8) / mBoardManager.BoardSize;

				float BoardSizeWithScale = (float)(mBoardManager.BoardSize * scaleLandscape);
				float positionY = (Screen.height / 2) - (Screen.height - BoardSizeWithScale) / 2;

				//redimensiona o board
				BoardContainer.localScale = new Vector2((float)scaleLandscape, (float)scaleLandscape);// esse -20 é a borda da tela. do lado esquerdo e direito
				BoardContainer.anchoredPosition = new Vector2(Screen.width - (BorderBord / 2), positionY); //esse 250 ta a caralha
			}
			else {
				//Screen.Heigth / 2 - ( ( Screen.Heigth -(Border.size * BoarderScale) ) / 2 )
				//com local position
				scale = (Screen.width - BorderBord) / mBoardManager.BoardSize; // esse -20 é a borda da tela. do lado esquerdo e direito
				//redimensiona o board
				BoardContainer.localScale = new Vector2((Screen.width - BorderBord) / mBoardManager.BoardSize, (Screen.width - BorderBord) / mBoardManager.BoardSize);// esse -20 é a borda da tela. do lado esquerdo e direito
				BoardContainer.localPosition = new Vector2((Screen.width / 2) - (BorderBord / 2), 250f); //esse 250 ta a caralha


				//scale = (Screen.width - BorderBord) / mBoardManager.BoardSize; // esse -20 é a borda da tela. do lado esquerdo e direito
				////redimensiona o board
				//BoardContainer.localScale = new Vector2((Screen.width - BorderBord) / mBoardManager.BoardSize, (Screen.width - BorderBord) / mBoardManager.BoardSize);// esse -20 é a borda da tela. do lado esquerdo e direito
				//BoardContainer.localPosition = new Vector2((Screen.width / 2) - (BorderBord / 2), 250f); //esse 250 ta a caralha
			}
			//seta novamente o tamanho da tela
			ScreenHeight = Screen.height;
			ScreenWidth = Screen.width;
		}
	}
}
