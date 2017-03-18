using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class JieSuanContext : BaseContext
{
    public JieSuanContext () : base(UIType.JieSuanMenu)
    {

    }
}

public class JieSuanView : AnimateView
{
    public DieScoreAnim dieScoreAnim;
    private int dieNum = 0;

    public override void OnEnter (BaseContext context)
    {
        dieScoreAnim.score.text = "0";
        base.OnEnter(context);
    }

    public override void OnExit (BaseContext context)
    { 
        base.OnExit(context);
    }

    public override void OnPause (BaseContext context)
    {

        base.OnPause(context);
    }

    public override void OnResume (BaseContext context)
    {
        base.OnResume(context);
    }

    public void PlayAgain ()
    {
        Singleton<ContextManager>.instance.Pop();
        Singleton<ContextManager>.instance.Push(new MainMenuContext());
        GameManage.instance.Replay();
        if (dieNum++ >= 1)
        {
            dieNum = 0;
            TjSdk.ShowInterVideo();
        }
    }

    public void PlayScoreAnim ()
    {
        dieScoreAnim.SetDieScore();
    }

    public void PlayGameOverMusic ()
    {
        MusicCtrl.instance.PlayShort((int)Mic.gameover);
    }

    public void ShareScore ()
    {

        StartCoroutine(ShareImage());
       
    }

    public void GoRate ()
    {

        #if UNITY_ANDROID
        Debug.Log("YXC" + "  Android端调用评价  ");
        var rateApp = new AndroidJavaClass("com.tj.RateApp");
        rateApp.CallStatic("ShowNote", Singleton<Localization>.instance.GetText("1"), Singleton<Localization>.instance.GetText("28"), Singleton<Localization>.instance.GetText("30"), Singleton<Localization>.instance.GetText("31"));
        #endif

        #if UNITY_IPHONE
        IOSFunction.RateApp(Myparameters.AppStoreId,
        (bool bo) =>
        {
        HasRate = bo;
        });
        #endif

    }

    IEnumerator ShareImage ()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;

        int height = Screen.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);

        byte[] imagebytes = tex.EncodeToPNG();//转化为png图

        tex.Compress(false);//对屏幕缓存进行压缩

//        image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）

        File.WriteAllBytes(Application.persistentDataPath + "/screencapture.png", imagebytes);//存储png图

        string imaPath = Application.persistentDataPath + "/screencapture.png";
        Debug.Log("YXC" + "  截图分享  imaPath=" + imaPath);
        UmengShare.OpenSharePlateform(Myparameters.ShareContent, imaPath, Myparameters.Title, Myparameters.Url);
    }
}
