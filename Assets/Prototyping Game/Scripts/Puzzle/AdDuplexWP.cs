using UnityEngine;
using System.Collections;

public class AdDuplexWP : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine(StartAds());
	}

	private IEnumerator StartAds()
	{
		yield return new WaitForSeconds(5);

		ShowAdControlTapped();
	}

	private void ShowAdControlTapped()
	{
		AdDuplexInterop.ShowAdControl();
	}
}
