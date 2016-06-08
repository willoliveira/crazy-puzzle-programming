﻿using UnityEngine;
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
		if (mDragAndDrop.EnabledDrag)
		{
			if (mDragAndDrop.IsCollider)
			{
				mBoardManager.SetPositionSquareBlank(gameObject, mDragAndDrop.PositionBeforeDrag);
			}
		}
	}
}
