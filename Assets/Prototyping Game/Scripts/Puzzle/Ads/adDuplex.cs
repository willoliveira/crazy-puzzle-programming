using UnityEngine;
using System.Collections;

public class adDuplex : MonoBehaviour {

	// Use this for initialization
	void Start () {

		#if UNITY_WSA_10_0
			ShowAdsWindowsPhone ();
		#endif
	}

	private void ShowAdsWindowsPhone() {
		AdDuplexInterop.ShowAdControl();
	}
}
