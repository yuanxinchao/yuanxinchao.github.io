using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class NotifyUser
{
    private static int NotifyTime = 2 * 24 * 60 * 60;

    public static void RegisterLocalNotification (string message)
    {

        #if UNITY_EDITOR
        return;
        #endif

        #if  UNITY_IPHONE
        Debug.Log("YXC" + "  unity  调用 开启本地推送");



        registerLocalNotification(NotifyTime, message);
        #endif
    }

    public static void CancelLocalNotification ()
    {
        #if UNITY_EDITOR
        return;
        #endif

        #if  UNITY_IPHONE
        Debug.Log("YXC" + "  unity  调用 取消本地推送");
        cancelLocalNotification();
        #endif
    }

    /// <summary>
    /// 调整广告出现时间，避免夜间推送
    /// </summary>
    private void JudgeTime ()
    {
      
    }


    [DllImport("__Internal")]
    static extern void registerLocalNotification (int notifyTime , string message);

    [DllImport("__Internal")]
    static extern void cancelLocalNotification ();
}
