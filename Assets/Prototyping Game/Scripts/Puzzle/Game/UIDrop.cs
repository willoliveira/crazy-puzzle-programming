using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using PrototypingGame;

public class UIDrop : MonoBehaviour, IDropHandler
{
	private UIBoardManager mUIBoardManager;
	private UIDragAndDrop mUIDragAndDrop;

	void Start()
	{
		mUIBoardManager = GameObject.Find("BoardManager").GetComponent<UIBoardManager>();
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag)
		{
			//Pega a referencia do objeto que estava sendo draggado
			mUIDragAndDrop = eventData.pointerDrag.GetComponent<UIDragAndDrop>();
			//verifica se essa peca possui o drag habilitado
			if(mUIDragAndDrop.EnabledDrag)
			{
				//seta a peca dragada como dropada, indicando que ela foi colocada no drop area
				mUIDragAndDrop.IsDropped = true;
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