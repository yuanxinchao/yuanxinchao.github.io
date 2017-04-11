using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class JudgeLanguage
{
    public static string JudgeWhichCh ()
    {

        #if UNITY_EDITOR
        return "CN";
        #endif

        #if  UNITY_IPHONE
        string lang = CurIOSLang();
        if (lang.Contains("Hans"))
        {
            return "CN";
        } else
            return "TR";
        #endif

        #if UNITY_ANDROID
        AndroidJavaObject Context;

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
        Context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.tj.Location"))
        {
        if (pluginClass != null)
        {
        AndroidJavaObject SystemInfo = pluginClass.CallStatic<AndroidJavaObject>("Instance", Context);
        info = SystemInfo.Call<string>("GetLanguage");
        }
        }
        if (info.Contains("TW"))
        {
        return "TR";

        }
        else{
        return "CN";
        }
        #endif
    }

    [DllImport("__Internal")]
    static extern string CurIOSLang ();
}
