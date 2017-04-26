using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class OulaAdsCallPlatform
{
    static AndroidJavaClass jc = null;

    public static void Init (params string[] key)
    {
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        InitHola();
        #endif

        #if UNITY_ANDROID
        Debug.Log("YXC" + "   init banner key=" + string.Join(",", key));
        jc = new AndroidJavaClass("com.holaads.Main");

        jc.CallStatic("initBanner");
        jc.CallStatic("initInsert");
        jc.CallStatic("initVideo");

        jc.CallStatic("setCallBack", "TjSdk");
        jc.CallStatic("setCallBack", "TjSdk", "OnOulaADCallBack");
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
        Debug.Log("YXC" + "  Call android ShowBanner  show=" + bo);
        if (bo)
            jc.CallStatic("showBanner");
        else
            jc.CallStatic("closeBanner");
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
        Debug.Log("YXC" + "  Call android showInsert");
        jc.CallStatic("showInsert");
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
        Debug.Log("YXC" + "  Call android showVideo ");
        jc.CallStatic("showVideo");
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
        Debug.Log("YXC" + "  Call android IsRewardVideoReady");
        return  jc.CallStatic<bool>("isVideoReady");
        #endif
        return false;
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
        Debug.Log("YXC" + "  Call android IsInterstitialReady");
        return  jc.CallStatic<bool>("isInsertReady");
        return false;
        #endif
        return false;
    }



    public static void LoadInterstitial ()
    {
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        LoadHolaInterstitial();
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android LoadInterstitial");
        jc.CallStatic("loadInsert");
        #endif
 
    }

    public static void LoadIncent ()
    {
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        LoadHolaIncent();
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  pretent to Call android LoadIncent");
        #endif

    }

    public static void IsInterstitialReadyCb ()
    {

        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android IsInterstitialReadyCb");
        jc.CallStatic("isInsertReady");
        #endif
    }

    public static void IsIncentAvailableCb ()
    {

        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Call android IsIncentAvailableCb");
        jc.CallStatic("isVideoReady");
        #endif
    }




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
