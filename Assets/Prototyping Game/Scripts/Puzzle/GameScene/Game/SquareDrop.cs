using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using PrototypingGame;

public class SquareDrop : MonoBehaviour, IDropHandler
{
	private BoardManager mUIBoardManager;
	private SquareDrag mSquareDrag;

	void Start()
	{
		mUIBoardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
	}

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
				int PostionDragRow = Mathf.FloorToInt(SquareDropped.anchoredPosition.x / mUIBoardManager.PieceSize);
				int PostionDragColumn = Mathf.FloorToInt((SquareDropped.anchoredPosition.y * -1) / mUIBoardManager.PieceSize);
				//seta a posicao na peca
				SquareDropped.anchoredPosition = new Vector2((PostionDragRow * mUIBoardManager.PieceSize) + (mUIBoardManager.PieceSize / 2), ((PostionDragColumn) * - mUIBoardManager.PieceSize) - (mUIBoardManager.PieceSize / 2));
				//avisa ao board que a peca foi droppada
				mUIBoardManager.SetPositionSquareBlank(eventData.pointerDrag);
			}

		}
	}
}
;