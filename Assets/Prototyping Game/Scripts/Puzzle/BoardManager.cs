using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PrototypingGame
{
	public class BoardManager : MonoBehaviour
	{
		#region PUBLIC VARS
		//imagem de referencia
		public Texture2D image;
		//referencia de onde vai adicionar as peças
		public Transform Board;
		//quadrado de referencia
		public GameObject SquareGameObject;

		//posição vazia
		[HideInInspector]
		public Vector2 posBlank;
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
		private MoveSquare moveSquare;
		//Ultima peça
		private Transform lastPiece;
		#endregion

		// Use this for initialization
		void Start()
		{
			//pega o move square
			moveSquare = GetComponent<MoveSquare>();
			//inicia a lista de struct com as pecas
			listObjCropImages = new List<StructCrop>();
			//Configura o board
			ConfigGame();
		}

		#region PUBLIC METHODS
		public void StartGame()
		{
			StartCoroutine(FadeInAndRandomPieces());
		}

		public void SetPositionSquareBlank(GameObject SquareGameObject, Vector3 pos)
		{
			//Atualiza a posicao da peca vazia
			GameObjectPositionBlank.transform.position = pos;
			//Atualiza a posicao vazia
			posBlank.x = columns - 1 - pos.y;
			posBlank.y = pos.x;
			//Atualiza a propriedade de linha e colona do square
			Square Square = SquareGameObject.GetComponent<Square>();
			Square.Column = (int)SquareGameObject.transform.position.x;
			Square.Row = columns - 1 - (int)SquareGameObject.transform.position.y;
			//Normaliza o nome do square
			Square.NormalizePieceName();
			//
			ToogleDrag();
		}
		/// <summary>
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
					if (checkNeighbors(row, column, (int)posBlank.x, (int)posBlank.y))
					{
						//Habilita o drag
						TransformSquare.GetComponent<DragAndDrop>().EnabledDrag = true;
					}
					else
					{
						//Desabilita o drag
						TransformSquare.GetComponent<DragAndDrop>().EnabledDrag = false;
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
			yield return StartCoroutine(Fade(lastPiece.GetComponent<SpriteRenderer>(), 0.03f));
			//randomiza
			yield return StartCoroutine(RandomPieces());
		}
		/// <summary>
		/// Faz um fade na peca
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="fadeAmount"></param>
		/// <returns></returns>
		private IEnumerator Fade(SpriteRenderer sprite, float fadeAmount)
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
			SpriteRenderer rendererLastPiece = sprite.GetComponent<SpriteRenderer>();
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
			//TODO: mudar isso para o responsivo com o onGui
			//posicao vazia
			//ta meio na gambs, mudar isso depois
			//criacao de uma peca pra ser o espaço vazio
			GameObjectPositionBlank = new GameObject();
			GameObjectPositionBlank.AddComponent<BoxCollider2D>();
			GameObjectPositionBlank.GetComponent<BoxCollider2D>().offset = new Vector2(.5f, .5f);
			GameObjectPositionBlank.GetComponent<BoxCollider2D>().isTrigger = true;
			GameObjectPositionBlank.transform.SetParent(Board);
			GameObjectPositionBlank.transform.localScale = new Vector3(1, 1, 1);

			SpriteRenderer squareSpriteRenderer;
			for (int cont = 0; cont < (columns * columns); cont++)
			{
				int row = Mathf.FloorToInt(cont / (columns));
				int column = cont % columns;
				//randomiza a imagem
				int randomPosition = (row * columns) + column;
				StructCrop StructCropImage = listObjCropImages[randomPosition];
				Texture2D randomCropImage = StructCropImage.crop;
				//remove do array
				//Seta a imagem como Sprite
				squareSpriteRenderer = SquareGameObject.GetComponent<SpriteRenderer>();
				squareSpriteRenderer.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width, randomCropImage.height), new Vector2(0, 0), cropSize);
				//Instancia o game object com a imagem recortada
				GameObject instance = Instantiate(SquareGameObject, new Vector3(row, (columns - 1 - column)), Quaternion.identity) as GameObject;
				instance.name = "square-" + column + "-" + row;
				//seta a linha e coluna que essa imagem pertence
				instance.GetComponent<Square>().Row = StructCropImage.row;
				instance.GetComponent<Square>().Column = StructCropImage.column;
				//
				instance.GetComponent<DragAndDrop>().Drop = GameObjectPositionBlank;
				//Adiciona no Board
				instance.transform.SetParent(Board);
				//coloca a peça com escola 1x1
				instance.transform.localScale = new Vector3(1, 1, 1);
			}
			//
			posBlank.x = columns - 1;
			posBlank.y = columns - 1;
			//arruma a posica da peca vazia
			GameObjectPositionBlank.transform.position = new Vector3(columns - 1, 0, 0);
			GameObjectPositionBlank.name = "PositionBlank";
			//TODO: mudar isso para o responsivo com o onGui
			//Board.localScale = new Vector3(2, 2, 1);
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
		/// </summary>
		/// <returns></returns>
		private IEnumerator RandomPieces()
		{
			//preenche o array de apoio para ajudar no random
			List<int> arrayPieces = new List<int>();
			for (int cont = 0; cont < (columns * columns); cont++)
				arrayPieces.Add(cont);
			//arrayPieces.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
			//varre as peças
			for (int cont = 0; cont < (columns * columns); cont++)
			{
				int row = Mathf.FloorToInt(cont / (columns));
				int column = cont % columns;
				//Rigidbody2D cacheSquare = Board.Find("square-" + x + "-" + y).GetComponent<Rigidbody2D>();
				Transform cacheSquare = Board.Find("square-" + row + "-" + column);
				if (cacheSquare != null)
				{
					int indexRandomPosition;
					//não deixa a posição ser a mesma da posição atual
					do
					{
						//randomiza a posição
						indexRandomPosition = Random.Range(0, arrayPieces.Count - 1);
					} while (arrayPieces[indexRandomPosition] == cont);
					//} while (indexRandomPosition == cont);
					//index da posição randomizada
					int valueRandomPosition = arrayPieces[indexRandomPosition];
					//linha e coluna da posição randomizada
					int rowPosRandomized = (int)Mathf.Floor(valueRandomPosition / columns);
					int columnPosRandomized = valueRandomPosition % columns;
					//vetor com a posição final do recorte
					Vector3 posEnd = new Vector3(columnPosRandomized * Board.localScale.x, (columns - 1 - rowPosRandomized) * Board.localScale.x);
					//preenche a coluna e linha dessa peca
					cacheSquare.GetComponent<Square>().Row = rowPosRandomized;
					cacheSquare.GetComponent<Square>().Column = columnPosRandomized;
					//anima e move a peca
					StartCoroutine(moveSquare.AnimateAndMoveSmooth(cacheSquare, posEnd));
					//remove do array
					arrayPieces.Remove(valueRandomPosition);
					//espera um pouco
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					//seta a posição vazia
					posBlank.x = columns - 1;
					posBlank.y = columns - 1;
					GameObjectPositionBlank.transform.position = new Vector3(columns - 1, 0, 0);
					GameObjectPositionBlank.transform.localScale = new Vector3(1, 1, 1);
				}
				yield return null;
				if (cont == (columns * columns) - 1)
				{
					//depois que acabar o random das peças, libera o jogo
					moveSquare.enabled = true;
					NormalizePiece();
					ToogleDrag();
				}

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