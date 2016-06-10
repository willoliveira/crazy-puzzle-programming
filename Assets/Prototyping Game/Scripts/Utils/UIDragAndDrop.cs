using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UIDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private GameObject itemBeingDragged;
	[HideInInspector] public Vector3 PositionBeforeDrag;
	[HideInInspector] public bool IsDropped;

	public bool EnabledDrag;
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (EnabledDrag)
		{
			//guarda uma referencia para o game object que comecou a ser draggado
			itemBeingDragged = gameObject;
			PositionBeforeDrag = transform.position;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (EnabledDrag)
		{
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		
		if (EnabledDrag && !IsDropped)
		{
			transform.position = PositionBeforeDrag;
		}
		itemBeingDragged = null;
	}
}
