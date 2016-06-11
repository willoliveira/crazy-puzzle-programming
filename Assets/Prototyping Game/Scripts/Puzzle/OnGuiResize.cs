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
			RectTransform BoardContainer = GameObject.Find("UIBoardTransform").GetComponent<RectTransform>();
			float scale = (Screen.width - BorderBord) / mBoardManager.BoardSize; // esse -20 é a borda da tela. do lado esquerdo e direito
			//redimensiona o board
			BoardContainer.localScale = new Vector2((Screen.width - BorderBord) / mBoardManager.BoardSize, (Screen.width - BorderBord) / mBoardManager.BoardSize);// esse -20 é a borda da tela. do lado esquerdo e direito
			BoardContainer.localPosition = new Vector2((Screen.width / 2) - (BorderBord / 2), 250f); //esse 250 ta a caralha
			//seta novamente o tamanho da tela
			ScreenHeight = Screen.height;
			ScreenWidth = Screen.width;
		}
	}
}
