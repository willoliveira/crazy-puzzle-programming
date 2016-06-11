using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace PrototypingGame
{
	public class UIBoardManager : MonoBehaviour
	{
		#region PUBLIC VARS
		//imagem de referencia
		public Texture2D image;
		//referencia de onde vai adicionar as peças
		public Transform Board;
		//quadrado de referencia
		public GameObject SquareGameObject;
		//Drop area
		public GameObject DropArea;
		//posição vazia
		[HideInInspector]
		public Vector2 posBlank;
		public struct Blank
		{
			public int Row;
			public int Column;
		};
		[HideInInspector]
		public Blank PositionBlank;
		//numero de colunas
		[HideInInspector]
		public int columns;
		#endregion


		#region PRIVATE VARS
		private GameObject GameObjectPositionBlank;
		private struct StructCrop
		{
			public Texture2D crop;
			public int row;
			public int column;
		};
		//
		private GameManager mGameManger;
		//tamanho do recorte
		private int cropSize;
		//Lista de crop images
		private List<StructCrop> listObjCropImages;
		//referencia da classe de movimento
		private UIMoveSquare moveSquare;
		//Ultima peça
		private Transform lastPiece;
		//
		private RectTransform InstanceDropArea;
		#endregion

		// Use this for initialization
		void Start()
		{
			//pega o move square
			moveSquare = GetComponent<UIMoveSquare>();
			//inicia a lista de struct com as pecas
			listObjCropImages = new List<StructCrop>();
			//

			//Configura o board
			ConfigGame();
		}

		#region PUBLIC METHODS
		public void RecenterBoard()
		{
			//Vector3 worldPoint;
			//worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 5));
			//worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
			//Board.transform.position = worldPoint;

			Debug.Log("ScreenSize: " + new Vector3(Screen.width, Screen.height, 5));
			//Debug.Log("Recenter: " + GameObject.Find("Recenter"));
		}

		public void ResizeBoard()
		{

		}

		public void StartGame()
		{
			StartCoroutine(FadeInAndRandomPieces());
		}
		
		/// <summary>
		/// 
		/// 
		/// OBS: o X em coordenada no plano cartesiano, aqui ele carecterizado como coluna e o Y como linha, pois,
		///		movendo o objeto em X, você estara movendo ele horizontalmente, então, fazendo ele mudar a coluna, 
		///		e no Y, mudando Verticalmente, então a linha
		/// </summary>
		/// <param name="SquareGameObject"></param>
		/// <param name="DragDropArea"></param>
		public void SetPositionSquareBlank(GameObject SquareGameObject, GameObject DragDropArea)
		{
			//reparte a string do nome
			string[] arrName = SquareGameObject.name.Split(new string[] { "-", "" }, System.StringSplitOptions.None);
			//pega referencia do objeto que foi draggado
			RectTransform SquareRectTransform = SquareGameObject.GetComponent<RectTransform>();
			//posicao antes do square antes de ser draggado
			int PositionBeforeDragRow = int.Parse(arrName[1]),
				PositionBeforeDragColumn = int.Parse(arrName[2]),
				//posicao antes do square depois de ser draggado
				PostionAfterDragRow = Mathf.FloorToInt((SquareRectTransform.anchoredPosition.y * -1) / 100),
				PostionAfterDragColumn = Mathf.FloorToInt(SquareRectTransform.anchoredPosition.x / 100);
			//Atualiza o objecto do drop area e a posicao vazia
			InstanceDropArea.anchoredPosition = new Vector2((PositionBeforeDragColumn * 100) + 50, ((PositionBeforeDragRow) * -100) - 50);			
			PositionBlank.Row = PositionBeforeDragRow;
			PositionBlank.Column = PositionBeforeDragColumn;
			//Atualiza a propriedade de linha e colona do square
			Square Square = SquareGameObject.GetComponent<Square>();
			Square.Row = PostionAfterDragColumn;
			Square.Column = PostionAfterDragRow;
			////Normaliza o nome do square
			Square.NormalizePieceName();
			//Ativa/Desativa o dragg das pecas
			ToogleDrag();
		}

		/// <summary>
		/// posBlank x referente a linha
		///			 y referente a coluna
		/// Habilita e desabilita o drag das peças
		/// </summary>
		public void ToogleDrag()
		{
			for (int cont = 0; cont < (columns * columns); cont++)
			{
				int row = Mathf.FloorToInt(cont / (columns));
				int column = cont % columns;
				//pega o gameobject do quadrado
				Transform TransformSquare = Board.Find("square-" + row + "-" + column);
				if (TransformSquare)
				{
					if (checkNeighbors(row, column, (int)PositionBlank.Row, (int)PositionBlank.Column))
					{
						//Habilita o drag
						TransformSquare.GetComponent<UIDragAndDrop>().EnabledDrag = true;
					}
					else
					{
						//Desabilita o drag
						TransformSquare.GetComponent<UIDragAndDrop>().EnabledDrag = false;
					}
				}
			}
		}
		#endregion

		#region PRIVATE METHODS

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
		/// Configura o jogo com base nas escolhas
		/// </summary>
		private void ConfigGame()
		{
			//
			mGameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
			//Seta a dificuldade do jogo
			if (mGameManger.mDiffilcultyMode == DiffilcultyMode.Normal)
			{
				columns = 3;
			}
			else if (mGameManger.mDiffilcultyMode == DiffilcultyMode.Hard)
			{
				columns = 4;
			}
			//pega o tamanho do recorte pelo numero de colunas
			cropSize = (int)(image.width / columns);
			//TODO: Preciso fazer os modos de jogo ainda
			if (mGameManger.mSelectMode == SelectMode.Image)
			{
				//Corta a imagem
				CropImage();
				//Create pieces
				CreatePieces();
			}
		}
		/// <summary>
		/// Inicia o game comecando dando um fade da ultima peca e randomizacao as pecas
		/// </summary>

		/// <summary>
		/// Faz o fade e randomiza as pecas, em sequencia
		/// </summary>
		/// <returns></returns>
		private IEnumerator FadeInAndRandomPieces()
		{
			//some com a ultima
			yield return StartCoroutine(Fade(lastPiece.GetComponent<Image>(), 0.03f));
			//
			lastPiece.gameObject.SetActive(false);
			//randomiza
			//yield return StartCoroutine(RandomPieces());
			RandomPieces();
		}
		/// <summary>
		/// Faz um fade na peca
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
			while ((fade && rendererLastPiece.color.a != 1f) || (!fade && rendererLastPiece.color.a > 0))
			{
				//se a peca ficar invisivel, para a rotina
				if ((fade && rendererLastPiece.color.a == 1f) || (!fade && rendererLastPiece.color.a <= 0))
				{
					break;
				}
				//a += fadeAmount;
				a -= 0.03f;
				rendererLastPiece.color = new Color(rendererLastPiece.color.r, rendererLastPiece.color.g, rendererLastPiece.color.b, a);
				//yield return new WaitForSeconds(5f);
				yield return new WaitForSeconds(0.01f);
			}
			yield return null;
		}
		/// <summary>
		/// Cropa a imagem
		/// </summary>
		private void CropImage()
		{
			for (int cont = 0; cont < (columns * columns); cont++)
			{
				int row = Mathf.FloorToInt(cont / (columns));
				int column = cont % columns;
				//Pega os pixels do recorte que quero fazer
				Color[] pix = image.GetPixels(image.width - cropSize - ((columns - 1 - row) * cropSize), image.height - cropSize - (column * cropSize), cropSize, cropSize);
				//Cria uma textura com o recorte
				Texture2D squareTexture2d = new Texture2D(cropSize, cropSize);
				squareTexture2d.SetPixels(pix);
				squareTexture2d.wrapMode = TextureWrapMode.Clamp;
				squareTexture2d.Apply();
				//monta o struct
				StructCrop structCrop;
				structCrop.crop = squareTexture2d;
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
			for (int cont = 0; cont < (columns * columns); cont++)
			{
				int row = Mathf.FloorToInt(cont / (columns));
				int column = cont % columns;
				//randomiza a imagem
				int randomPosition = (row * columns) + column;
				StructCrop StructCropImage = listObjCropImages[randomPosition];
				Texture2D randomCropImage = StructCropImage.crop;
				//Seta a imagem como Sprite
				squareImage = SquareGameObject.GetComponent<Image>();
				squareImage.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width, randomCropImage.height), new Vector2(0, 0), cropSize);
				//Instancia o game object com a imagem recortada
				instance = Instantiate(SquareGameObject, new Vector3((row * 100) + 50, ((column) * -100) - 50), Quaternion.identity) as GameObject;
				instance.name = "square-" + column + "-" + row;
				//seta a linha e coluna que essa imagem pertence
				instance.GetComponent<Square>().Row = StructCropImage.row;
				instance.GetComponent<Square>().Column = StructCropImage.column;
				//Adiciona no Board
				instance.transform.SetParent(Board, false);
			}
			//onde a peca vazia esta
			PositionBlank.Row = columns - 1;
			PositionBlank.Column = columns - 1;
			//Instancia a posica da peca vazia
			instance = Instantiate(DropArea, new Vector3(((columns - 1) * 100) + 50, ((columns - 1) * -100) - 50, 0), Quaternion.identity) as GameObject; // 50 por causa que é a metade da peca do puzzle
			instance.transform.SetParent(Board, false);
			//Pega a referencia do RectTransform da area de Drop e guarda
			InstanceDropArea = instance.GetComponent<RectTransform>();
			//rename na ultima peça
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
		/// Randomiza as pecas antes do inicio do jogo
		/// 
		/// TODO: liberar o game realmente após acabar o randmo, se não me engano ele não estao acabando o tempo mudou por conta das animacoes, arrumar isso
		/// TODO: Esse metodo que esta cagando o unity e fazendo ele fechar
		/// </summary>
		/// <returns></returns>
		private void RandomPieces()
		{
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
						//Debug.Log("do while");
						//TODO quem sabe num é esse while que ta cagando tudo... Talvez nao...
						//randomiza a posição
						indexRandomPosition = Random.Range(0, arrayPieces.Count - 1);
					} while (arrayPieces[indexRandomPosition] == cont);
					//index da posição randomizada
					int valueRandomPosition = arrayPieces[indexRandomPosition];
					//linha e coluna da posição randomizada
					int rowPosRandomized = (int)Mathf.Floor(valueRandomPosition / columns);
					int columnPosRandomized = valueRandomPosition % columns;
					//vetor com a posição final do recorte
					Vector3 posEnd = new Vector3((columnPosRandomized * 100) + 50, ((rowPosRandomized) * -100) - 50);
					//preenche a coluna e linha dessa peca
					cacheSquare.GetComponent<Square>().Row = rowPosRandomized;
					cacheSquare.GetComponent<Square>().Column = columnPosRandomized;
#if UNITY_EDITOR
					//alternativa para quando nao quiser animacao
					cacheSquare.anchoredPosition = posEnd;
#else
					//anima e move a peca
					StartCoroutine(moveSquare.AnimateAndMoveSmooth(cacheSquare, posEnd));
#endif
					//remove do array
					arrayPieces.Remove(valueRandomPosition);
					//espera um pouco
					//yield return new WaitForSeconds(0.5f);
				}
				else
				{
					//seta a posição vazia
					PositionBlank.Row = columns - 1;
					PositionBlank.Column = columns - 1;
					//Seta a posicao na area de drop das pecas
					InstanceDropArea.anchoredPosition = new Vector3(((columns - 1) * 100) + 50, ((columns - 1) * -100) - 50, 0);
				}
				//TODO: Tentar fazer isso depois somente depois que as animacoes acabarem
				if (cont == (columns * columns) - 1)
				{
					//depois que acabar o random das peças, libera o jogo
					moveSquare.enabled = true;
					NormalizePiece();
					ToogleDrag();
				}
				//yield return null;
			}
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
		#endregion
	}
}