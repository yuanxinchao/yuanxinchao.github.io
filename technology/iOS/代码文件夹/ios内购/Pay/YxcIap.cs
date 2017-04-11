using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

public class YxcIap
{
    



    public delegate void PayCallback(int numid,bool result);

    public static PayCallback BuyOrRestoreCb;

    //    static Dictionary<int, PayCallback> actDic = new Dictionary<int,PayCallback>();
    //    static PayCallback payCallback = null;

    public static void iosBuy (int numid , PayCallback ac)
    {
        #if UNITY_EDITOR
        return;
        #endif
        Debug.Log("YXC" + "添加numid为" + numid + "的Buy callback");
//        actDic [numid] = ac;
        BuyOrRestoreCb = ac;
        InitAndBuy(numid, PayResult);

    }

    public static void iosRestore (int numid , PayCallback ac)
    {
        #if UNITY_EDITOR
        return;
        #endif
        Debug.Log("YXC" + "添加numid为" + numid + "的restore callback");
//        actDic [numid] = ac;
        BuyOrRestoreCb = ac;
        RestoreBuy(numid, PayResult);
    }


    [DllImport("__Internal")]
    static extern void InitAndBuy (int numid , PayCallback payCb);

    [AOT.MonoPInvokeCallback(typeof(PayCallback))]
    static void PayResult (int numId , bool result)
    {

//        if (actDic.ContainsKey(numId))
//        {
        Debug.Log("YXC" + "numId 为" + numId + "CallBack");
//        actDic.TryGetValue(numId, out BuyOrRestoreCb);
//        actDic.Remove(numId);
        if (BuyOrRestoreCb != null)
        {
            BuyOrRestoreCb(numId, result);
        }
//        }
//        if (payCallback != null)
//        {
//            payCallback(numId, result);
//        }
    }


    //    public static void SetListernCallback (PayCallback payCb)
    //    {
    //        payCallback = payCb;
    //    }


    [DllImport("__Internal")]
    static extern void RestoreBuy (int numid , PayCallback payCb);

}
