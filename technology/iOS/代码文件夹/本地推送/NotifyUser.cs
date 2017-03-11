using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class NotifyUser
{
    private static string Message = "扎心了老铁:您已经好久没超越自己了，赶快回来玩一局吧！";
    private static int NotifyTime = 2 * 24 * 60 * 60;


    public static void RegisterLocalNotification ()
    {

        #if UNITY_EDITOR
        return;
        #endif
        Debug.Log("YXC" + "  unity  调用 开启本地推送");
        registerLocalNotification(NotifyTime, Message);
    }

    public static void CancelLocalNotification ()
    {
        #if UNITY_EDITOR
        return;
        #endif
        Debug.Log("YXC" + "  unity  调用 取消本地推送");
        cancelLocalNotification();
    }


    [DllImport("__Internal")]
    static extern void registerLocalNotification (int notifyTime , string message);

    [DllImport("__Internal")]
    static extern void cancelLocalNotification ();
}
