using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using SimpleJSON;

public class SearchImages : MonoBehaviour
{

	public ImageServiceAPI mImageServiceAPI;

	public InputField mInputField;

	public GameObject ImageContainer;
	public GameObject ImagePrefab;

	struct ImageObject
	{

	}

	// Use this for initialization
	void Start()
	{

	}

	public void OnSearchImage()
	{
		StartCoroutine(GetImages());
	}


	IEnumerator GetImages()
	{
		string strResult = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImages(mInputField.text, 1, value => strResult = value));
		//Processa o objeto de imagens que foi carregado
		ProcessImages(JSON.Parse(strResult));
	}

	/// <summary>
	/// formato a resposta
	/// {
	///		"total": 4692
	///		"totalHits": 500,
	///		"hits": [{
	///		        "id": 195893,
	///		        "pageURL": "https://pixabay.com/en/blossom-bloom-flower-yellow-close-195893/",
	///		        "type": "photo",
	///		        "tags": "blossom, bloom, flower",
	///		        "previewURL": "https://pixabay.com/static/uploads/photo/2013/10/15/09/12/flower-195893_150.jpg"
	///		        "previewWidth": 150,
	///		        "previewHeight": 84,
	///		        "webformatURL": "https://pixabay.com/get/35bbf209db8dc9f2fa36746403097ae226b796b9e13e39d2_640.jpg",
	///		        "webformatWidth": 640,
	///		        "webformatHeight": 360,
	///		        "imageWidth": 4000,
	///		        "imageHeight": 2250,
	///		        "views": 7671,
	///		        "downloads": 6439,
	///		        "favorites": 1,
	///		        "likes": 5,
	///		        "comments": 2,
	///		        "user_id": 48777,
	///		        "user": "Josch13",
	///		        "userImageURL": "https://pixabay.com/static/uploads/user/2013/11/05/02-10-23-764_250x250.jpg",
	///		    },
	///		    {
	///		        "id": 14724,
	///		        ...
	///		    },
	///		    ...
	///		]
	///}
	/// </summary>
	/// <param name="JsonImages"></param>
	private void ProcessImages(JSONNode JsonImages)
	{
		Debug.Log(JsonImages);
		//pega o array da resposta das imagens
		JSONArray arrImages = JsonImages["hits"].AsArray;
		for (int cont = 0, len = arrImages.Count; cont < len; cont++)
		{
			Debug.Log(arrImages[cont]["tags"].Value);
			//Pega o RectTransform do prefab da image
			RectTransform CacheRectTransformImagePrefab = ImagePrefab.GetComponent<RectTransform>();
			//calcula as posicoes
			float PositionX = (CacheRectTransformImagePrefab.rect.width / 2) + ((CacheRectTransformImagePrefab.rect.width) * (cont));
			float PositionY = (CacheRectTransformImagePrefab.rect.height / 2) * -1;
			//Instancia um prefab para colocar a imagem
			GameObject ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(PositionX, PositionY), Quaternion.identity) as GameObject;
			//Get components
			Image Image = ImagePrefabInstance.transform.Find("Container/Image").GetComponent<Image>();
			Text ImageTitle = ImagePrefabInstance.transform.Find("ImageTags").GetComponent<Text>();


			//
			StartCoroutine(GetImage(arrImages[cont]["webformatURL"].Value, Image));

			//coloca o nome da tag
			ImageTitle.text = arrImages[cont]["tags"].Value;
			//Adiciona a imagem ao container
			ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);
		}
	}

	IEnumerator GetImage(string UrlImage, Image ImageSearch)
	{
		Texture2D TextureImageSearch = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImage(UrlImage, value => TextureImageSearch = value));
		//Carrega a imagem 
		ImageSearch.sprite = Sprite.Create(TextureImageSearch, new Rect(new Vector2(0, 0), new Vector2(TextureImageSearch.width, TextureImageSearch.height)), new Vector2(0, 0));
	}
}
