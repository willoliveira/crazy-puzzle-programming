using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityStandardAssets.CrossPlatformInput;

using System.Collections;
using System;

public class UIDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private GameObject itemBeingDragged;
	[HideInInspector] public Vector3 PositionBeforeDrag;
	[HideInInspector] public bool IsDropped;

	public Text getAxis;

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

			getAxis.text = "MX: " + CrossPlatformInputManager.GetAxis("Mouse X") + " - MY: " + CrossPlatformInputManager.GetAxis("Mouse Y");
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		
		//if (EnabledDrag && !IsDropped)
		//{
		//	transform.position = PositionBeforeDrag;
		//}
		itemBeingDragged = null;
	}
}
