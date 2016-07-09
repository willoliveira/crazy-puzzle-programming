using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using SimpleJSON;
using UnityEngine.EventSystems;

public class SearchImagesWeb : MonoBehaviour
{
	#region PUBLIC VARS
	public ImageServiceAPI mImageServiceAPI;
	public Button GetMore;
	public InputField mInputField;
	public GameObject ImageContainer;
	public GameObject ImagePrefab;

	public GameObject ErrorLoading;
	#endregion

	#region PRIVTE VARS
	private int Page = 0;
	private int ImagensTotal = 0;
	private int ImagensLoaded = 0;

	private ObjectImage SelectedObjectImage;
	#endregion

	// Use this for initialization
	void Start()
	{
		//deixa o botao de pesquisar mais imagens inativo no começo
		GetMore.interactable = false;
		//deixa a mensagem de erro no inicio desabilitada
		ErrorLoading.SetActive(false);
	}

	void OnDisable()
	{
		//quando desabilitar a pagina, limpa a busca
		//ClearSearch();
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
	/// 
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
		//limpas as imagens que estavam na tela
		ClearSearch();
		//pega as imagens
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
			GetMore.interactable = false;
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
		ProcessImages(strResult);
	}
	/// <summary>
	/// formato da resposta
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
	private void ProcessImages(string response)
	{
		//se nao houve resposta, quer dizer que houver algum erro
		if (string.IsNullOrEmpty(response) || (!string.IsNullOrEmpty(response) && JSON.Parse(response)["totalHits"].AsInt == 0))
		{
			if (string.IsNullOrEmpty(response))
				ErrorLoading.GetComponent<Text>().text = "Não foi possivel\ncarregar as imagens!";
			else
				ErrorLoading.GetComponent<Text>().text = "A busca não retornou\nnenhuma imagem!";
			//ativa a mensagem de erro
			ErrorLoading.SetActive(true);
			GetMore.interactable = false;
		}
		else
		{
			ErrorLoading.SetActive(false);
			GetMore.interactable = true;
			//Transforma a string
			JSONNode JsonImages = JSON.Parse(response);
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
				//Propriedades da imagem
				ObjectImagePrefabInstance.ImageURL = arrImages[cont]["webformatURL"].Value;
				//Carrega o thumbnail
				StartCoroutine(GetImage(arrImages[cont]["webformatURL"].Value, Image));
				//Adiciona a imagem ao container
				ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);
			}
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
