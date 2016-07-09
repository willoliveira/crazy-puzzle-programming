
using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.EventSystems;
 using System.Collections;
 
 public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region PUBLIC VARS
	public bool EnabledDrag;
	[HideInInspector]
	public Vector3 PositionBeforeDrag;
	[HideInInspector]
	public bool IsDropped = false;
	[HideInInspector]
	public RectTransform ParentRT;
	
	public bool hasDropArea;
	#endregion

	#region PRIVATE VARS
	private bool mouseDown = false;
	private Vector3 startMousePos;

	private int indeBeforeDrag = 0;
	
	private bool restrictX;
	private bool restrictY;
	private float fakeX;
	private float fakeY;
	private float myWidth;
	private float myHeight;
	
	private RectTransform MyRect;
	#endregion

	void Start()
	{
		MyRect = GetComponent<RectTransform>();

		myWidth = (MyRect.rect.width + 5) / 2;
		myHeight = (MyRect.rect.height + 5) / 2;
	}

	public void OnBeginDrag(PointerEventData ped)
	{
		if (!EnabledDrag) return;

		indeBeforeDrag = transform.GetSiblingIndex();
		//se houver area de drop, colocar ele acima de todo mundo, menos do drop
		if (hasDropArea)
		{
			transform.SetSiblingIndex(SquareDrop.DropArea.transform.GetSiblingIndex() - 1);
		}
		else
		{
			transform.SetAsLastSibling();
		}
		mouseDown = true;
		PositionBeforeDrag = transform.position;
		startMousePos = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData ped)
	{
		mouseDown = false;
		//volta pro index antes de começar o drag
		transform.SetSiblingIndex(indeBeforeDrag);
		//se houver area de drag
		if (EnabledDrag && hasDropArea && !IsDropped)
		{
			IsDropped = false;
			transform.position = PositionBeforeDrag;
		}
		IsDropped = false;
	}

	public void OnDrag(PointerEventData ped)
	{
		if (!EnabledDrag) return;

		if (mouseDown)
		{
			myWidth = (MyRect.rect.width + 5) / 2;
			myHeight = (MyRect.rect.height + 5) / 2;

			Vector3 currentPos = Input.mousePosition;
			Vector3 diff = currentPos - startMousePos;
			Vector3 pos = PositionBeforeDrag + diff;
			transform.position = pos;

			if (transform.localPosition.x < 0 - ((ParentRT.rect.width / 2) - myWidth) || transform.localPosition.x > ((ParentRT.rect.width / 2) - myWidth))
				restrictX = true;
			else
				restrictX = false;

			if (transform.localPosition.y < 0 - ((ParentRT.rect.height / 2) - myHeight) || transform.localPosition.y > ((ParentRT.rect.height / 2) - myHeight))
				restrictY = true;
			else
				restrictY = false;

			if (restrictX)
			{
				if (transform.localPosition.x < 0)
					fakeX = 0 - (ParentRT.rect.width / 2) + myWidth;
				else
					fakeX = (ParentRT.rect.width / 2) - myWidth;

				Vector3 xpos = new Vector3(fakeX, transform.localPosition.y, 0.0f);
				transform.localPosition = xpos;
			}

			if (restrictY)
			{
				if (transform.localPosition.y < 0)
					fakeY = 0 - (ParentRT.rect.height / 2) + myHeight;
				else
					fakeY = (ParentRT.rect.height / 2) - myHeight;

				Vector3 ypos = new Vector3(transform.localPosition.x, fakeY, 0.0f);
				transform.localPosition = ypos;
			}

		}
	}


}