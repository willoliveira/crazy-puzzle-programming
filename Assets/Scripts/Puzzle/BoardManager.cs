using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoardManager : MonoBehaviour
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
	//referencia da classe de movimento
	private MoveSquare moveSquare;
	//Ultima peça
	private Transform lastPiece;
	
	// Use this for initialization
	void Start()
	{
		//pega as colunas
		cropSize = (int)(image.width / columns);
		//pega o move square
		moveSquare = GetComponent<MoveSquare>();
		//inicia a lista de struct com as pecas
		listObjCropImages = new List<StructCrop>();
		//Corta a imagem
		CropImage();
		//Create pieces
		CreatePieces();
	}

	public void StartGame()
	{
		StartCoroutine(InitialGame());
	}

	private IEnumerator InitialGame()
	{
		float a = 1f;
		SpriteRenderer rendererLastPiece = lastPiece.GetComponent<SpriteRenderer>();
		while (true)
		{
			//se a peca ficar invisivel, para a rotina
			if (rendererLastPiece.color.a <= 0)
			{
				//Random pieces
				StartCoroutine(RandomPieces());
				break;
			}
			a -= 0.03f;
			rendererLastPiece.color = new Color(rendererLastPiece.color.r, rendererLastPiece.color.g, rendererLastPiece.color.b, a);
			yield return new WaitForSeconds(0.01f);
		}
		yield return null;
	}

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

	private void CreatePieces()
	{
		//aumenta o board GAMBS!!!
		Board.localScale = new Vector3(2, 2, 1);
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
			GameObject instance = Instantiate(SquareGameObject, new Vector3(row * Board.localScale.x, (columns - 1 - column) * Board.localScale.y), Quaternion.identity) as GameObject;
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
		//posicao vazia
		posBlank.x = 2;
		posBlank.y = 2;
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