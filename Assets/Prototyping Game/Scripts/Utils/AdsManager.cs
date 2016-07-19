using UnityEngine;
using System;
using System.Collections;
#if !UNITY_EDITOR && UNITY_ANDROID
using GoogleMobileAds.Api;
#endif

public class AdsManager : MonoBehaviour {

#if UNITY_WSA_10_0
	public GameObject mAdDuplexManagement;
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
	string adUnitId;
	BannerView bannerView;
#endif

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR && UNITY_ANDROID
		adUnitId = "ca-app-pub-2302915872676550/5582174420";
		StartBanner();
#endif
#if UNITY_WSA_10_0
		mAdDuplexManagement.SetActive(true);
#endif
	}

#if !UNITY_EDITOR && UNITY_ANDROID
	private void StartBanner() {
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
#endif

}
