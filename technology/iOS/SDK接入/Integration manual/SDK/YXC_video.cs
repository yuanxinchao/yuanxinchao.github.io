using UnityEngine;
using System.Collections;
using Heyzap;
using System.Collections.Generic;
using System;
using Umeng;
public class YXC_video : MonoBehaviour
{

	static Action finishtodo = null;
	public static bool showBanner = false;
	// Use this for initialization
	void Start ()
	{
		//*************初始化统计部分************//
		YXC_analyze.AnalyzeInit (Myparameters.Umeng_key, Myparameters.Channel_Id);
	
		//*************初始化heyzap视频banner************//
		HeyzapAds.Start (Myparameters.Heyzap_key, HeyzapAds.FLAG_NO_OPTIONS);
		HZIncentivizedAd.SetDisplayListener (ReceiveVideoCB);
		HZBannerAd.ShowWithOptions (null);//初始化banner
		HZBannerAd.SetDisplayListener (ReceiveHZBannerCB);
		GetIncentivizedVideo ();

	}
	#region  展示banner 视屏 插屏
	public static void PlayBanner (bool show)
	{
		showBanner = show;
		if (show) {
			HZBannerAd.ShowWithOptions (null);
		} else {
			HZBannerAd.Hide ();
		}
		//		AdsYuMiBannerUnity.setAdsYuMiViewHidden (!show);
	}

	public static void ShowVideo (Action ac=null)
	{
	
		if (ac != null) {
			finishtodo = ac;
		}
		
		Debug.Log ("heyzap video show");
		HZIncentivizedAd.Show ();
	}

	public static void ShowInterstitial ()
	{
		HZInterstitialAd.Show ();
	}

	#endregion

	void ReceiveVideoCB (string state, string tag)
	{


		if (state.Equals ("incentivized_result_complete")) {

			YXC_analyze.Events ("CompletedPlayVIDEO");
			Debug.Log ("heyazp视频播放完成，现在取下次视频");
			Debug.Log ("tag=" + tag);
			finishtodo ();
			HZIncentivizedAd.Fetch ();
		}
		if (state.Equals ("fetch_failed")) {
			Debug.Log ("heyazp视频获取失败，现在再去取视频");
			Debug.Log ("tag=" + tag);
			HZIncentivizedAd.Fetch ();
		}

		if (state.Equals ("available")) {
			Debug.Log ("heyazp视频获取成功");
			Debug.Log ("tag=" + tag);

		}
	}
	void ReceiveHZBannerCB (string state, string tag)
	{
		if (state.Equals ("loaded")) {
			Debug.Log ("bannerjiajiahao");
			PlayBanner (showBanner);
		}
		
	}
	#region 获取视频和插屏
	public static void GetIncentivizedVideo ()
	{
		HZIncentivizedAd.Fetch ();
		Debug.Log ("HeyzapAds.Fetch");
		
	}
	public static void GetInterstitial ()
	{
		HZInterstitialAd.Fetch ();
	}
	#endregion
	#region 判断视频和插屏是否加载好
	public static bool IsIncentivizedAvailable ()
	{
		
		if (HZIncentivizedAd.IsAvailable ()) 
			return true;
		else {
			Debug.Log ("heyzap video is no ready");
			GetIncentivizedVideo ();
		}
		
		return false;
	}
	public static bool IsInterstitialAvailable ()
	{
		if (HZInterstitialAd.IsAvailable ())
			return true;
		else {
			
			GetInterstitial ();
			return false;
		}
	}
	#endregion
}
