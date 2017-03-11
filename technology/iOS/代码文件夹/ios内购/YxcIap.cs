using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class YxcIap
{
    public delegate void PayCallback(int numid,bool result);

    static PayCallback payCallback = null;

    public static void iosBuy (int numid)
    {
        #if UNITY_EDITOR
        return;
        #endif
        InitAndBuy(numid, PayResult);

    }

    public static void iosRestore (int numid)
    {
        #if UNITY_EDITOR
        return;
        #endif
        RestoreBuy(numid, PayResult);

    }


    [DllImport("__Internal")]
    static extern void InitAndBuy (int numid , PayCallback payCb);

    [AOT.MonoPInvokeCallback(typeof(PayCallback))]
    static void PayResult (int numId , bool result)
    {
        if (payCallback != null)
        {
            payCallback(numId, result);
        }
    }


    public static void SetListernCallback (PayCallback payCb)
    {
        payCallback = payCb;
    }


    [DllImport("__Internal")]
    static extern void RestoreBuy (int numid , PayCallback payCb);

}
