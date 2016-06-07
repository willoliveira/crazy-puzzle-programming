using UnityEngine;
using System.Collections;

using PrototypingGame;

[RequireComponent(typeof(DragAndDrop))]
public class SquareDragAndDrop : MonoBehaviour
{
	private DragAndDrop mDragAndDrop;
	private BoardManager mBoardManager;

	void Start()
	{
		mDragAndDrop = GetComponent<DragAndDrop>();
		mBoardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
	}

	void OnMouseUp()
	{
		Debug.Log("OnMouseUp");
		if (mDragAndDrop.IsCollider)
		{
			Debug.Log("mDragAndDrop.IsCollider");
			mBoardManager.SetPositionSquareBlank(gameObject, mDragAndDrop.PositionBeforeDrag);
		}
	}
}
