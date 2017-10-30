using UnityEngine;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{

	/// <summary>
	/// Enum para os tipos de jogo
	/// </summary>
	public enum GameMode
	{
		Classic,
		Free,
		Hard
	}
	/// <summary>
	/// Enum para os tipos de imagem do jogo
	/// </summary>
	public enum ImageMode
	{
		Local,
		Internet,
		Default
	}

	public class GameManager : MonoBehaviour
	{
		#region PUBLIC VARS
		[HideInInspector]
		public static GameManager instance;
		[HideInInspector]
		public GameMode mGameMode;
		[HideInInspector]
		public ImageMode mImageMode;
		//URL da imagem quando for da internet
		[HideInInspector]
		public string ImageURL;
		//imagem selecionada
		[HideInInspector]
		public Texture2D ImageSelect;
		//retangulo de recorte da image seleciona
		[HideInInspector]
		public Rect ImageCropRect;

		public bool GamePaused;
		#endregion

		#region PRIVATE VARS
		#endregion

		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Awake()
		{
			//não deixa ele ser destruido
			DontDestroyOnLoad(gameObject);
			//se for null mantem, senao destroi ele
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				DestroyObject(gameObject);
			}
			//tira o mult touch
			Input.multiTouchEnabled = false;
		}
	}
}