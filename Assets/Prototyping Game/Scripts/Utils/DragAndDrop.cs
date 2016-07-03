using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
	public GameObject Drop;
	public bool EnabledDrag = true;

	[HideInInspector] public Vector3 PositionBeforeDrag;
	[HideInInspector] public bool IsCollider = false;
	[HideInInspector] public bool IsDrag = false;

	private Vector3 Offset;

	void Start() { }

	void OnMouseDown()
	{
		if (EnabledDrag)
		{
			Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
			PositionBeforeDrag = gameObject.transform.position;
		}
	}

	void OnMouseUp()
	{
		if (EnabledDrag)
		{
			IsDrag = false;
			if (IsCollider)
			{
				gameObject.transform.position = Drop.transform.position;
			}
			else
			{
				gameObject.transform.position = PositionBeforeDrag;
				PositionBeforeDrag = Vector3.zero;
			}
		}
	}

	void OnMouseDrag()
	{
		if (EnabledDrag)
		{
			IsDrag = true;
			//todo: pensar em como fazer a limitação de onde a peça pode se mover, por enquanto para testes, vou deixar o movimento livre
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + Offset;
			transform.position = curPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject == Drop)
		{
			IsCollider = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == Drop)
		{
			IsCollider = false;
		}
	}
}