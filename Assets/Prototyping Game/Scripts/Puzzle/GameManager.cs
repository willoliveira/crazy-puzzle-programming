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

		public Texture2D ImageSelect;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Start()
		{
			//mSelectMode = GameMode.Free;
		}
	}
}