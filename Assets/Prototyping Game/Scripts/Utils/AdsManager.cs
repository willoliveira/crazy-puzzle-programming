using UnityEngine;
using System.Collections;

using GoogleMobileAds.Api;


public class AdsManager : MonoBehaviour {

	string adUnitId;

	// Use this for initialization
	void Start () {

		#if UNITY_ANDROID
		adUnitId = "ca-app-pub-9314300016729396/3916609662";
		StartBannerGoogle();
		#endif
	}

	private void StartBannerGoogle() {
		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

}
