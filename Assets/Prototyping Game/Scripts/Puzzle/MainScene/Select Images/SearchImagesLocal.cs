using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SearchImagesLocal : MonoBehaviour {

	#region PRIVATE VARS
	public ImageServiceAPI mImageServiceAPI;

	public GameObject imgLocal;
	public GameObject btGetMore;

	public GameObject ImageContainer;
	public GameObject ImagePrefab;

	public Text numImages;
	public GameObject errorLoading;
	#endregion


	#region PRIVATE VARS
	private int ImagensTotal = 0;
	private int ImagensLoaded = 0;

	private int ImagensAmountLoad = 9;

	private List<string> galleryImages;
	#endregion

	void Start()
	{
		//deixa o botao de pesquisar mais imagens inativo no começo
		//btGetMore.GetComponent<Button>().interactable = false;

		ImagensLoaded = 0;
		//pega os caminhos da imagens de cel
		galleryImages = GetAllGalleryImagePaths();
		//total de imagens que tem no celular
		ImagensTotal = galleryImages.Count;


		//GameObject ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[0], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[1], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[2], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[3], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[4], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[5], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[6], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[7], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
		//StartCoroutine(GetImage(galleryImages[8], ImagePrefabInstance));
		//ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);

		//if (ImagensTotal == 0)
		//{
		//	numImages.text = "Nenhuma imagem no celular.";
		//	btGetMore.SetActive(false);
		//	errorLoading.SetActive(true);
		//}
		//else
		//{
		//	numImages.text = ImagensTotal + " imagens.";
		errorLoading.SetActive(false);
		//	//
		StartCoroutine(LoadImages());
		//}
	}

	public void LoadMore()
	{
		LoadImages();
	}

	private IEnumerator LoadImages()
	{
		int len,
			cont = ImagensLoaded;
		//se ja tiver chegado na ultima, coloca o tamanho como o total
		if (ImagensLoaded + ImagensAmountLoad > ImagensTotal)
		{
			len = ImagensTotal;
			ImagensLoaded = ImagensTotal;

			btGetMore.GetComponent<Button>().interactable = false;
		}
		else
		{
			len = ImagensLoaded + ImagensAmountLoad;
			ImagensLoaded = ImagensLoaded + ImagensAmountLoad;
		}

		btGetMore.GetComponent<Button>().interactable = false;

		//varre as imagens que serao carregadas
		for (; cont < len; cont++)
		{
			//Instancia um prefab para colocar a imagem
			GameObject ImagePrefabInstance = Instantiate(ImagePrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
			//ObjectImage ObjectImagePrefabInstance = ImagePrefabInstance.GetComponent<ObjectImage>();
			//Get components
			//Image Image = ImagePrefabInstance.transform.Find("Image").GetComponent<Image>();
			//Propriedades da imagem
			//ObjectImagePrefabInstance.ImageTexture = LoadImageGallery(cont);
			yield return StartCoroutine(GetImage(galleryImages[cont], ImagePrefabInstance));
			yield return new WaitForSeconds(1);
			//Carrega o thumbnail
			//ImagePrefabInstance.GetComponent<Image>().sprite = Sprite.Create(ObjectImagePrefabInstance.ImageTexture, new Rect(new Vector2(0, 0), new Vector2(ObjectImagePrefabInstance.ImageTexture.width, ObjectImagePrefabInstance.ImageTexture.height)), new Vector2(.5f, .5f));
			//Adiciona a imagem ao container
			ImagePrefabInstance.transform.SetParent(ImageContainer.transform, false);
		}
		btGetMore.GetComponent<Button>().interactable = true;
	}

	//private IEnumerator LoadImagesOnDemanad(int index, GameObject finalIndex)
	//{
	//	yield return StartCoroutine(GetImage);
	//}

	private IEnumerator GetImage(string UrlImage, GameObject ImageSearch)
	{
		Texture2D TextureImageSearch = null;
		//Chama a rotina que carrega o json das imagens
		yield return StartCoroutine(mImageServiceAPI.GetImage(UrlImage, value => TextureImageSearch = value));
		//Carrega a imagem 
		ImageSearch.GetComponent<ObjectImage>().ImageTexture = TextureImageSearch;
		ImageSearch.transform.Find("Image").GetComponent<Image>().sprite = Sprite.Create(TextureImageSearch, new Rect(new Vector2(0, 0), new Vector2(TextureImageSearch.width, TextureImageSearch.height)), new Vector2(0, 0));
	}

	public Texture2D LoadImageGallery(int indexGallery)
	{
		Texture2D t = new Texture2D(2, 2);

		(new WWW(galleryImages[indexGallery])).LoadImageIntoTexture(t);

		imgLocal.GetComponent<Image>().sprite = Sprite.Create(t, new Rect(new Vector2(0, 0), new Vector2(t.width, t.height)), new Vector2(.5f, .5f));

		return t;
		
	}

	private List<string> GetAllGalleryImagePaths()
	{
		List<string> results = new List<string>();
		HashSet<string> allowedExtesions = new HashSet<string>() { ".png", ".jpg", ".jpeg" };

		try
		{
			AndroidJavaClass mediaClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media");

			// Set the tags for the data we want about each image.  This should really be done by calling; 
			//string dataTag = mediaClass.GetStatic<string>("DATA");
			// but I couldn't get that to work...

			const string dataTag = "_data";

			string[] projection = new string[] { dataTag };
			AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");

			string[] urisToSearch = new string[] { "EXTERNAL_CONTENT_URI", "INTERNAL_CONTENT_URI" };
			foreach (string uriToSearch in urisToSearch)
			{
				AndroidJavaObject externalUri = mediaClass.GetStatic<AndroidJavaObject>(uriToSearch);
				AndroidJavaObject finder = currentActivity.Call<AndroidJavaObject>("managedQuery", externalUri, projection, null, null, null);
				bool foundOne = finder.Call<bool>("moveToFirst");
				while (foundOne)
				{
					int dataIndex = finder.Call<int>("getColumnIndex", dataTag);
					string data = finder.Call<string>("getString", dataIndex);
					if (allowedExtesions.Contains(Path.GetExtension(data).ToLower()))
					{
						string path = @"file:///" + data;
						results.Add(path);
					}

					foundOne = finder.Call<bool>("moveToNext");
				}
			}
		}
		catch (System.Exception e)
		{
			// do something with error...
		}

		return results;
	}

	/// <summary>
	/// based off 2 lines of Java code found at at http://stackoverflow.com/questions/18416122/open-gallery-app-in-androi
	///      Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("content://media/internal/images/media"));
	///      startActivity(intent); 
	/// expanded the 1st line to these 3:
	///      Intent intent = new Intent();
	///      intent.setAction(Intent.ACTION_VIEW);
	///      intent.setData(Uri.parse("content://media/internal/images/media"));
	/// </summary>
	public void OpenAndroidGallery()
	{
		#region [ Intent intent = new Intent(); ]
		//instantiate the class Intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		//instantiate the object Intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		#endregion [ Intent intent = new Intent(); ]
		
		#region [ intent.setAction(Intent.ACTION_VIEW); ]
		//call setAction setting ACTION_SEND as parameter
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_GET_CONTENT"));
		#endregion [ intent.setAction(Intent.ACTION_VIEW); ]
		
		#region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//instantiate the class Uri
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		//instantiate the object Uri with the parse of the url's file
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		#endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		
		//set the type of file
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

		#region [ startActivity(intent); ]
		//instantiate the class UnityPlayer
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//instantiate the object currentActivity
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		////call the activity with our Intent
		//currentActivity.Call("startActivity", intentObject);


		currentActivity.Call("startActivityForResult",
			intentClass.GetStatic<string>("SELECT_IMAGE"));

		#endregion [ startActivity(intent); ]
	}
}
