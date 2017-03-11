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
