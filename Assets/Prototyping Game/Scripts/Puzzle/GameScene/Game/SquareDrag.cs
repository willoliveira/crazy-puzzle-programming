using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SquareDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region PUBLIC VARS
	public bool EnabledDrag;
	[HideInInspector] public Vector3 PositionBeforeDrag;
	[HideInInspector] public bool IsDropped = false;
	#endregion

	#region PRIVATE VARS
	private GameObject itemBeingDragged;
	private Text getAxisTest;
	#endregion

	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
			if (EnabledDrag)
		{
			//guarda uma referencia para o game object que comecou a ser draggado
			itemBeingDragged = gameObject;
			PositionBeforeDrag = transform.position;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		if (EnabledDrag)
		{
			transform.position = Input.mousePosition;
			//getAxisTest.text = "MX: " + CrossPlatformInputManager.GetAxis("Mouse X") + " - MY: " + CrossPlatformInputManager.GetAxis("Mouse Y");
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("OnEndDrag");

		if (EnabledDrag && !IsDropped)
		{
			IsDropped = false;
			transform.position = PositionBeforeDrag;
		}
		IsDropped = false;
		itemBeingDragged = null;
	}
	#endregion
}
