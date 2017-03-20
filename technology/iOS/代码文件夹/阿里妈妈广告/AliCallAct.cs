using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class AliCallAct
{
    public static void Init (string[] key)
    {
        #if UNITY_IPHONE
		Debug.Log ("YXC" + "  Call iOS init key=" + key [0]);
		InitAlimamaiOS (key[0]);
        #endif

        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android init banner key=" + key [0] + "inter key=" + key [1]);

        InitAlimamaAndroid(key);
        #endif
    }

    public static void ShowBanner (bool bo)
    {
        #if UNITY_IPHONE
		Debug.Log ("YXC" + "  Call iOS show  show=" + bo);
		ShowAliBanner (bo);
        #endif

        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=" + bo);
        ShowAliBannerAndroid(bo);
        #endif
    }

    public static void ShowInterstitial ()
    {


        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=");
        ShowAliInterstitialAndroid();
        #endif
    }

    [DllImport("__Internal")]
    static extern void InitAlimamaiOS (string aliKey);

    [DllImport("__Internal")]
    static extern void ShowAliBanner (bool bo);


    #if UNITY_ANDROID
    static AndroidJavaObject AlimamaAndroidClass;

    static void InitAlimamaAndroid (string[] key)
    {

        AlimamaAndroidClass = new AndroidJavaObject("com.tj.Alimama");
        AlimamaAndroidClass.Call("initYYB", key [0], key [1]);
    }

    public static void ShowAliBannerAndroid (bool show)
    {

        AlimamaAndroidClass.Call("ShowBanner", show);
    }

    public static void ShowAliInterstitialAndroid ()
    {
        AlimamaAndroidClass.Call("ShowInterstitial");
    }
    #endif
}
