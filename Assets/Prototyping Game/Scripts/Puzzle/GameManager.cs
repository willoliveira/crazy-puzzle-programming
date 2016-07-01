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
		public Texture2D ImageSelect;
		//imagem cropada selecionada
		public Texture2D ImageCropSelect;
		//retangulo de recorte da image
		public Rect ImageCropRect;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Start()
		{
			//mSelectMode = GameMode.Free;
		}
	}
}