using UnityEngine;
using System.Collections;
using System;

namespace CountingSheeps.NewOldPuzzle
{
	public static class AdDuplexInterop
	{
		/// <summary>
		/// 
		/// </summary>
		public static event EventHandler LoadInterstitialEvent;

		/// <summary>
		/// 
		/// </summary>
		public static event EventHandler ShowInterstitialEvent;

		/// <summary>
		/// 
		/// </summary>
		public static event EventHandler AdControlShow;

		/// <summary>
		/// 
		/// </summary>
		public static event EventHandler AdControlHide;

		public static event EventHandler OnInterstitialClosed;

		/// <summary>
		/// 
		/// </summary>
		public static void ShowAdControl()
		{
			if (AdControlShow != null)
			{
				AdControlShow(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static void HideAdControl()
		{
			if (AdControlHide != null)
			{
				AdControlHide(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static void LoadInterstitialAd()
		{
			if (LoadInterstitialEvent != null)
			{
				LoadInterstitialEvent(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static void ShowInterstitialAd()
		{
			if (ShowInterstitialEvent != null)
			{
				ShowInterstitialEvent(null, EventArgs.Empty);
			}
		}

		public static void InterstitialClose()
		{
			if (OnInterstitialClosed != null)
			{
				OnInterstitialClosed(null, EventArgs.Empty);
			}
		}
	}
}