using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	/// <summary>
	/// Enum para os tipos de jogo
	/// </summary>
	public enum SelectMode
	{
		Classic,
		Free
	}
	/// <summary>
	/// Enum para o niveis de dificuldade
	/// </summary>
	public enum DiffilcultyMode
	{
		Normal,
		Hard
	}

	/// <summary>
	/// Enum para o niveis de dificuldade
	/// </summary>
	public enum ImageMode
	{
		Default,
		Local,
		Internet,
	}

	public class GameManager : MonoBehaviour
	{

		[HideInInspector]
		public SelectMode mSelectMode;
		[HideInInspector]		
		public DiffilcultyMode mDiffilcultyMode;
		[HideInInspector]
		public ImageMode mImageMode;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Start()
		{
			mSelectMode = SelectMode.Free;
			mDiffilcultyMode = DiffilcultyMode.Normal;
		}
	}
}