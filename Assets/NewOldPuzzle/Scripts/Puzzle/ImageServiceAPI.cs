using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;


namespace CountingSheeps.NewOldPuzzle
{

	public class ImageServiceAPI : MonoBehaviour
	{
		#region CONST VARS
		const string URLApi = "https://pixabay.com/api/";
		const string key = "2779220-fc18bd9ac9619ced4a7c773fc";
		#endregion

		#region PRIVATE VARS
		//Imagens por pagina
		private int PerPage = 9;
		//busca
		private string q = "";
		//tipo de imagens
		private string ImageType = "photo";
		//busca segura
		private bool SafeSearch = true;
		//linguagem da busca
		private string Lang = "pt";
		#endregion

		// Use this for initialization
		void Start() { }

		#region PUBLIC METHODS
		/// <summary>
		/// 
		/// key
		/// q
		/// per_page
		/// image_type
		/// safesearch
		/// lang
		/// 
		/// [image type] Accepted values: "all", "photo", "illustration", "vector" 
		/// 
		/// Podemos pensar nos seguintes parametros também
		/// [category] Accepted values: fashion, nature, backgrounds, science, education, people, feelings, religion, health, places, animals, industry, food, computer, sports, transportation, travel, buildings, business, music
		/// [lang] Accepted values: cs, da, de, en, es, fr, id, it, hu, nl, no, pl, pt, ro, sk, fi, sv, tr, vi, th, bg, ru, el, ja, ko, zh 
		/// 
		/// </summary>
		/// <param name="searchText">Busca</param>
		/// <returns></returns>
		public IEnumerator GetImages(string SearchText, int Page, System.Action<string> result)
		{
			q = SearchText;
			WWW www = new WWW(string.Format("{0}?key={1}&q={2}&per_page={3}&image_type={4}&safesearch={5}&lang={6}&page={7}",
				URLApi, key, q, PerPage, ImageType, SafeSearch, Lang, Page));
			yield return www;

			if (www.error == null)
			{
				result(www.text);
			}
			else
			{
				Debug.Log("Error GetImages: " + www.error);
				result("");
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="UrlImage"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public IEnumerator GetImage(string UrlImage, System.Action<Texture2D> result)
		{
			WWW www = new WWW(UrlImage);
			yield return www;

			if (www.error == null)
			{
				result(www.texture);
			}
			else
			{
				//TODO: fazer algo quando der erro aqui
				Debug.Log("Error GetImage: " + www.error);
				result(Texture2D.whiteTexture);
			}
		}
		#endregion
	}
}