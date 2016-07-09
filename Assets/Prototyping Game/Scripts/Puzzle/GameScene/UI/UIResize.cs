using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIResize : MonoBehaviour
{

	#region PUBLIC VARS

	#region CONFIG GUI RESIZE
	//Tamanho da borda
	public float BorderPortrait = 40f;
	public float BorderLandscapeLeft = 30f;
	//Porcentagem do tamanho do board quando a tela esta em landscape
	[Range(0, 1)]
	public float PorcentBordLandscape = 0.8f;
	//Porcentagem do tamanho do boarder top quando a tela esta em portrait
	[Range(0, 1)]
	public float PorcentBorderTopPortrait = 0.3f;
	//Tempo
	public RectTransform TimeText;
	#endregion

	#endregion

	#region PRIVATE VARS
	//Tamanho da tela
	private int ScreenHeight;
	private int ScreenWidth;
	//Referencia do Board
	private BoardManager mBoardManager;
	#endregion

	void Start()
	{
		ScreenHeight = 0;
		ScreenWidth = 0;
		//BoardManager
		mBoardManager = BoardManager.instance;
	}

	// Update is called once per frame
	void Update()
	{
		if (ScreenHeight != Screen.height || ScreenWidth != Screen.width)
		{
			//Debug.Log("Change Resolution");
			//Redimensiona o board
			RecizeBoard();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	void RecizeBoard()
	{
		RectTransform BoardContainer = GameObject.Find("UIBoardTransform").GetComponent<RectTransform>();
		//Landscape
		if (Screen.width > Screen.height)
		{
			//Formula
			//Screen.Heigth / 2 - ( ( Screen.Heigth - ( Border.size * BoarderScale ) ) / 2 )
			// deixando o board com 80 do tamanho da tela em altura
			double scaleLandscape = (Screen.height * PorcentBordLandscape) / mBoardManager.BoardSize;
			//Aplica o scale no board
			float BoardSizeWithScale = (float)(mBoardManager.BoardSize * scaleLandscape);
			//Posicionamento do board
			float PositionLandscapeY = (Screen.height / 2) - (Screen.height - BoardSizeWithScale) / 2;
			//redimensiona o board
			BoardContainer.localScale = new Vector2((float)scaleLandscape, (float)scaleLandscape);
			BoardContainer.anchoredPosition = new Vector2(Screen.width - BorderLandscapeLeft, PositionLandscapeY);

			//posiciona a 15% do bottom da tela
			float TimeTextPositionXLandscape = (Screen.width - (BoardSizeWithScale + BorderLandscapeLeft)) / 2;
			TimeText.anchoredPosition = new Vector2(TimeTextPositionXLandscape, 0);
			//altera o anchor pressets
			TimeText.anchorMin = new Vector2(0, 0.5f);
			TimeText.anchorMax = new Vector2(0, 0.5f);
			TimeText.pivot = new Vector2(0.5f, 0.5f);
		}
		//Portrait
		else
		{
			//Multiplico a borda por dois, para a imagem conseguir ficar no meio
			float ScalePortrait = (Screen.width - (BorderPortrait * 2)) / mBoardManager.BoardSize;
			//Posicionamento do board
			double positionPortraitY = (Screen.height / 2) - (Screen.height * PorcentBorderTopPortrait) / 2;
			//redimensiona o board
			BoardContainer.localScale = new Vector2(ScalePortrait, ScalePortrait);
			BoardContainer.anchoredPosition = new Vector2(Screen.width - BorderPortrait, (float)positionPortraitY);

			//posiciona a 15% do bottom da tela
			TimeText.anchoredPosition = new Vector2(0, Screen.height * 0.15f);
			//altera o anchor pressets
			TimeText.anchorMin = new Vector2(0.5f, 0);
			TimeText.anchorMax = new Vector2(0.5f, 0);
			TimeText.pivot = new Vector2(0.5f, 0.5f);
		}
		//seta novamente o tamanho da tela
		ScreenHeight = Screen.height;
		ScreenWidth = Screen.width;
	}
}