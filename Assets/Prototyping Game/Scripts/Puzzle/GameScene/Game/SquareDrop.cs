﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SquareDrop : MonoBehaviour, IDropHandler
{
	public AudioClip DropSound;

	#region PRIVATE VARS
	private AudioManager mAudioManager;
	private BoardManager mUIBoardManager;
	private SquareDrag mSquareDrag;
	#endregion

	void Start()
	{
		mUIBoardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
		mAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	#region PUBLIC METHODS
	public void OnDrop(PointerEventData eventData)
	{
		//Debug.Log("OnDrop");
		if (eventData.pointerDrag)
		{
			//Pega a referencia do objeto que estava sendo draggado
			mSquareDrag = eventData.pointerDrag.GetComponent<SquareDrag>();
			//verifica se essa peca possui o drag habilitado
			if(mSquareDrag.EnabledDrag)
			{
				//seta a peca dragada como dropada, indicando que ela foi colocada no drop area
				mSquareDrag.IsDropped = true;
				//seta a posicao da peca droppada, a posicao da area de drop
				RectTransform SquareDropped = eventData.pointerDrag.GetComponent<RectTransform>();
				//pega as posicoes da linha e coluna
				//int PostionDragRow = Mathf.FloorToInt(SquareDropped.anchoredPosition.x / mUIBoardManager.PieceSize);
				//int PostionDragColumn = Mathf.FloorToInt((SquareDropped.anchoredPosition.y * -1) / mUIBoardManager.PieceSize);
				//seta a posicao na peca
				//Hora ou outra esse calculo quebrava...
				//SquareDropped.anchoredPosition = new Vector2((PostionDragRow * mUIBoardManager.PieceSize) + (mUIBoardManager.PieceSize / 2), ((PostionDragColumn) * - mUIBoardManager.PieceSize) - (mUIBoardManager.PieceSize / 2));
				SquareDropped.anchoredPosition = GameObject.Find("DropArea").GetComponent<RectTransform>().anchoredPosition;
				//avisa ao board que a peca foi droppada
				mUIBoardManager.SetPositionSquareBlank(eventData.pointerDrag);
				//
				mAudioManager.Play(DropSound, this.name, AudioType.SFX);
			}

		}
	}
	#endregion
}