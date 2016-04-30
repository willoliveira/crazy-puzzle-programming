using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //tamanho do recorte
    public int cropSize = 200;
    //imagem de referencia
    public Texture2D image;
    //numero de colunas
    private float columns;

    //referencia de onde vai adicionar as peças
    public Transform Board;
    //quadrado de referencia
    public GameObject SquareGameObject;
    //Lista de crop images
    private List<Texture2D> arrCropImages;

    // Use this for initialization
    void Start()
    {
        //pega as colunas
        columns = image.width / cropSize;
        
        arrCropImages = new List<Texture2D>();
        //Corta a imagem
        CropImage();
        //Random
        RandomPieces();
    }

    public void CropImage()
    {
        
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                //se for no lugar da ultima peça não pega
                if (x == 0 && y == 3) continue;
                //Pega os pixels do recorte que quero fazer
                Color[] pix = image.GetPixels(image.width - cropSize - (x * cropSize), image.height - cropSize - (y * cropSize), cropSize, cropSize);
                //Cria uma textura com o recorte
                Texture2D squareTexture2d = new Texture2D(cropSize, cropSize);
                squareTexture2d.SetPixels(pix);
                squareTexture2d.wrapMode = TextureWrapMode.Clamp;
                squareTexture2d.Apply();
                //adiciona no array o recorte
                arrCropImages.Add(squareTexture2d);                
            }
        }
    }


    public void RandomPieces()
    {
        //Assim que adiciona componente nos games object
        //SquareGameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer squareSpriteRenderer;
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                

                //pega a imagem
                int index = Random.Range(0, arrCropImages.Count);
                Texture2D randomCropImage = arrCropImages[index];
                //
                arrCropImages.Remove(randomCropImage);
                //Seta a imagem como Sprite
                squareSpriteRenderer = SquareGameObject.GetComponent<SpriteRenderer>();
                squareSpriteRenderer.sprite = Sprite.Create(randomCropImage, new Rect(0, 0, randomCropImage.width, randomCropImage.height), new Vector2(0, 0), cropSize);
                //Instancia o game object com a imagem recortada
                GameObject instance = Instantiate(SquareGameObject, new Vector3(x * 2, y * 2), Quaternion.identity) as GameObject;
                instance.transform.localScale = new Vector3(2, 2, 1);
                //Adiciona no Board
                instance.transform.SetParent(Board);
            }
        }
    }


}

