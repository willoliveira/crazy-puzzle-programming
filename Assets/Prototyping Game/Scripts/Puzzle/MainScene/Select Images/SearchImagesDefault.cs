using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchImagesDefault : MonoBehaviour {
	
	public Sprite[] LocalImages;
	public GameObject ImageContainer;
	public GameObject ImagePrefab;
	
	void Start () {

		CreateContainersImages();
	}

	private void CreateContainersImages()
	{
		for (int cont = 0, len = LocalImages.Length; cont < len; cont++)
		{
			//Instancia um prefab para colocar a imagem
			GameObject ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
			//pega a referencia do object image
			ObjectImage mObjectImage = ImagePrefabInstance.GetComponent<ObjectImage>();

			//Get components
			Image Image = ImagePrefabInstance.transform.Find("Image").GetComponent<Image>();
			//Text ImageTitle = ImagePrefabInstance.transform.Find("ImageTags").GetComponent<Text>();
			//
			Image.sprite = LocalImages[cont];
			//
			mObjectImage.ImageTexture = Image.sprite.texture;

			//Adiciona a imagem ao container
			ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);
		}
	}

}
