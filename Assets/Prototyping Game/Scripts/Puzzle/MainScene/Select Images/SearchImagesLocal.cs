using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using PickerImageFile;

public class SearchImagesLocal : MonoBehaviour
{

	Plugin p = new Plugin();

	void Start()
	{
		Debug.Log(p.GetMessage());
		p.OpenFilePicker(response =>
		{
			Debug.Log(response);
		});


	}

}
