using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

public class OulaCallPlatform
{
    static AndroidJavaClass jc = null;
    //jar包log标签
    private string jarLogTag = "Unity_Hola_Analysis";
    //Hola的log标签
    private string HolaLogTag = "Channel_";

    public static void InitWithProductId (string productId , string channelId , string Appid)
    {
        Debug.Log("YXC" + "  调用初始化方法InitWithProductIdOula productId=" + productId + "  channelId=" + channelId + "  Appid=" + Appid);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        InitWithProductIdOula(productId, channelId, "id" + Appid);
        #endif
        #if UNITY_ANDROID
        jc = new AndroidJavaClass("com.hola.Analysis");
        jc.CallStatic("init", productId, channelId);
        #endif
    }

    public static void LogWithKey (string eventKey , string eventValue)
    {
        Debug.Log("YXC" + "  常规打点 key=" + eventKey + "  value=" + eventValue);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        LogWithKeyOula(eventKey, eventValue);
        #endif
        #if UNITY_ANDROID
        jc.CallStatic("log", eventKey, eventValue);
        #endif
    }

    public static void LogWithOnlyKey (string eventKey)
    {
        Debug.Log("YXC" + "  常规打点 key=" + eventKey);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        LogWithOnlyKeyOula(eventKey);
        #endif
        #if UNITY_ANDROID
        jc.CallStatic("log", eventKey);
        #endif
    }

    public static string getStaToken ()
    {
        Debug.Log("YXC" + "  getStaToken");
        #if UNITY_EDITOR
        return "unity editor";
        #endif
        #if UNITY_IPHONE
        return getStaTokenOula();
        #endif
        #if UNITY_ANDROID
        return jc.CallStatic<string>("getStaToken");
        #endif
    }

    public static string getAndroidID ()
    {
        Debug.Log("YXC" + "  getAndroidID");
        #if UNITY_EDITOR
        return "unity editor";
        #endif
        #if UNITY_IPHONE
        return _getIDFA();
        #endif
        #if UNITY_ANDROID
        return jc.CallStatic<string>("getAndroidID");
        #endif
    }

    public static void CountWithKey (string eventKey)
    {
        Debug.Log("YXC" + "  自动计数打点 key=" + eventKey);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        CountWithKeyOula(eventKey);
        #endif
        #if UNITY_ANDROID
        jc.CallStatic("count", eventKey);
        #endif
    }

    public static void LogWithKeyAndDic (string eventKey , Dictionary<string,string> dic)
    {
        Debug.Log("YXC" + "  自动计数打点 key=" + eventKey);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        string jsonDic = MiniJSON.Json.Serialize(dic);
        Debug.Log("YXC" + "  统计打点的dic为" + jsonDic);
        string ob = (string)MiniJSON.Json.Serialize(dic);
        LogWithKeyAndDicOula(eventKey, ob);
        #endif
        #if UNITY_ANDROID
        string jsonDic = MiniJSON.Json.Serialize(dic);
        Debug.Log("YXC" + "  统计打点的dic为" + jsonDic);
        object ob = (object)MiniJSON.Json.Serialize(dic);
        jc.CallStatic("log", "myEvent3", (object)ob);
        #endif
    }

    /// <summary>
    /// 用于统计游戏活跃用户，游戏接入时必须调用该接口。该段代码添加于CP方认可的第一个用户活跃行为的位置，如：进入游戏大厅，连接服务器等。
    /// </summary>
    public static void GAPLog ()
    {
        Debug.Log("YXC" + "  活跃打点GAPLog");
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        GAPLogOula();
        #endif
        #if UNITY_ANDROID
        #endif

    }

    /// <summary>
    /// 用于将游戏自定义的数据统计到Hola后台
    /// </summary>
    /// <param name="eventKey">Event key.</param>
    /// <param name="eventValue">Event value.</param>
    public static void CustomLogWithKey (string eventKey , string eventValue)
    {
        Debug.Log("YXC" + "  自定义打点event= " + eventKey + "  eventValue=" + eventValue);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        CustomLogWithKeyOula(eventKey, eventValue);
        #endif
        #if UNITY_ANDROID
        #endif
    }

    public static void STDebugManager (bool openLog)
    {
        Debug.Log("YXC" + "  开启Log");
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        STDebugManagerOula(openLog);
        #endif
        #if UNITY_ANDROID
        #endif
    }

    public static void LogPaymentWithPlayerId (string playerId , string receiptDataString)
    {
        Debug.Log("YXC" + "  开启Log");
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        LogPaymentWithPlayerIdOula(playerId, receiptDataString);
        #endif
        #if UNITY_ANDROID
        #endif
    }

    public static void FacebookLoginWithGameId (string playerId , string openId , string openToken)
    {
        Debug.Log("YXC" + "  facebook登陆打点  playerId=" + playerId + "  openId=" + openId + "  openToken=" + openToken);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        FacebookLoginWithGameIdOula(playerId, openId, openToken);
        #endif
        #if UNITY_ANDROID
        jc.CallStatic("facebookLogin", playerId, openId, openToken);
        #endif
    }

    public static void guestLoginWithGameId (string playerId)
    {
        Debug.Log("YXC" + "  游客登陆打点  playerId=" + playerId);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE
        guestLoginWithGameIdOula(playerId);
        #endif
        #if UNITY_ANDROID
        jc.CallStatic("guestLogin", playerId);
        #endif
    }

    public static void portalLoginWithPlayerId (string playerId , string portalId)
    {
        Debug.Log("YXC" + "  平台登陆打点  playerId=" + playerId + "  portalId=" + portalId);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE

        #endif
        #if UNITY_ANDROID
        jc.CallStatic("portalLogin", playerId, portalId);
        #endif
    }

    public static void ThirdpartyServiceLogPaymentWithPlayerId (string gameAccountId ,
                                                                string gameAccountServer ,
                                                                string iabPurchaseOriginalJson ,
                                                                string iabPurchaseSignature)
    {
        Debug.Log("YXC" + "  支付打点  gameAccountId=" + gameAccountId + "  gameAccountServer=" + gameAccountServer);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE

        #endif
        #if UNITY_ANDROID
        jc.CallStatic("logPaymentWithServer", gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature);
        #endif

    }

    public static void ThirdpartyLogPaymentWithPlayerId (string gameAccountId ,
                                                         string iabPurchaseOriginalJson ,
                                                         string iabPurchaseSignature)
    {
        Debug.Log("YXC" + "  支付打点  gameAccountId=" + gameAccountId + "  iabPurchaseOriginalJson=" + iabPurchaseOriginalJson);
        #if UNITY_EDITOR
        return;
        #endif
        #if UNITY_IPHONE

        #endif
        #if UNITY_ANDROID
        jc.CallStatic("logPayment", gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature);
        #endif

    }

    [DllImport("__Internal")]
    static extern void InitWithProductIdOula (string productId , string channelId , string Appid);

    [DllImport("__Internal")]
    static extern void LogWithKeyOula (string eventKey , string eventValue);

    [DllImport("__Internal")]
    static extern void LogWithOnlyKeyOula (string eventKey);

    [DllImport("__Internal")]
    static extern void CountWithKeyOula (string eventKey);

    [DllImport("__Internal")]
    static extern void GAPLogOula ();

    [DllImport("__Internal")]
    static extern void CustomLogWithKeyOula (string eventKey , string eventValue);

    [DllImport("__Internal")]
    static extern void STDebugManagerOula (bool openLog);

    [DllImport("__Internal")]
    static extern void LogPaymentWithPlayerIdOula (string playerId , string receiptDataString);

    [DllImport("__Internal")]
    static extern void FacebookLoginWithGameIdOula (string playerId , string openId , string openToken);

    [DllImport("__Internal")]
    static extern void guestLoginWithGameIdOula (string playerId);

    [DllImport("__Internal")]
    static extern void LogWithKeyAndDicOula (string eventKey , string dic);

    [DllImport("__Internal")]
    static extern string getStaTokenOula ();

    [DllImport("__Internal")]
    static extern string _getIDFA ();
}
