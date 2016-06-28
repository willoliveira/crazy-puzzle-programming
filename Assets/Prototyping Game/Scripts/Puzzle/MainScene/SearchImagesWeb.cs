using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using SimpleJSON;
using UnityEngine.EventSystems;

public class SearchImagesWeb : MonoBehaviour
{
	public ImageServiceAPI mImageServiceAPI;
	public InputField mInputField;
	public GameObject ImageContainer;
	public GameObject ImagePrefab;


	private int Page = 0;
	private int ImagensTotal = 0;
	private int ImagensLoaded = 0;

	private ObjectImage SelectedObjectImage;

	// Use this for initialization
	void Start()
	{

	}

	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	public void btLinkPixabay()
	{
		Application.OpenURL("https://pixabay.com/");
	}
	/// <summary>
	/// TODO: partir daqui agora
	/// </summary>
	public void OnSelectImage()
	{
		//SelectedObjectImage = data.pointerPress.transform.GetComponent<ObjectImage>();
	}
	/// <summary>
	/// 
	/// </summary>
	public void OnSearchImages()
	{
		ClearSearch();

		StartCoroutine(GetImages());
	}
	/// <summary>
	/// TODO: Implementar um loading depois
	/// </summary>
	public void OnGetMore()
	{
		Page += 1;
		//Chama o metodo que carrega as imagens
		StartCoroutine(GetImages());
		//se nao houver mais imagens a serem carregadas, desativa o botao
		if (Page * 10 >= ImagensTotal)
		{
			//desativa o botao
			GameObject.Find("GetMore").GetComponent<Button>().interactable = false;
		}

	}
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	private void ClearSearch()
	{
		//zera a pagina
		Page = 1;
		//limpa o container
		foreach (Transform child in ImageContainer.transform)
		{
			Destroy(child.gameObject);
		}
	}
	/// <summary>
	/// Chama o metodo que carrega as imagens
	/// </summary>
	/// <returns></returns>
	private IEnumerator GetImages()
	{
		string strResult = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImages(mInputField.text, Page, value => strResult = value));
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
		//Adiciona o numero de imagens carregado
		ImagensLoaded += arrImages.Count;
		//Guarda o total
		ImagensTotal = JsonImages["totalHits"].AsInt;
		for (int cont = 0, len = arrImages.Count; cont < len; cont++)
		{
			//Instancia um prefab para colocar a imagem
			GameObject ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
			ObjectImage ObjectImagePrefabInstance = ImagePrefabInstance.GetComponent<ObjectImage>();
			//Get components
			Image Image = ImagePrefabInstance.transform.Find("Image").GetComponent<Image>();
			//Text ImageTitle = ImagePrefabInstance.transform.Find("ImageTags").GetComponent<Text>();
			
			//Propriedades da imagem
			ObjectImagePrefabInstance.WebformatWidth = arrImages[cont]["webformatWidth"].AsInt;
			ObjectImagePrefabInstance.WebformatHeight = arrImages[cont]["webformatHeight"].AsInt;
			ObjectImagePrefabInstance.WebformatURL = arrImages[cont]["webformatURL"].Value;
			
			//Carrega o thumbnail
			StartCoroutine(GetImage(arrImages[cont]["webformatURL"].Value, Image));

			//coloca o nome da tag
			//ImageTitle.text = arrImages[cont]["tags"].Value;
			//Adiciona a imagem ao container
			ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="UrlImage"></param>
	/// <param name="ImageSearch"></param>
	/// <returns></returns>
	private IEnumerator GetImage(string UrlImage, Image ImageSearch)
	{
		Texture2D TextureImageSearch = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImage(UrlImage, value => TextureImageSearch = value));
		//Carrega a imagem 
		ImageSearch.sprite = Sprite.Create(TextureImageSearch, new Rect(new Vector2(0, 0), new Vector2(TextureImageSearch.width, TextureImageSearch.height)), new Vector2(0, 0));
	}
	#endregion
}
