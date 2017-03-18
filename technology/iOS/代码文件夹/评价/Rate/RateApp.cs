using UnityEngine;
using System.Collections;
using System;

public class RateApp
{
    private static long OriginTime
    {
        set { PlayerPrefs.SetString("OriginTime", value.ToString()); }
        get { return long.Parse(PlayerPrefs.GetString("OriginTime", "0")); }
    }

    private static bool HasRate
    {
        set{ PlayerPrefs.SetInt("HasRate", Convert.ToInt32(value)); }
        get{ return Convert.ToBoolean(PlayerPrefs.GetInt("HasRate", 0)); }
    }

    private static int day = 2;

    public static void GoRateApp ()
    {
        #if UNITY_EDITOR
        return;
        #endif
        Debug.Log("YXC" + "  GoRateApp  ");
        //如果第一次进游戏记录时间，2天后提醒评价
        if (Tutorial.Tutor)
        {
            Debug.Log("YXC" + "  第一次进入，记录时间  ");
            OriginTime = System.DateTime.Now.Ticks;
        }

        TimeSpan timeSpan = new TimeSpan(System.DateTime.Now.Ticks - OriginTime);
        if (timeSpan.TotalDays > day && !HasRate)
        {
            Debug.Log("YXC" + "  未评价且时间到，提示评价  ");
            OriginTime = System.DateTime.Now.Ticks;

            #if UNITY_ANDROID
            Debug.Log("YXC" + "  Android端调用评价  ");
            var rateApp = new AndroidJavaClass("com.tj.RateApp");
            rateApp.CallStatic("ShowNote", Singleton<Localization>.instance.GetText("1"), Singleton<Localization>.instance.GetText("28"), Singleton<Localization>.instance.GetText("30"), Singleton<Localization>.instance.GetText("31"));
            #endif

            #if UNITY_IPHONE
            IOSFunction.RateApp(Myparameters.AppStoreId,
                (bool bo) =>
                {
                    HasRate = bo;
                });
            #endif
        }
    }
            
}
