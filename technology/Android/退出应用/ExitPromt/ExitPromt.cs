using UnityEngine;
using System.Collections;

public class ExitPromt
{
    public delegate void ExitDelegate(bool bo);

    public static void ExitGame (ExitDelegate exitCallback = null)
    {
        #if UNITY_ANDROID
        var androidExitListener = new AndroidExitListener(exitCallback);
        Debug.Log("YXC" + "  Android端调用退出  ");
        try
        {
            var exitPromptGame = new AndroidJavaClass("com.tj.ExitPrompt");
            exitPromptGame.CallStatic("ExitPromptGame", Singleton<Localization>.instance.GetText("1"), 
                Singleton<Localization>.instance.GetText("29"),
                Singleton<Localization>.instance.GetText("30"),
                Singleton<Localization>.instance.GetText("31"),
                androidExitListener);
        } catch (AndroidJavaException exp)
        {
            Debug.Log("YXC" + "  Android端异常  Message=" + exp.Message);
        }
        #endif
    }

    class AndroidExitListener : AndroidJavaProxy
    {

        ExitDelegate exitCallback = null;

        public AndroidExitListener (ExitDelegate exitCallback)
            : base("com.tj.ExitListener")
        {
            this.exitCallback = exitCallback;
        }

        public void onExit (bool bo)
        {
            exitCallback(bo);
        }
    }
}
