using UnityEngine;
using System.Collections;

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

public class GameManager : MonoBehaviour {

	[HideInInspector]
	public SelectMode mSelectMode;
	[HideInInspector]
	public DiffilcultyMode mDiffilcultyMode;
}
