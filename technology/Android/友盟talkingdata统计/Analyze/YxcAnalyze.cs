using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class YxcAnalyze
{
    public static void AnalyzeInit ()
    {
     
      
        //*************初始化友盟统计************//
        Umeng.GA.StartWithAppKeyAndChannelId(Myparameters.Umeng_key, Myparameters.Channel_Id);
        Debug.Log("Umeng_key:" + Myparameters.Umeng_key + ",Channel_Id:" + Myparameters.Channel_Id);
        //*************其他统计的初始化************//
        TalkingDataGA.OnStart(Myparameters.TalkingData_key, Myparameters.Channel_Id + Myparameters.Version);
        TDGAAccount.SetAccount(SystemInfo.deviceUniqueIdentifier);
        Debug.Log("TalkingData_key:" + Myparameters.TalkingData_key
        + ",Channel_Id:" + Myparameters.Channel_Id
        + ",Version:" + Myparameters.Version
        + ",Uuid:" + SystemInfo.deviceUniqueIdentifier);
    }

    public static void Events (string event_name)
    {
        Umeng.GA.Event(event_name);
        TalkingDataGA.OnEvent(event_name, null);
    }

    public static void EventsBegin (string s)
    {
        Umeng.GA.EventBegin(s);
        TDGAMission.OnBegin(s);
    }

    public static void EventsEnd (string s)
    {
        Umeng.GA.EventEnd(s);
        TDGAMission.OnCompleted(s);
    }

    /// <summary>
    /// 在游戏启动后(包含游戏刚开启或从后台恢复到前台)，调用 OnStart，该接口完成统计模块的初始化和统计 session 的创建，所以越早调用越好。
    /// </summary>
    public static void OnStart ()
    {
        Debug.Log("YXC" + "   开始啦 ");
        TalkingDataGA.OnStart(Myparameters.TalkingData_key, Myparameters.Channel_Id + Myparameters.Version);
    }

    /// <summary>
    /// 在游戏结束时(包含切出游戏和退到后台情况，如点击home、锁屏等按键)，调用 OnEnd。
    /// </summary>
    public static void OnEnd ()
    {
        Debug.Log("YXC" + "   退出啦  ");
        TalkingDataGA.OnEnd();
    }
}
