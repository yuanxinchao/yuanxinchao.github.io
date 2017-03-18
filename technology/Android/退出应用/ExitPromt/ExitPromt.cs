using UnityEngine;
using System.Collections;

public class ExitPromt
{

	public static void ExitGame ()
	{
		#if UNITY_ANDROID
		Debug.Log("YXC" + "  Android端调用退出  ");
        var exitPromptGame = new AndroidJavaClass("com.tj.ExitPrompt");
        exitPromptGame.CallStatic("ExitPromptGame", Singleton<Localization>.instance.GetText("1"), 
            Singleton<Localization>.instance.GetText("29"),
            Singleton<Localization>.instance.GetText("30"),
            Singleton<Localization>.instance.GetText("31"));
		#endif
	}

}
