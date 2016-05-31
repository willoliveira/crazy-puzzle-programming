using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	public enum SelectMode
	{
		Image,
		Word,
		Number
	}

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


		void Start()
		{
			mSelectMode = SelectMode.Image;
			mDiffilcultyMode = DiffilcultyMode.Normal;
		}
	}
}