using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
	//imagem de referencia
	public Texture2D image;
	//referencia de onde vai adicionar as peças
	public Transform Board;
	//quadrado de referencia
	public GameObject SquareGameObject;	
	//posição vazia
	public Vector2 posBlank;
	//numero de colunas
	[HideInInspector]
	public int columns = 3;

	struct StructCrop
	{
		public Texture2D crop;
		public int row;
		public int column;
	};

	//tamanho do recorte
	private int cropSize;	
	//Lista de crop images
	private List<StructCrop> listObjCropImages;
	//matriz game
	private int[,] matrizBoard;
	//referencia da classe de movimento
	private MoveSquare moveSquare;
	//Ultima peça
	private Transform lastPiece;



	// Use this for initialization
	void Start()
	{
		//pega as colunas
		cropSize = (int)(image.width / columns);
		//cria a matriz
		matrizBoard = new int[columns, columns];

		moveSquare = GetComponent<MoveSquare>();

		listObjCropImages = new List<StructCrop>();
		//Corta a imagem
		CropImage();

		//Create pieces
		CreatePieces();
	}
	
	private void CropImage()
	{
		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < columns; y++)
			{
				//se for no lugar da ultima peça não pega
				if ((columns - 1 - x) == 0 && y == 3) continue;
				//Pega os pixels do recorte que quero fazer
				Color[] pix = image.GetPixels(image.width - cropSize - ((columns - 1 - x) * cropSize), image.height - cropSize - ((y) * cropSize), cropSize, cropSize);
				//Cria uma textura com o recorte
				Texture2D squareTexture2d = new Texture2D(cropSize, cropSize);
				squareTexture2d.SetPixels(pix);
				squareTexture2d.wrapMode = TextureWrapMode.Clamp;
				squareTexture2d.Apply();
				//monta o struct
				StructCrop structCrop;
				structCrop.crop = squareTexture2d;
				structCrop.row = y;
				structCrop.column = x;
				//adiciona no array o recorte
				listObjCropImages.Add(structCrop);
			}
		}
	}

	private void CreatePieces()
	{
		//aumenta o board
		Board.localScale = new Vector3(2, 2, 1);
		//Assim que adiciona Component nos games object
		//SquareGameObject.AddComponent<SpriteRenderer>();
		SpriteRenderer squareSpriteRenderer;
		for (int cont = 0; cont < (columns * columns); cont++)
		{
			int row = Mathf.FloorToInt(cont / (columns));
			int column = cont % columns;
			//randomiza a imagem
			//int randomPosition = Random.Range(0, listObjCropImages.Count - 1); // quando era random na criacao
			int randomPosition = (row * columns) + column;
			StructCrop StructCropImage = listObjCropImages[randomPosition];
			Texture2D randomCropImage = StructCropImage.crop;
			//remove do array
			//listObjCropImages.Remove(StructCropImage); // quando era random na criacao
			//Seta a imagem como Sprite
			squareSpriteRenderer = SquareGameObject.GetComponent<SpriteRenderer>();
			squareSpriteRenderer.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width, randomCropImage.height), new Vector2(0, 0), cropSize);
			//Instancia o game object com a imagem recortada
			//GameObject instance = Instantiate(SquareGameObject, new Vector3(y * Board.localScale.x, (columns - 1 - row) * Board.localScale.y), Quaternion.identity) as GameObject;// quando era random na criacao
			GameObject instance = Instantiate(SquareGameObject, new Vector3(row * Board.localScale.x, (columns - 1 - column) * Board.localScale.y), Quaternion.identity) as GameObject;
			//instance.name = "square-" + x + "-" + y; // quando era no random na criacao
			instance.name = "square-" + column + "-" + row;
			//seta a linha e coluna que essa imagem pertence
			instance.GetComponent<Square>().Row = StructCropImage.row;
			instance.GetComponent<Square>().Column = StructCropImage.column;
			//Adiciona no Board
			instance.transform.SetParent(Board);
			//coloca a peça com escola 1x1
			instance.transform.localScale = new Vector3(1, 1, 1);
		}
		//pega a ultima peça
		lastPiece = Board.Find("square-2-2") as Transform;
		lastPiece.name = "lastPiece";

		posBlank.x = 2;
		posBlank.y = 2;
	}

	public void InitialGame()
	{
		//Random pieces
		StartCoroutine(RandomPieces());
	}

	//todo: colocar o label de onde as peças estao após a randomização
	private IEnumerator RandomPieces()
	{
		List<int> arrayPieces = new List<int>();
		//arrayPieces.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
		arrayPieces.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
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
				do {
					//randomiza a posição
					indexRandomPosition = Random.Range(0, arrayPieces.Count - 1);
				} while (indexRandomPosition == cont);
				//index da posição randomizada
				int valueRandomPosition = arrayPieces[indexRandomPosition],
				//linha e coluna da posição randomizada
					rowPos = (int)Mathf.Floor(valueRandomPosition / columns),
					columnPos = valueRandomPosition % columns;
				//movimenta a peça pra posição randomizada
				//cacheSquare.position = new Vector3(columnPos * Board.localScale.x, (columns - 1 - rowPos) * Board.localScale.x, 0);
				StartCoroutine(moveSquare.MovePieceSmooth(cacheSquare, new Vector3(columnPos * Board.localScale.x, (columns - 1 - rowPos) * Board.localScale.x, 0)));
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
			}
			if (cont == (columns * columns) - 1)
			{
				//depois que acabar o random das peças, libera o jogo
				moveSquare.enabled = true;
				RenamePieces();
			}
				
			yield return null;
		}
	}

	private void RenamePieces()
	{
		Square piece;
		for (int cont = 0, len = Board.childCount; cont < len; cont++)
		{
			//pega o filho
			Transform child = Board.GetChild(cont);
			//se for a ultima peça, ignora
			//if (child.name == "lastPiece") return;			
			//renomeia a peça pela posição que ela ocupa
			child.name = "square-" + (columns - child.localPosition.y - 1) + "-" + child.localPosition.x;
		}
	}
	
}