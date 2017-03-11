using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
public class IOSFunction
{
	public delegate void RateCallback (bool bo);
	static RateCallback rateCB;
	//评价应用
	public static void RateApp (string appid, RateCallback RCB=null)
	{
		if (RCB != null) {
			rateCB = RCB;
		}
		_RateApp1 (appid, rateCallback);
	}
	[DllImport("__Internal")]
	static extern void _RateApp1 (string appid, RateCallback RCB);

	[AOT.MonoPInvokeCallback(typeof(RateCallback))]
	static void rateCallback (bool bo)
	{
		if (rateCB != null) {
			rateCB (bo);
		}
	}



	//拷贝
	public static void CopyURL (string url)
	{
		_CopyURL (url);
	}
	[DllImport("__Internal")]
	static extern void _CopyURL (string url);

	//打开微信
	public static void OpenWX ()
	{
		_OpenWX ();
	}
	[DllImport("__Internal")]
	static extern void _OpenWX ();

	//获取ios设备等信息
//	public static void GetUserInfo (Action<string,string,string,string> uerInfor)
//	{
//		UerInforCallBack = uerInfor;
//		_GetUserInfo (GetUserInfoCallBack);
//	}
//	static Action<string,string,string,string> UerInforCallBack = null;
//	[AOT.MonoPInvokeCallback(typeof(Action))]
//	static void GetUserInfoCallBack (string idfa, string device, string phoneVersion, string phoneModel)
//	{
//		if (UerInforCallBack != null) {
//			UerInforCallBack (idfa, device, phoneVersion, phoneModel);
//		}
//	}
//	[DllImport("__Internal")]
//	static extern void _GetUserInfo (Action<string,string,string,string> callback);
}
public class IOSGetUserInfo
{
	public static string getIDFA ()
	{
		return _getIDFA ();
	}
	[DllImport("__Internal")]
	static extern string _getIDFA ();

	public static string getResolution ()
	{
		return _getResolution ();
	}
	[DllImport("__Internal")]
	static extern string _getResolution ();

	public static string getPhoneVersion ()
	{
		return _getPhoneVersion ();
	}
	[DllImport("__Internal")]
	static extern string _getPhoneVersion ();

	public static string getPhoneModel ()
	{
		return _getPhoneModel ();
	}
	[DllImport("__Internal")]
	static extern string _getPhoneModel ();

	public static void getNetType ()
	{
		_getNetType ();
	}
	[DllImport("__Internal")]
	static extern void _getNetType ();

	public static string getIMSI ()
	{
		return _getIMSI ();
	}
	[DllImport("__Internal")]
	static extern string _getIMSI ();
}