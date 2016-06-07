using UnityEngine;
using System.Collections;

public class GameobjectDragAndDrop : MonoBehaviour
{

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

	void OnMouseUp()
	{
		Debug.Log(isCollider);
		if (isCollider)
		{
			Debug.Log("gameObject: " + gameObject.transform.localPosition + " - collision: " + Drop.transform.localPosition);
			Debug.Log("gameObject: " + gameObject + " - collision: " + Drop);
			gameObject.transform.position = Drop.transform.position;
		}
	}

	void OnMouseDrag()
	{
		//todo: pensar em como fazer a limitação de onde a peça pode se mover
		//if (!isCollider)
		//{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		//}
	}

	void OnTriggerEnter2D(Collider2D collision) {

		Debug.Log("OnTriggerEnter2D");
		Debug.Log(collision.gameObject);
		Debug.Log(Drop);

		if (collision.gameObject == Drop)
		{
			isCollider = true;
			//collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("OnTriggerExit2D");
		if (collision.gameObject == Drop)
		{
			isCollider = false;
			//collision.gameObject.GetComponent<Renderer>().material.color = Color.red;

		}
	}


	//void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (collision.gameObject == Drop)
	//	{
	//		isCollider = true;
	//		collision.gameObject.GetComponent<Renderer>().material.color = Color.green;			
	//	}
	//}

	//void OnCollisionExit2D(Collision2D collision)
	//{
	//	if (collision.gameObject == Drop)
	//	{
	//		isCollider = false;
	//		collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
	//	}
	//}

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
