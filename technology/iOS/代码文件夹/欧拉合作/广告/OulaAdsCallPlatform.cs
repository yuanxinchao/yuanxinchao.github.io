using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class OulaAdsCallPlatform
{
    public static void Init (params string[] key)
    {
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        InitHola();
        #endif

        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android init banner key=" + key [0] + "inter key=" + key [1]);

        InitAlimamaAndroid(key);
        #endif
    }

    public static void ShowBanner (bool bo)
    {
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        ShowHolaBanner(bo);
        #endif

        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=" + bo);
        ShowAliBannerAndroid(bo);
        #endif
    }

    public static void ShowInterstitial (HolaCallback cb = null)
    {

        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        holaCallback = cb;
        ShowHolaInterstitial(HolaiOSCallback);
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=");
        ShowAliInterstitialAndroid();
        #endif
    }

    public static void ShowRewardVideo (string placementId , HolaCallback cb = null)
    {

        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        holaCallback = cb;
        ShowHolaRewardVideo(placementId, HolaiOSCallback);
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=");
        ShowAliInterstitialAndroid();
        #endif
    }


    public static bool IsRewardVideoReady ()
    {

        #if UNITY_EDITOR
        return false;
        #endif
        #if UNITY_IPHONE
        return IsHolaRewardVideoReady();
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=");
        ShowAliInterstitialAndroid();
        #endif
    }

    public static bool IsInterstitialReady ()
    {

        #if UNITY_EDITOR
        return false;
        #endif
        #if UNITY_IPHONE
        return IsHolaInterstitialReady();
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android show  show=");
        ShowAliInterstitialAndroid();
        #endif
    }



    public static void LoadInterstitial ()
    {
        LoadHolaInterstitial();
    }

    public static void LoadIncent ()
    {
        LoadHolaIncent();
    }
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


   



    static HolaCallback holaCallback;

    [AOT.MonoPInvokeCallback(typeof(HolaCallback))]
    static void HolaiOSCallback (string infoCallback)
    {
        if (holaCallback != null)
        {
            holaCallback(infoCallback);
        }
    }



    public delegate void HolaCallback(string infoCallback);

    [DllImport("__Internal")]
    static extern void InitHola ();

    [DllImport("__Internal")]
    static extern void ShowHolaBanner (bool bo);

    [DllImport("__Internal")]
    static extern void ShowHolaInterstitial (HolaCallback holaCallback);

    [DllImport("__Internal")]
    static extern void ShowHolaRewardVideo (string placementId , HolaCallback holaCallback);

    [DllImport("__Internal")]
    static extern bool IsHolaRewardVideoReady ();

    [DllImport("__Internal")]
    static extern bool IsHolaInterstitialReady ();

    [DllImport("__Internal")]
    static extern void LoadHolaInterstitial ();

    [DllImport("__Internal")]
    static extern void LoadHolaIncent ();
}
