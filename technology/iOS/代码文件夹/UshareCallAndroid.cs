using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
public class UshareCallAndroid
{
	public static void InitUmengKey (string[] key)
	{
		#if UNITY_ANDROID
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			Debug.Log ("YXC" + "  Umeng Call android InitKey");
			curActivity.Call ("InitUmengKey", key [0], key [1], key [2], key [3], key [4], key [5]);
		}
		#elif UNITY_IPHONE
		InitUmengKeyiOS (key [0], key [1], key [2], key [3], key [4], key [5], Myparameters.Umeng_key);
		#endif
	}













	[DllImport("__Internal")]
	static extern void InitUmengKeyiOS (string QqKey, string QqSecret, string WeixinKey, string WeixinSecret, string SinaKey, string SinaSecret, string UmengKey);
}
