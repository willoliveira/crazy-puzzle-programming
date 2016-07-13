using UnityEngine;
using System.Collections;

using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour {

	string adUnitId;
	BannerView bannerView;

	// Use this for initialization
	void Start () {

#if UNITY_ANDROID
		adUnitId = "ca-app-pub-2302915872676550/5582174420";
		StartBannerGoogle();
#endif
	}

	private void StartBannerGoogle() {
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		bannerView.OnAdLoaded += HandleAdLoaded;
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}


	public void HandleAdLoaded(object sender, EventArgs args)
	{
		bannerView.Show();
	}

}
