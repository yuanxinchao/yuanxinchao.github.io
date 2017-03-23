using UnityEngine;
using System.Collections;
using System;

public class UmengShare
{
	static Platform[] platforms = { Platform.QQ,Platform.QZONE,Platform.SINA,Platform.WEIXIN,Platform.WEIXIN_CIRCLE};
	static Action shareSuccessCallback = null;

	public static void InitUmengKey (params string[] key)
	{
		Debug.Log ("YXC" + "  Umeng Call InitKey");
		UshareCallAct.InitUmengKey (key);
	}

	void onShareback (string result)
	{
		Debug.Log ("YXC" + "  分享结果  " + result);
	}

	static void CancelCallback ()
	{
		Debug.Log ("YXC" + "  用户取消分享  ");
	}

	static void SuccessCallback (Platform platform, int stCode, string errorMsg)
	{

		if (stCode == UMSocial.SUCCESS) {
			if (shareSuccessCallback != null) {
				shareSuccessCallback ();
				shareSuccessCallback = null;
			}
            
			Debug.Log ("YXC" + "  分享成功  ");
		} else {

			Debug.Log ("YXC" + "  分享失败错误信息为  " + errorMsg);
		}
	}

	public static void  OpenSharePlateform (string content, string imaPath, string title, string url, Action ac = null)
	{
		Debug.Log ("YXC" + "  打开分享面板  ");
		if (ac != null) {
			shareSuccessCallback = ac;
		}
		UMSocial.setDismissDelegate (CancelCallback);
		UMSocial.OpenShareWithImagePath (platforms, content, imaPath, title, url, SuccessCallback);
	}
	public static void  OpenShareImageOnly (string imaPath, Action ac = null)
	{
		Debug.Log ("YXC" + "  打开分享面板  ");
		if (ac != null) {
			shareSuccessCallback = ac;
		}
		UMSocial.setDismissDelegate (CancelCallback);
		UMSocial.openShareWithImageOnly (platforms, imaPath, SuccessCallback);
	}

	public static void DirectShare (Platform platform, string content, string imagePath, string title, string url, Action ac = null)
	{
		Debug.Log ("YXC" + "  直接分享到  " + platform.ToString ());
		#if UNITY_EDITOR
		return;
		#endif
		
		if (ac != null) {
			shareSuccessCallback = ac;
		}
		UMSocial.setDismissDelegate (CancelCallback);
		UMSocial.DirectShare (platform, content, imagePath, title, url, SuccessCallback);
	}

	public static bool IsInstall (Platform platform)
	{
		bool isInstall = UshareCallAct.IsInstall (platform);
		Debug.Log (string.Format ("YXC" + "  平台 " + platform.ToString () + "{0}安装", isInstall ? "已" : "未"));
		return isInstall;
	}
}
