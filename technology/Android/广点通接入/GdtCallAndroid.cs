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
			Debug.Log ("YXC" + "  Gdt Call android Init");
			curActivity.Call ("Init", key [0], key [1]);
		}
	}
	public static void GetInterstitial ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			//			Debug.Log ("YXC" + "  Gdt Call GetInterstitial");
			curActivity.Call ("GetInterstitial");
		}
	}
	
	public static bool IsInterstitialAvailable ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			Debug.Log ("YXC" + "  Gdt Call android IsInterstitialAvailable");
			return curActivity.Call<bool> ("IsInterstitialAvailable");
		}
	}
	
	public static void ShowInterstitial ()
	{
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			//			Debug.Log ("YXC" + "  Gdt Call ShowInterstitial");
			curActivity.Call ("ShowInterstitial");
		}
	}
}
	// static AndroidJavaObject GdtAndroidClass;
	// public static void Init (string[] key)
	// {
	// 	using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.tj.GdtAds")) {
	// 		GdtAndroidClass = new AndroidJavaObject ("com.tj.GdtAds");
	// 	}
	// 	Debug.Log ("YXC" + "  Gdt Call android Init");
	// 	GdtAndroidClass.Call ("Init", key [0], key [1]);
	// }
	// public static void GetInterstitial ()
	// {
	// 	//			Debug.Log ("YXC" + "  Gdt Call GetInterstitial");
	// 	GdtAndroidClass.Call ("GetInterstitial");

	// }
	
	// public static bool IsInterstitialAvailable ()
	// {
	// 	Debug.Log ("YXC" + "  Gdt Call android IsInterstitialAvailable");
	// 	return GdtAndroidClass.Call<bool> ("IsInterstitialAvailable");
	// }
	
	// public static void ShowInterstitial ()
	// {
	// 	//			Debug.Log ("YXC" + "  Gdt Call ShowInterstitial");
	// 	GdtAndroidClass.Call ("ShowInterstitial");
	// }

	// public static void CallTest ()
	// {
	// 	GdtAndroidClass.Call ("Print");
	// }