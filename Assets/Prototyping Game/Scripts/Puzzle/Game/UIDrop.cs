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
		Debug.Log("OnDrop");
		if (eventData.pointerDrag)
		{
			//Pega a referencia do objeto que estava sendo draggado
			mUIDragAndDrop = eventData.pointerDrag.GetComponent<UIDragAndDrop>();		
			if(mUIDragAndDrop.EnabledDrag)
			{
				Debug.Log("if(mUIDragAndDrop.EnabledDrag)");
				mUIDragAndDrop.IsDropped = true;
				//Debug.Log(transform.parent.GetComponent<RectTransform>());
				////seta a posicao da peca droppada, a posicao da area de drop
				RectTransform r = eventData.pointerDrag.GetComponent<RectTransform>();
				////Vector2 positionDest = new Vector2(transform.parent.GetComponent<RectTransform>().anchoredPosition.x, transform.parent.GetComponent<RectTransform>().anchoredPosition.y);

				//Debug.Log("Drop Anchored: " + transform.parent.GetComponent<RectTransform>().anchoredPosition + " - Square: " + r.anchoredPosition);
				
				int PostionDragRow = Mathf.FloorToInt(r.anchoredPosition.x / 100);
				int PostionDragColumn = Mathf.FloorToInt((r.anchoredPosition.y * -1) / 100);

				////r.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
				r.anchoredPosition = new Vector2((PostionDragRow * 100) + 50, ((PostionDragColumn) * -100) - 50);

				Debug.Log(eventData.pointerDrag);
				////avisa ao board que a peca foi droppada
				mUIBoardManager.SetPositionSquareBlank(eventData.pointerDrag);
			}

		}
	}
}
;