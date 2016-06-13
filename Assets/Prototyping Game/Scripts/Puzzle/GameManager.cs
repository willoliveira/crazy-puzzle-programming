using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	/// <summary>
	/// Enum para os tipos de jogo
	/// </summary>
	public enum SelectMode
	{
		Image,
		Word,
		Number
	}
	/// <summary>
	/// Enum para o niveis de dificuldade
	/// </summary>
	public enum DiffilcultyMode
	{
		Normal,
		Hard
	}
		
	public class GameManager : MonoBehaviour
	{

		[HideInInspector]
		public SelectMode mSelectMode;
		[HideInInspector]
		public DiffilcultyMode mDiffilcultyMode;
		/// <summary>
		/// Inicializa valores default para os modos de jogo
		/// </summary>
		void Start()
		{
			mSelectMode = SelectMode.Image;
			mDiffilcultyMode = DiffilcultyMode.Normal;
		}
	}
}