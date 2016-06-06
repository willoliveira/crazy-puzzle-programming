using UnityEngine;
using System.Collections;

public class GameobjectDragAndDrop : MonoBehaviour {
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool isCollider = false;

	public GameObject Drop;




	void Update()
	{
		//if (Input.GetMouseButtonDown(0))
		//{
		//	OnMouseDown();
		//}
	}

	void OnMouseDown()
	{
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		//if (!isCollider)
		//{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		//}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == Drop)
		{
			isCollider = true;
			collision.gameObject.GetComponent<Renderer>().material.color = Color.green;



			//gameObject.transform.position = new Vector3();
		}
    }

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject == Drop)
		{
			isCollider = false;
			collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	/// <summary>
	/// Verifica se o clique do mouse clicou no objeto
	/// </summary>
	/// <param name="hit"></param>
	/// <returns></returns>
	GameObject ReturnClickedObject(out RaycastHit hit)
	{
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
		{
			target = hit.collider.gameObject;
		}
		return target;
	} 
	//See more at: http://www.theappguruz.com/blog/drag-and-drop-any-game-object#sthash.GvYro3Gx.dpuf
}
