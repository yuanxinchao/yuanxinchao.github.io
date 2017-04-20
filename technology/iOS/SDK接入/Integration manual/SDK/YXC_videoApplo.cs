using UnityEngine;
using System.Collections;
using System;
public class YXC_videoApplo : MonoBehaviour
{
	public static bool showBanner = false;
	static Action finishtodo = null;
	void Start ()
	{
		AppLovin.SetSdkKey (Myparameters.Applovin_key);
		AppLovin.InitializeSdk ();
		AppLovin.SetUnityAdListener ("TJSDK");
		GetIncentivizedVideo ();
	}

	#region  展示banner 视屏 插屏
	public static void PlayBanner (bool show)
	{
		showBanner = show;
		if (show) {

		} else {

		}
		//		AdsYuMiBannerUnity.setAdsYuMiViewHidden (!show);
	}
	
	public static void ShowVideo (Action ac=null)
	{
		
		if (ac != null) {
			finishtodo = ac;
		}

		AppLovin.ShowRewardedInterstitial ();
	}
	
	public static void ShowInterstitial ()
	{
		AppLovin.ShowInterstitial ();
	}
	
	#endregion
	public void onAppLovinEventReceived (string ev)
	{
		Debug.Log ("qqqqqpppppp" + ev);
		if (ev.Contains ("REWARDAPPROVEDINFO")) {
			
			// The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
			
			// Split the string based on the delimeter
			string[] split = ev.Split ('|');
			
			// Pull out the currency amount
			double amount = double.Parse (split [1]);
			
			// Pull out the currency name
			string currencyName = split [2];
			
			// Do something with the values from above.  For example, grant the coins to the user.
			Debug.Log ("complete applovin video");
		} else if (ev.Contains ("LOADEDREWARDED")) {
			// A rewarded video was successfully loaded.
		} else if (ev.Contains ("LOADREWARDEDFAILED")) {
			// A rewarded video failed to load.
			Debug.Log ("load applovin fail go to load again");
			GetIncentivizedVideo ();
		} else if (ev.Contains ("HIDDENREWARDED")) {
			// A rewarded video was closed.  Preload the next rewarded video.
			Debug.Log ("applovin视频播放完成，现在取下次视频");
			finishtodo ();
			GetIncentivizedVideo ();
		}
	}

	#region 获取视频和插屏
	public static void GetIncentivizedVideo ()
	{
		AppLovin.LoadRewardedInterstitial ();
		
	}
	public static void GetInterstitial ()
	{
		AppLovin.PreloadInterstitial ();
	}
	#endregion
	#region 判断视频和插屏是否加载好
	public static bool IsIncentivizedAvailable ()
	{
		
		if (AppLovin.IsIncentInterstitialReady ()) 
			return true;
		else {
			GetIncentivizedVideo ();
		}
		
		return false;
	}
	public static bool IsInterstitialAvailable ()
	{
		if (AppLovin.HasPreloadedInterstitial ())
			return true;
		else {
			GetInterstitial ();
			return false;
		}
	}
	#endregion
}