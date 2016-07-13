using UnityEngine;
using System.Collections;

using AdDuplexUnitySDK;

#if UNITY_ANDROID
using GoogleMobileAds.Api;
#endif
using System;

public class AdsManager : MonoBehaviour {

	public AdDuplexUnitySDK.Assets.Plugins.AdDuplexManagement mAdDuplexManagement;
	string adUnitId;
	#if UNITY_ANDROID
	BannerView bannerView;
	#endif

	// Use this for initialization
	void Start () {

#if UNITY_ANDROID
		adUnitId = "ca-app-pub-2302915872676550/5582174420";
#endif
#if UNITY_WSA_10_0
		adUnitId = "ca-app-pub-2302915872676550/5582174420";
#endif
		StartBanner();
	}

	private void StartBanner() {
#if UNITY_ANDROID
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		bannerView.OnAdLoaded += HandleAdLoaded;
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
#endif
#if UNITY_WSA_10_0
#endif
	}

#if UNITY_ANDROID
	public void HandleAdLoaded(object sender, EventArgs args)
	{
		bannerView.Show();
	}
#endif

}
