using UnityEngine;
using System.Collections;

/// <summary>
/// 广电通调用android方法
/// </summary>
public class GdtCallAndroid
{
	public static void Init (string[] key)
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			curActivity.Call ("Init", key [0], key [1]);
		}
	}
	
	
	public static void GetInterstitial ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			curActivity.Call ("GetInterstitial");
		}
	}
	
	public static bool IsInterstitialAvailable ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			return curActivity.Call<bool> ("IsInterstitialAvailable");
		}
	}
	
	public static void ShowInterstitial ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			Debug.Log ("Gdt Call ShowInterstitial");
			curActivity.Call ("ShowInterstitial");
		}
	}
	
}
