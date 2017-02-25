using UnityEngine;
using System.Collections;

public class GetSystemInfo : MonoBehaviour
{

	static AndroidJavaObject Context;
	static AndroidJavaObject SystemInfo;

	public static void Init ()
	{
		using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			Context = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
		}
		using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.tj.sdk.GetSystemInfo")) {
			if (pluginClass != null) {
				SystemInfo = pluginClass.CallStatic<AndroidJavaObject> ("Instance", Context);
			}
		}
	}

	public static string GetDisplay ()
	{
		return SystemInfo.Call<string> ("GetDisplay");
	}

	public static string GetBuildVersion ()
	{
		return SystemInfo.Call<string> ("GetBuildVersion");
	}

	public static string GetBuildModel ()
	{
		return SystemInfo.Call<string> ("GetBuildModel");
	}

	public static string GetIMEI ()
	{
		return SystemInfo.Call<string> ("GetIMEI");
	}
	//获取指定包名的Activity  
	public static AndroidJavaObject GetActivity (string package_name, string activity_name)
	{
		return new AndroidJavaClass (package_name).GetStatic<AndroidJavaObject> (activity_name);
	} 
	public static string GetIMSI ()
	{
		return SystemInfo.Call<string> ("GetIMSI");
	}
	public static string GetNetworkType ()
	{
		return SystemInfo.Call<string> ("GetNetworkType");
	}
	public static string GetCpuName ()
	{
		return SystemInfo.Call<string> ("GetCpuName");
	}
}
