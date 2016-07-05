using UnityEngine;
using System.Collections;

namespace PrototypingGame
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
	/// Enum para o niveis de dificuldade
	/// </summary>
	public enum ImageMode
	{		
		Local,
		Internet,
		Default
	}

	public class GameManager : MonoBehaviour
	{

		[HideInInspector]
		public GameMode mGameMode;
		[HideInInspector]
		public ImageMode mImageMode;
		//imagem selecionada
		[HideInInspector]
		public Texture2D ImageSelect;
		[HideInInspector]
		public string ImageURL;
		//imagem cropada selecionada
		[HideInInspector]
		public Texture2D ImageCropSelect;
		[HideInInspector]
		public Sprite SpriteCropSelect;
		//retangulo de recorte da image
		[HideInInspector]
		public Rect ImageCropRect;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Awake()
		{
			//tira o mult touch
			//Input.multiTouchEnabled = false;
			//mSelectMode = GameMode.Free;
		}
	}
}