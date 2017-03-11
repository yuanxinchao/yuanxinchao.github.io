using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 广电通的广告调用
/// </summary>
public class GdtAds:BaseAds<GdtAds>
{
	public static bool showBanner = false;
	static Action finishtodo = null;
	
	
	public GdtAds ()
	{
		
	}
	
	public override void Init (params string[] key)
	{
		Debug.Log ("Gdt Init AppId" + key [0]);
		GdtCallAndroid.Init (key);
		GetInterstitial ();
	}
	
	#region  展示banner 视屏 插屏
	
	public override void PlayBanner (bool show)
	{
		showBanner = show;
		if (show) {
			
		} else {
			
		}
		//      AdsYuMiBannerUnity.setAdsYuMiViewHidden (!show);
	}
	
	public override void ShowIncentVideo (Action ac = null)
	{
		
		if (ac != null) {
			finishtodo = ac;
		} else {
			finishtodo = null;
		}
		
		ShowRewardedInterstitial ();
	}
	
	public override void ShowInterVideo (Action ac = null)
	{
		
		if (ac != null) {
			finishtodo = ac;
		} else {
			finishtodo = null;
		}
		
		ShowInterstitial ();
	}
	
	public override void ShowInterstitial ()
	{
		Debug.Log ("Gdt 调用播放广告");
		GdtCallAndroid.ShowInterstitial ();
	}
	
	public override void ShowRewardedInterstitial ()
	{
	}
	
	#endregion
	
	public override void AdsCallback (string ev)
	{
		Debug.Log ("qqqqqpppppp" + ev);
		if (ev.Contains ("LOADREWARDEDFAILED")) {
			// A rewarded video failed to load.
			Debug.Log ("load Gdt 插屏 fail go to load again");
			GetInterstitial ();
		} else if (ev.Contains ("HIDDENREWARDED")) {
			// A rewarded video was closed.  Preload the next rewarded video.
			Debug.Log ("Gdt 播放完成，现在取下次插屏");
			if (finishtodo != null)
				finishtodo ();
			GetInterstitial ();
		}
	}
	
	#region 获取视频和插屏
	
	public override void GetIncentivizedVideo ()
	{
	}
	
	public override void GetInterstitial ()
	{
		GdtCallAndroid.GetInterstitial ();
	}
	
	#endregion
	
	#region 判断视频和插屏是否加载好
	
	public override bool IsIncentivizedAvailable ()
	{
		return false;
	}
	
	public override bool IsInterstitialAvailable ()
	{
		Debug.Log ("检测 Gdt 插频状态");
		if (GdtCallAndroid.IsInterstitialAvailable ()) {
			Debug.Log ("Gdt 插频加载好啦");
			return true;
		} else {
			Debug.Log ("Gdt 插频没加载好");
			GetInterstitial ();
			return false;
		}
	}
	
	#endregion
	
}
