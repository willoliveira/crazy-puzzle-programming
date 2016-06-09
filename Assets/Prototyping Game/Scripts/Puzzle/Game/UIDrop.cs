using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UIDrop : MonoBehaviour, IDropHandler
{

	public GameObject item
	{
		get
		{
			if (transform.childCount > 0)
			{
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("UIDrop OnDrop");
	}
}
