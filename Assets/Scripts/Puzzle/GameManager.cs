using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    //imagem de referencia
    public Texture2D image;
    //referencia de onde vai adicionar as peças
    public Transform Board;
    //quadrado de referencia
    public GameObject SquareGameObject;

    struct StructCrop
    {
        public Texture2D crop;
        public int row;
        public int column;
    };

    //tamanho do recorte
    private int cropSize = 200;
    //numero de colunas
    private int columns = 4;
    //Lista de crop images
    private List<Texture2D> arrCropImages;
    private List<StructCrop> listObjCropImages;
    //matriz game
    private int[,] matrizBoard;
    private GameObject[,] matrizPieces;
    //posição vazia
    private Vector2 posBlank;

    private bool axisHorizontalDown = false;
    private bool axisVerticalDown = false;

    // Use this for initialization
    void Start()
    {
        //pega as colunas
        cropSize = (int) (image.width / columns);
        //cria a matriz
        matrizBoard = new int[columns, columns];
        matrizPieces = new GameObject[columns, columns];

        arrCropImages = new List<Texture2D>();
        listObjCropImages = new List<StructCrop>();
        //Corta a imagem
        CropImage();
        //Random
        RandomPieces();
    }

    void Update()
    {
        int HorizontalAxis = RoudAxis(CrossPlatformInputManager.GetAxis("Horizontal"));
        int VerticalAxis   = RoudAxis(CrossPlatformInputManager.GetAxis("Vertical"));

        //Debug.Log("round: " + VerticalAxis + " - original: " + CrossPlatformInputManager.GetAxis("Vertical"));

        /*Horizontal*/
        if (HorizontalAxis == 1 || HorizontalAxis == -1)
        {
            if (!axisHorizontalDown)
            {
                int dir = HorizontalAxis;
                MovePuzzle(dir, 0);
                axisHorizontalDown = true;
            }            
        }
        else if (HorizontalAxis == 0)
        {
            axisHorizontalDown = false;
        }

        /*Vertical*/
        if (VerticalAxis == 1 || VerticalAxis == -1)
        {
            if (!axisVerticalDown)
            {
                int dir = VerticalAxis;
                MovePuzzle(0, dir * -1);
                axisVerticalDown = true;
            }            
        }
        else if (VerticalAxis == 0)
        {
            axisVerticalDown = false;
        }
    }
    
    private int RoudAxis(float num)
    {
        int floatToInt = 0;
        if (num > 0)
            floatToInt = Mathf.CeilToInt(num);
        else if (num < 0)
            floatToInt = Mathf.FloorToInt(num);
        return floatToInt;
    }

    private void MovePuzzle(int dirX, int dirY)
    {
        //pega o game object
        Transform cacheGameObject;
        cacheGameObject = Board.Find("square-" + (posBlank.x - dirY) + "-" + (posBlank.y - dirX));
        if (cacheGameObject == null)
        {
            Debug.Log("Não pode mover para essa direção");
            return;
        }
        //move
        cacheGameObject.position = new Vector3(posBlank.y * 2, (columns - 1 - posBlank.x) * 2, 0);
        cacheGameObject.name = "square-" + posBlank.x + "-" + posBlank.y;
        //posição correta da peça
        Debug.Log(cacheGameObject.GetComponent<Square>().Row + "-" + cacheGameObject.GetComponent<Square>().Column);
        //atualiza o x da peça vazia
        posBlank.x = posBlank.x - dirY;
        posBlank.y = posBlank.y - dirX;
    }

    private void CropImage()
    {        
        //for (int x = columns - 1; x > -1; x--)
        for (int x = 0; x < columns; x++)
        {
            //for (int y = columns - 1; y > - 1; y--)
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
                arrCropImages.Add(squareTexture2d);
                listObjCropImages.Add(structCrop);
            }
        }
    }


    private void RandomPieces()
    {
        //Assim que adiciona Component nos games object
        //SquareGameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer squareSpriteRenderer;
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (arrCropImages.Count == 0 || 
                    ((x * columns) + y) == (columns * columns) - 1)
                {
                    //ainda não usei
                    posBlank.x = x;
                    posBlank.y = y;
                    matrizBoard[x, y] = 0;
                    return;
                }
                //ainda não usei
                matrizBoard[x, y] = 1;
                //randomiza a imagem
                int randomPosition = Random.Range(0, listObjCropImages.Count - 1);
                StructCrop StructCropImage = listObjCropImages[randomPosition];
                Texture2D randomCropImage = StructCropImage.crop;
                //remove do array
                listObjCropImages.Remove(StructCropImage);
                //Seta a imagem como Sprite
                squareSpriteRenderer = SquareGameObject.GetComponent<SpriteRenderer>();
                squareSpriteRenderer.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width, randomCropImage.height), new Vector2(0, 0), cropSize);
                //Instancia o game object com a imagem recortada
                GameObject instance = Instantiate(SquareGameObject, new Vector3(y * 2, (columns - 1 - x) * 2), Quaternion.identity) as GameObject;
                instance.transform.localScale = new Vector3(2, 2, 1);
                instance.name = "square-" + x + "-" + y;

                //instance.name = "square-" + y + "-" + x;
                instance.GetComponent<Square>().Row = StructCropImage.row;
                instance.GetComponent<Square>().Column = StructCropImage.column;

                //Debug.Log("square-" + y + "-" + x);
                //Adiciona no Board
                instance.transform.SetParent(Board);

                matrizPieces[x, y] = instance;
            }
        }
    }

}