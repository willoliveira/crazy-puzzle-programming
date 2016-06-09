using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UIDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;


	public void Drag()
	{
		transform.position = Input.mousePosition;
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		//GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		itemBeingDragged = null;
		if (transform.parent != startParent)
		{
			transform.position = startPosition;
		}
	}
}
