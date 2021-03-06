﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
	public struct Blank
	{
		public int Row;
		public int Column;
	};
	private struct StructCrop
	{
		public Sprite crop;
		public int row;
		public int column;
	};

	#region PUBLIC VARS
	[HideInInspector]
	public static BoardManager instance;
	
	//referencia de onde vai adicionar as peças
	public Transform Board;
	//quadrado de referencia
	public GameObject SquareGameObject;
	//Drop area
	public GameObject DropArea;
	//Referencia da classe de timer
	public Timer mTimer;
	//Referencia da classe de movimento
	public MoveSquare mMoveSquare;
	//Janela de finish
	public FinishScreen mFinishScreen;

	public GameObject InitGame;
	public GameObject InGame;
	
	#region CONFIG BOARD
	//tamanho do board
	public int BoardSize = 300;
	//tamanho das pecas
	public int PieceSize;
	//numero de colunas
	[HideInInspector]
	public int columns;
	//posição vazia
	[HideInInspector]
	public Blank PositionBlank;
	#endregion

	#region TESTES UI
	//screen size
	public Text ScreenSizeText;
	#endregion

	#endregion

	#region PRIVATE VARS
	//GameObjectPositionBlank
	private GameObject GameObjectPositionBlank;
	//mGameManager
	private GameManager mGameManager;
	//tamanho do recorte
	private int cropSize;
	//Lista de crop images
	private List<StructCrop> listObjCropImages;
	//Ultima peça
	private Transform lastPiece;
	//InstanceDropArea
	private RectTransform InstanceDropArea;
	#endregion

	void Awake()
	{
		//guarda referencia
		instance = this;
		//tira o mult touch
		Input.multiTouchEnabled = false;
	}

	void Start()
	{
		mGameManager = GameManager.instance;
		
		//inicia a lista de struct com as pecas
		listObjCropImages = new List<StructCrop>();
		//Configura o board
		ConfigGame();
		//
		InGame.SetActive(false);
	}

	#region PUBLIC METHODS
	/// <summary>
	/// Inicia o game comecando dando um fade da ultima peca e randomizacao as pecas
	/// </summary>
	public void StartGame()
	{
		//Altamente provisorio, se for ser assim, fazer direito.
		InitGame.SetActive(false);
		InGame.SetActive(false);
		//Limpa o tempo
		mTimer.ClearTimer();
		mTimer.IsEnable = false;
		//Começa o jogo
		StartCoroutine(FadeInAndRandomPieces());
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="pause"></param>
	public void PauseGame(bool pause)
	{
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//pega o gameobject do quadrado
			Transform TransformSquare = Board.Find("square-" + row + "-" + column);
			if (TransformSquare)
			{
				TransformSquare.GetComponent<DragAndDropUI>().EnabledDrag = !pause;
			}
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <summary>
	/// OBS: o X em coordenada no plano cartesiano, aqui ele carecterizado como coluna e o Y como linha, pois,
	///		movendo o objeto em X, você estara movendo ele horizontalmente, então, fazendo ele mudar a coluna, 
	///		e no Y, mudando Verticalmente, então a linha
	/// </summary>
	/// <param name="SquareGameObject"></param>
	/// <param name="DragDropArea"></param>
	public void SetPositionSquareBlank(GameObject SquareGameObject)
	{
		//reparte a string do nome
		string[] arrName = SquareGameObject.name.Split(new string[] { "-", "" }, System.StringSplitOptions.None);
		//pega referencia do objeto que foi draggado
		RectTransform SquareRectTransform = SquareGameObject.GetComponent<RectTransform>();
		//posicao antes do square antes de ser draggado
		int PositionBeforeDragRow = int.Parse(arrName[1]),
			PositionBeforeDragColumn = int.Parse(arrName[2]),
			//posicao antes do square depois de ser draggado
			PostionAfterDragRow = Mathf.FloorToInt((SquareRectTransform.anchoredPosition.y * -1) / PieceSize),
			PostionAfterDragColumn = Mathf.FloorToInt(SquareRectTransform.anchoredPosition.x / PieceSize);
		//Atualiza o objecto do drop area e a posicao vazia
		InstanceDropArea.anchoredPosition = new Vector2((PositionBeforeDragColumn * PieceSize) + PieceSize / 2, ((PositionBeforeDragRow) * -PieceSize) - PieceSize / 2);
		PositionBlank.Row = PositionBeforeDragRow;
		PositionBlank.Column = PositionBeforeDragColumn;
		//Atualiza a propriedade de linha e colona do square
		Square Square = SquareGameObject.GetComponent<Square>();
		Square.Row = PostionAfterDragRow;
		Square.Column = PostionAfterDragColumn;
		//Normaliza o nome do square
		Square.NormalizePieceName();

		//Testa se acertou tudo
		if (ValidBoard())
		{
			mTimer.IsEnable = false;
			StartCoroutine(FadeInAndFinishGame());
		}
		else if (mGameManager.mGameMode == GameMode.Classic || mGameManager.mGameMode == GameMode.Hard)
		{
			//Ativa/Desativa o dragg das pecas
			ToogleDrag();
		}
	}
	/// <summary>
	/// depois que acabar o random das peças, libera o jogo
	/// </summary>
	public void EndRandomPieces()
	{
		
		//ativa as opções in game
		InGame.SetActive(true);
		//habilita a movimentação
		mMoveSquare.IsEnable = true;
		//Limpa o tempo e começa a contagem
		mTimer.ClearTimer();
		mTimer.IsEnable = true;
		//normaliza o nome das peças
		NormalizePiece();
		//Habilita/desabilita o drag
		ToogleDrag();
	}
	#endregion

	#region PRIVATE METHODS

	#region BOARDMANAGER TOOLS
	/// <summary>
	/// Valida se o jogador acabou de montar o board
	/// </summary>
	/// <returns>Retorna se montou ou nao</returns>
	private bool ValidBoard()
	{
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//pega o gameobject do quadrado
			Transform TransformSquare = Board.Find("square-" + row + "-" + column);
			if (TransformSquare)
			{
				Square square = TransformSquare.GetComponent<Square>();
				if (square.Row != square.RowCorrect || square.Column != square.ColumnCorrect)
				{
					return false;
				}
			}
		}
		return true;
	}
	/// <summary>
	/// 
	/// posBlank x referente a linha
	///			 y referente a coluna
	/// Habilita e desabilita o drag das peças
	/// 
	/// TODO: Adaptar o metodo para o modo livre, em que ele não precisa validar os vizinhos e nao desliga nenhum drag. Talvez ate nao precise ser aqui, ser em quem chama esse metodo
	/// </summary>
	/// <param name="DesactiveAll"></param>mMoveSquare
	private void ToogleDrag(bool DesactiveAll = false)
	{
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//pega o gameobject do quadrado
			Transform TransformSquare = Board.Find("square-" + row + "-" + column);
			if (TransformSquare)
			{
				//se for pra desabilitar tudo
				if (DesactiveAll)
				{
					//Desabilita o drag
					TransformSquare.GetComponent<DragAndDropUI>().EnabledDrag = false;
				}
				//caso for pra habilitar e for o FREE Mode
				else if (mGameManager.mGameMode == GameMode.Free)
				{
					TransformSquare.GetComponent<DragAndDropUI>().EnabledDrag = true;
				}
				//Caso contrario, só habilita os vizinho
				else
				{
					if (checkNeighbors(row, column, (int)PositionBlank.Row, (int)PositionBlank.Column))
					{
						//Habilita o drag
						TransformSquare.GetComponent<DragAndDropUI>().EnabledDrag = true;
					}
					else
					{
						//Desabilita o drag
						TransformSquare.GetComponent<DragAndDropUI>().EnabledDrag = false;
					}
				}
			}
		}
	}
	/// <summary>
	/// Verifica os vizinhos das coordenadas que estou passando
	/// </summary>
	/// <param name="rowNeighbor"></param>
	/// <param name="columnNeighbor"></param>
	/// <param name="rowMe"></param>
	/// <param name="columnMe"></param>
	/// <returns></returns>
	private bool checkNeighbors(int rowNeighbor, int columnNeighbor, int rowMe, int columnMe)
	{
		//Se estiver na mesma linha
		if (rowNeighbor == rowMe)
		{
			//Se minha coluna for maior que do meu vizinh0
			if (columnMe > columnNeighbor)
			{
				//testa se eu estou atras
				if (columnNeighbor + 1 == columnMe) return true;
			}
			else
			{
				//testa se eu estou a frente
				if (columnNeighbor - 1 == columnMe) return true;
			}
		}
		//Se estiver na mesma coluna
		else if (columnNeighbor == columnMe)
		{
			//Se minha linha for maior que do meu vizinho
			if (rowMe > rowNeighbor)
			{
				//testa se eu estou em baixo
				if (rowNeighbor + 1 == rowMe) return true;
			}
			else
			{
				//testa se eu estou em cima
				if (rowNeighbor - 1 == rowMe) return true;
			}
		}
		return false;
	}
	/// <summary>
	/// Quando o game termina
	/// </summary>
	private void FinishGame()
	{
		//Desativa o timer
		mTimer.IsEnable = false;
		//Desativa o drag de todas a pecas
		ToogleDrag(true);
		//Mostra o feed
		mFinishScreen.Show(mTimer.TimerFormatted());
	}
	#endregion

	#region ORDEM DE EXECUCAO START GAME
	/// <summary>
	/// Configura o jogo com base nas escolhas
	/// </summary>
	private void ConfigGame()
	{
		//Seta a dificuldade do jogo
		if (mGameManager.mGameMode == GameMode.Hard)
		{
			columns = 4;
		}
		else
		{
			columns = 3;
		}
		PieceSize = BoardSize / columns;
		//seta a escala da drop area do board
		DropArea.GetComponent<RectTransform>().localScale = new Vector2((float)PieceSize / 100, (float)PieceSize / 100);
		//pega o tamanho do recorte pelo numero de colunas
		cropSize = (int)(mGameManager.ImageCropRect.width / columns);
		//Corta a imagem
		CropImage();
		//Create pieces
		CreatePieces();
	}
	/// <summary>
	/// Corta a image
	/// TODO: Talvez separar isso em uma classe, assim qualquer outra coisa no game podera usar isso
	/// </summary>
	private void CropImage()
	{
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//monta o struct
			StructCrop structCrop;
			//Eu recorto assim pra conseguir seguir a orientacao de uma matriz, pra conseguir marcar qual peça é qual, assim consigo ver se o cara terminou o quebra-cabeça
			//soma a posicao do retangulo em o retangulo de crop estao: + mGameManager.ImageCropRect.x e + mGameManager.ImageCropRect.y
			structCrop.crop = Sprite.Create(mGameManager.ImageSelect, 
				new Rect(mGameManager.ImageCropRect.width - cropSize - ((columns - 1 - row) * cropSize) + mGameManager.ImageCropRect.x, 
						 mGameManager.ImageCropRect.height - cropSize - (column * cropSize) + mGameManager.ImageCropRect.y, 
						 cropSize, 
						 cropSize),
				new Vector2(0, 0));
			structCrop.row = column;
			structCrop.column = row;
			//adiciona no array o recorte
			listObjCropImages.Add(structCrop);
		}
	}
	/// <summary>
	/// Cria as pecas com base nos recortes
	/// </summary>
	private void CreatePieces()
	{
		GameObject instance;
		Image squareImage;
		RectTransform BoardRectTrasnform = Board.GetComponent<RectTransform>();
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//randomiza a imagem
			int randomPosition = (row * columns) + column;
			StructCrop StructCropImage = listObjCropImages[randomPosition];
			//Texture2D randomCropImage = StructCropImage.crop;
			Sprite randomCropImage = StructCropImage.crop;
			//
			SquareGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(PieceSize, PieceSize);

			//Seta a imagem como Sprite
			squareImage = SquareGameObject.GetComponent<Image>();
			squareImage.sprite = randomCropImage;
			//squareImage.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width * Board.localScale.x, randomCropImage.height * Board.localScale.x), new Vector2(0, 0), cropSize);
			//Instancia o game object com a imagem recortada
			instance = Instantiate(SquareGameObject, new Vector3((row * PieceSize) + (PieceSize / 2), (column * -PieceSize) - (PieceSize / 2)), Quaternion.identity) as GameObject;
			instance.name = "square-" + column + "-" + row;

			DragAndDropUI SquareDragAndDropUI = instance.GetComponent<DragAndDropUI>();
			SquareDragAndDropUI.ParentRT = BoardRectTrasnform;

			//seta a linha e coluna que essa imagem 
			Square CacheSquare = instance.GetComponent<Square>();
			CacheSquare.Row = CacheSquare.RowCorrect = StructCropImage.row;
			CacheSquare.Column = CacheSquare.ColumnCorrect = StructCropImage.column;
			//Adiciona no Board
			instance.transform.SetParent(Board, false);
			instance.transform.SetAsFirstSibling();
		}
		//onde a peca vazia esta
		PositionBlank.Row = columns - 1;
		PositionBlank.Column = columns - 1;
		//Instancia a posica da peca vazia
		instance = Instantiate(DropArea, new Vector3(((columns - 1) * PieceSize) + PieceSize / 2, ((columns - 1) * -PieceSize) - PieceSize / 2, 0), Quaternion.identity) as GameObject; // 50 por causa que é a metade da peca do puzzle
		instance.name = "DropArea";
		instance.transform.SetParent(Board, false);
		instance.transform.SetAsLastSibling();
		//Pega a referencia do RectTransform da area de Drop e guarda
		InstanceDropArea = instance.GetComponent<RectTransform>();
		//seta no SquareDrop uma referencia do drop
		SquareDrop.DropArea = InstanceDropArea;
		//rename na ultima peça
		renameLastPiece();
	}
	/// <summary>
	/// Faz o fade e randomiza as pecas, em sequencia
	/// </summary>
	/// <returns></returns>
	private IEnumerator FadeInAndRandomPieces()
	{
		//some com a ultima
		if ((int)lastPiece.GetComponent<Image>().color.a == 1)
			yield return StartCoroutine(Fade(lastPiece.GetComponent<Image>(), 0.03f));
		else
			yield return null;
		//
		//lastPiece.gameObject.SetActive(false);
		//randomiza
		//yield return StartCoroutine(RandomPieces());
		RandomPieces();
	}
	/// <summary>
	/// Cropa a imagem
	/// </summary>
	/// <summary>
	/// Randomiza as pecas antes do inicio do jogo
	/// 
	/// TODO: liberar o game realmente após acabar o random, se não me engano ele não estao acabando o tempo mudou por conta das animacoes, arrumar isso
	/// </summary>
	/// <returns></returns>
	private void RandomPieces()
	{
		//seta o inicio da randomizacao
		mMoveSquare.InitRandomPieces();
		//preenche o array de apoio para ajudar no random
		List<int> arrayPieces = new List<int>();
		for (int cont = 0; cont < (columns * columns); cont++)
			arrayPieces.Add(cont);
		//varre as peças
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//referencia do objeto
			RectTransform cacheSquare = Board.Find("square-" + row + "-" + column) as RectTransform;
			if (cacheSquare != null)
			{
				int indexRandomPosition;
				//não deixa a posição ser a mesma da posição atual
				do
				{
					//TODO: quem sabe é esse while que ta cagando tudo... [UPDATE] Talvez nao... [UPDATE 1] era esse while mesmo que tava travando meu jogo
					//randomiza a posição
					indexRandomPosition = Random.Range(0, arrayPieces.Count - 1);
					//se ele repetir a peca apenas na ultima, deixa queto. deixar pensar em algo pra isso
					if (arrayPieces.Count == 2)
					{
						break;
					}
				} while (arrayPieces[indexRandomPosition] == cont);

				//index da posição randomizada
				int valueRandomPosition = arrayPieces[indexRandomPosition];
				//linha e coluna da posição randomizada
				int rowPosRandomized = (int)Mathf.Floor(valueRandomPosition / columns);
				int columnPosRandomized = valueRandomPosition % columns;
				//vetor com a posição final do recorte
				Vector3 posEnd = new Vector3((columnPosRandomized * PieceSize) + PieceSize / 2, ((rowPosRandomized) * -PieceSize) - PieceSize / 2);
				//preenche a coluna e linha dessa peca
				cacheSquare.GetComponent<Square>().Row = rowPosRandomized;
				cacheSquare.GetComponent<Square>().Column = columnPosRandomized;
				//#if UNITY_EDITOR
				//					//alternativa para quando nao quiser animacao
				//					cacheSquare.anchoredPosition = posEnd;
				//#else
				//anima e move a peca
				StartCoroutine(mMoveSquare.AnimateAndMoveSmooth(cacheSquare, posEnd));
				//#endif
				//remove do array
				arrayPieces.Remove(valueRandomPosition);
			}
			else
			{
				//seta a posição vazia
				PositionBlank.Row = columns - 1;
				PositionBlank.Column = columns - 1;
				//Seta a posicao na area de drop das pecas
				InstanceDropArea.anchoredPosition = new Vector3(((columns - 1) * PieceSize) + PieceSize / 2, ((columns - 1) * -PieceSize) - PieceSize / 2, 0);
			}
		}
	}
	/// <summary>
	/// Faz um fade na peca
	/// 
	/// TODO: Posso separar esse metodo depois em uma classe, assim qualquer coisa no jogo poderá usar ele
	/// </summary>
	/// <param name="sprite"></param>
	/// <param name="fadeAmount"></param>
	/// <returns></returns>
	private IEnumerator Fade(Image sprite, float fadeAmount)
	{
		bool fade;
		float a;
		//verifica se é fadeIn ou fadeOut
		if (sprite.color.a == 0)
		{
			fade = true;
			a = 0f;
		}
		else
		{
			fade = false;
			a = 1f;
			fadeAmount *= -1f;
		}
		Image rendererLastPiece = sprite.GetComponent<Image>();
		while ((fade && rendererLastPiece.color.a < 1) || (!fade && rendererLastPiece.color.a > 0))
		{
			a += fadeAmount;
			rendererLastPiece.color = new Color(rendererLastPiece.color.r, rendererLastPiece.color.g, rendererLastPiece.color.b, (float)a);
			//se a peca ficar invisivel, para a rotina
			if ((fade && rendererLastPiece.color.a >= 1f) || (!fade && rendererLastPiece.color.a <= 0))
			{
				if (fade) a = 1f;
				else a = 0f;
				//seta um valor de alpha arredondado
				rendererLastPiece.color = new Color(rendererLastPiece.color.r, rendererLastPiece.color.g, rendererLastPiece.color.b, (float)a);
				break;
			}
			//yield return new WaitForSeconds(5f);
			yield return new WaitForSeconds(0.02f);
		}
		yield return null;
	}
	/// <summary>
	/// Normaliza os nomes das pecas
	/// </summary>
	private void NormalizePiece()
	{
		for (int cont = 0, len = Board.childCount; cont < len; cont++)
		{
			//pega o filho
			Square childSquare = Board.GetChild(cont).GetComponent<Square>() as Square;
			if (childSquare)
			{
				//normaliza os nomes
				childSquare.NormalizePieceName();
			}
		}
		renameLastPiece();
	}
	/// <summary>
	/// Renomeia a ultima peca
	/// </summary>
	private void renameLastPiece()
	{
		//pega a ultima peça
		lastPiece = Board.Find("square-" + (columns - 1) + "-" + (columns - 1)) as Transform;
		lastPiece.name = "lastPiece";
	}
	/// <summary>
	/// Volta a peça ao normal e finaliza o jogo
	/// </summary>
	/// <returns></returns>
	private IEnumerator FadeInAndFinishGame()
	{
		//some com a ultima
		yield return StartCoroutine(Fade(lastPiece.GetComponent<Image>(), 0.03f));
		//espero um pouco
		yield return new WaitForSeconds(1.5f);
		//chama o fim do jogo
		FinishGame();
	}
	#endregion

	#endregion
}