using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;



//分享平台枚举

public enum Platform : int
{
	/// 新浪微博
	SINA = 0,
	/// 微信
	WEIXIN = 1,
	/// 微信朋友圈
	WEIXIN_CIRCLE = 2,
	/// QQ
	QQ = 3,
	/// QQ空间
	QZONE = 4,
	/// fb
	FACEBOOK =5,
	///
	TWITTER = 6
}
;


public class UMSocial
{
    
	//成功状态码 
	//用于 授权回调 和 分享回调 的是非成功的判断
	public const int SUCCESS = 200;

	//授权回调
	//注意 android 分享失败 没有 errorMsg
	public delegate void AuthDelegate (Platform platform,int stCode,Dictionary<string,string> message);

	//分享回调
	//注意 android 分享失败 没有 errorMsg
	public delegate void ShareDelegate (Platform platform,int stCode,string errorMsg);

	public delegate void ShareBoardDismissDelegate ();
	//授权某社交平台
	//platform 平台名 callback 授权成功完成
	public static void Authorize (Platform platform, AuthDelegate callback=null)
	{
        
      
            

#if UNITY_ANDROID
		try {

			SetPlatforms (new Platform[] { platform });
			Run (delegate {
				var androidAuthListener = new AndroidAuthListener (callback);
				UMSocialSDK.CallStatic ("authorize", (int)platform, androidAuthListener);
			});
		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}

#elif UNITY_IPHONE
		authDelegate = callback;
		authorize ((int)platform, AuthCallback);
#endif
	}
	//解除某平台授权
	//platform 平台名 callback 解除完成回调
	public static void DeleteAuthorization (Platform platform, AuthDelegate callback=null)
	{
		if (string.IsNullOrEmpty (appKey)) {
			Debug.LogError ("请设置appkey");
			return;
		}
#if UNITY_ANDROID
		try {
			Run (delegate {
				var androidAuthListener = new AndroidAuthListener (callback);
				UMSocialSDK.CallStatic ("deleteAuthorization", (int)platform, androidAuthListener);
			}
			);
		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}

#elif UNITY_IPHONE
		authDelegate = callback;
		deleteAuthorization ((int)platform, AuthCallback);
#endif

	}
	public static void openShareWithImageOnly (Platform[] platforms, string imagePath, ShareDelegate callback=null)
	{
		
		
		if (platforms == null) {
			Debug.LogError ("平台不能为空");
			return;
		}
		//var _platforms = platforms ?? Enum.GetValues(typeof(Platform)) as Platform[];
		var length = platforms.Length;
		var platformsInt = new int[length];
		for (int i = 0; i < length; i++) {
			platformsInt [i] = (int)platforms [i];
			
		}
		
		#if UNITY_ANDROID
		try {
			
			Run (delegate {
				var androidShareListener = new AndroidShareListener (callback);
				UMSocialSDK.CallStatic ("openShareWithImageOnly", platformsInt, imagePath, androidShareListener);
			});
			
		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}
		
		#elif UNITY_IPHONE
		shareDelegate = callback;
		openShareWithImageOnlyiOS (platformsInt, length, imagePath, ShareCallback);
		#endif
	}
	//打开分享面板
	//platforms 需要分享的平台数组 ,text 分享的文字, imagePath 分享的照片文件路径, callback 分享成功或失败的回调
	//imagePath可以为url 但是url图片必须以http://或者https://开头
	//imagePath如果为本地文件 只支持 Application.persistentDataPath下的文件
	//例如 Application.persistentDataPath + "/" +"你的文件名"
	//如果想分享 Assets/Resouces的下的 icon.png 请前使用 Resources.Load() 和 FileStream 写到 Application.persistentDataPath下
	public static void OpenShareWithImagePath (Platform[] platforms, string text, string imagePath, string title, string targeturl, ShareDelegate callback=null)
	{
       

		if (platforms == null) {
			Debug.LogError ("平台不能为空");
			return;
		}
		//var _platforms = platforms ?? Enum.GetValues(typeof(Platform)) as Platform[];
		var length = platforms.Length;
		var platformsInt = new int[length];
		for (int i = 0; i < length; i++) {
			platformsInt [i] = (int)platforms [i];

		}

#if UNITY_ANDROID
		try {

			Run (delegate {
				var androidShareListener = new AndroidShareListener (callback);
				UMSocialSDK.CallStatic ("openShareWithImagePath", platformsInt, text, imagePath, title, targeturl, androidShareListener);
			});

		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}

#elif UNITY_IPHONE
		shareDelegate = callback;
		openShareWithImagePath (platformsInt, length, text, imagePath, title, targeturl, ShareCallback);

#elif UNITY_STANDALONE
		Application.OpenURL (targeturl);
#endif

	}
	public static void setDismissDelegate (ShareBoardDismissDelegate callback=null)
	{




		#if UNITY_ANDROID
		try {

			Run (delegate {
				var AndroidDismissListener = new AndroidDismissListener (callback);
				UMSocialSDK.CallStatic ("setDismissCallBack", AndroidDismissListener);
			});

		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}

		#elif UNITY_IPHONE
		dismissDelegate = callback;
		setDismissCallback (ShareBoardCallback);
		#endif

	}
	//直接分享到各个社交平台
	//platform 平台名，text 分享的文字，imagePath 分享的照片文件路径，callback 分享成功或失败的回调

	public static void DirectShare (Platform platform, string text, string imagePath, string title, string targeturl, ShareDelegate callback=null)
	{
        
 
#if UNITY_ANDROID
		try {

			// SetPlatforms(new Platform[] { platform });

			Run (delegate {
				var androidShareListener = new AndroidShareListener (callback);
				UMSocialSDK.CallStatic ("directShare", text, imagePath, title, targeturl, (int)platform, androidShareListener);
			});
		} catch (AndroidJavaException exp) {
			Debug.LogError (exp.Message);
		}


#elif UNITY_IPHONE
		shareDelegate = callback;
		directShare (text, imagePath, title, targeturl, (int)platform, ShareCallback);
#endif

	}




	//是否已经授权某平台
	//platform 平台名
	public static bool IsAuthorized (Platform platform)
	{
       

#if UNITY_ANDROID

		return UMSocialSDK.CallStatic<bool> ("isAuthorized", (int)platform);
#elif UNITY_IPHONE

		return isAuthorized ((int)platform);
#else 
		return false;
#endif
	}


   

 

   

 

















#if UNITY_ANDROID

	//设置SDK支持的平台
	public static void SetPlatforms (Platform[] platforms)
	{
		if (string.IsNullOrEmpty (appKey)) {
			Debug.LogError ("请设置appkey");
			return;
		}
		var length = platforms.Length;
		var platformsInt = new int[length];
		for (int i = 0; i < length; i++) {
			platformsInt [i] = (int)platforms [i];

		}

		Run (delegate {

			UMSocialSDK.CallStatic ("setPlatforms", platformsInt);
		});


	}



   


#endif













	//以下代码是内部实现
	//请勿修改

#if UNITY_ANDROID


	delegate void Action ();
	static void Run (Action action)
	{
		activity.Call ("runOnUiThread", new AndroidJavaRunnable (action));
	}


	class AndroidAuthListener : AndroidJavaProxy
	{


		public AndroidAuthListener (AuthDelegate Delegate)
				: base("com.umeng.socialsdk.AuthListener")
		{
			this.authDelegate = Delegate;
		}

		AuthDelegate authDelegate = null;
		public void onAuth (int platform, int stCode, string key, string value)
		{
			Debug.Log ("xxxxxx stCode=" + stCode);
			string[] keys = key.Split (',');
			string[] values = value.Split (',');
			Dictionary<string, string> dic = new Dictionary<string, string> ();
			//dic.Add (keys , values );
			for (int i = 0; i < keys.Length; i++) {
				dic.Add (keys [i], values [i]);
			}
			Debug.Log ("xxxxxx length=" + values.Length);

			authDelegate ((Platform)platform, stCode, dic);
		}
	}

	class AndroidShareListener : AndroidJavaProxy
	{

		ShareDelegate shareDelegate = null;
		public AndroidShareListener (ShareDelegate Delegate)
				: base("com.umeng.socialsdk.ShareListener")
		{
			this.shareDelegate = Delegate;
		}
		public void onShare (int platform, int stCode, string errorMsg)
		{
			shareDelegate ((Platform)platform, stCode, errorMsg);
		}
	}
	class AndroidDismissListener : AndroidJavaProxy
	{

		ShareBoardDismissDelegate shareDelegate = null;
		public AndroidDismissListener (ShareBoardDismissDelegate Delegate)
	: base("com.umeng.socialsdk.ShareBoardDismissListener")
		{
			this.shareDelegate = Delegate;
		}
		public void onDismiss ()
		{

			shareDelegate ();
		}
	}

    


	static AndroidJavaClass UMSocialSDK = new AndroidJavaClass ("com.umeng.socialsdk.UMSocialSDK");


	static AndroidJavaObject activity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject> ("currentActivity");


#endif

	static string appKey = null;

	static AuthDelegate authDelegate = null;

	static ShareDelegate shareDelegate = null;
	static ShareBoardDismissDelegate dismissDelegate = null;
//delegate void CallBack(IntPtr param); 
	public delegate void AuthHandler (Platform platform,int stCode,string key,string value);

	[AOT.MonoPInvokeCallback(typeof(AuthHandler))]
	static void AuthCallback (Platform platform, int stCode, string key, string value)
	{
		Debug.Log ("xxxxxx stCode=" + stCode);
		string[] keys = key.Split (',');
		string[] values = value.Split (',');
		Dictionary<string, string> dic = new Dictionary<string, string> ();
		//dic.Add (keys , values );
		for (int i = 0; i < keys.Length; i++) {
			dic.Add (keys [i], values [i]);
		}
		Debug.Log ("xxxxxx length=" + values.Length);

		if (authDelegate != null)
			authDelegate (platform, stCode, dic);
	}


	[AOT.MonoPInvokeCallback(typeof(ShareDelegate))]
	static void ShareCallback (Platform platform, int stCode, string errorMsg)
	{
		if (shareDelegate != null)
			shareDelegate (platform, stCode, errorMsg);
	}
	[AOT.MonoPInvokeCallback(typeof(ShareBoardDismissDelegate))]
	static void ShareBoardCallback ()
	{
		if (dismissDelegate != null)
			dismissDelegate ();
	}
	[DllImport("__Internal")]
	static extern void authorize (int platform, AuthHandler callback);

	[DllImport("__Internal")]
	static extern void deleteAuthorization (int platform, AuthHandler callback);

	[DllImport("__Internal")]
	static extern bool isAuthorized (int platform);

	[DllImport("__Internal")]
	static extern void openShareWithImagePath (int[] platform, int platformNum, string text, string imagePath, string title, string targeturl, ShareDelegate callback);

	[DllImport("__Internal")]
	static extern void directShare (string text, string imagePath, string title, string targeturl, int platform, ShareDelegate callback);

	[DllImport("__Internal")]
	static extern void setDismissCallback (ShareBoardDismissDelegate callback);

	[DllImport("__Internal")]
	static extern void openShareWithImageOnlyiOS (int[] platform, int platformNum, string imagePath, ShareDelegate callback);
  

	
}


