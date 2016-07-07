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

		//URL da imagem quando for da internet
		[HideInInspector]
		public string ImageURL;
		//imagem selecionada
		[HideInInspector]
		public Texture2D ImageSelect;
		//retangulo de recorte da image seleciona
		[HideInInspector]
		public Rect ImageCropRect;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Awake()
		{
			//tira o mult touch
			Input.multiTouchEnabled = false;
			//mSelectMode = GameMode.Free;
		}
	}
}