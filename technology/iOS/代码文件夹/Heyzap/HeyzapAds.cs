using UnityEngine;
using System.Collections;
using System;
using Heyzap;
using System.Runtime.InteropServices;

public class HeyZapAds: BaseAds<HeyZapAds>
{
    public static bool showBanner = false;
    static Action finishtodo = null;

    public HeyZapAds ()
    {

    }

    public override void Init (params string[] key)
    {

        Debug.Log("YXC" + this.GetType() + " Init key" + string.Join("-", key));
        HeyzapAds.Start(key [0], HeyzapAds.FLAG_NO_OPTIONS);
        HZIncentivizedAd.SetDisplayListener(ReceiveHZ);
        HZInterstitialAd.SetDisplayListener(ReceiveHZ);
        HeyzapAds.ShowMediationTestSuite();
        HZBannerAd.ShowWithOptions(null);
        HZBannerAd.SetDisplayListener(ReceiveHZ);
        GetIncentivizedVideo();

    }

    #region  展示banner 视屏 插屏

    public override void PlayBanner (bool show)
    {
        showBanner = show;
        if (show)
        {
            HZBannerAd.ShowWithOptions(null);
        } else
        {
            HZBannerAd.Hide();
        }
        //      AdsYuMiBannerUnity.setAdsYuMiViewHidden (!show);
    }

    public override void ShowIncentVideo (Action ac = null)
    {

        if (ac != null)
        {
            finishtodo = ac;
        } else
        {
            finishtodo = null;
        }
        ShowRewardedInterstitial();
    }

    public override void ShowInterVideo (Action ac = null)
    {

        if (ac != null)
        {
            finishtodo = ac;
        } else
        {
            finishtodo = null;
        }
        ShowInterstitial();
    }

    public override void ShowInterstitial ()
    {
        Debug.Log("YXC" + this.GetType() + "展示插屏视频");
        HZInterstitialAd.Show();
    }

    public override void ShowRewardedInterstitial ()
    {
        Debug.Log("YXC" + this.GetType() + "展示激励视频");
        HZIncentivizedAd.Show();
    }

    #endregion

    public void ReceiveHZ (string state , string tag)
    {
        Debug.Log("YXC" + this.GetType() + "ReceiveHZ");
        if (state.Equals("incentivized_result_complete"))
        {
            AdsCallback("HIDDENREWARDED");
        }
        if (state.Equals("hide"))
        {
            AdsCallback("HIDDENREWARDED");
        }
        if (state.Equals("loaded"))
        {
            AdsCallback("ReceiveBanner");
        }
    }


    public override void AdsCallback (string ev)
    {
        Debug.Log("YXC" + this.GetType() + "  回调信息  " + ev);
        if (ev.Contains("REWARDAPPROVEDINFO"))
        {
            Debug.Log("YXC" + this.GetType() + " 视频开始播放  " + ev);
        } else if (ev.Contains("LOADEDREWARDED"))
        {
            // A rewarded video was successfully loaded.
            Debug.Log("YXC" + this.GetType() + "load reward video success");
        } else if (ev.Contains("LOADEDINTER"))
        {
            Debug.Log("YXC" + this.GetType() + "load inter video success");
        } else if (ev.Contains("LOADREWARDEDFAILED"))
        {
            // A rewarded video failed to load.
            Debug.Log("YXC" + this.GetType() + "load reward video fail go to load again");
            GetIncentivizedVideo();
        } else if (ev.Contains("LOADINTERFAILED"))
        {
            Debug.Log("YXC" + this.GetType() + "load inter video fail go to load again");
            GetInterstitial();
        } else if (ev.Contains("HIDDENREWARDED"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            Debug.Log("YXC" + this.GetType() + "视频播放完成，现在取下次视频");
            if (finishtodo != null)
                finishtodo();

            GetIncentivizedVideo();
        } else if (ev.Contains("HIDDENINTER"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            Debug.Log("YXC" + this.GetType() + "视频播放完成，现在取下次视频");
            if (finishtodo != null)
                finishtodo();

            GetInterstitial();
        }
    }

    #region 获取视频和插屏

    public override void GetIncentivizedVideo ()
    {
        Debug.Log("YXC" + this.GetType() + "获取视频");
        HZIncentivizedAd.Fetch();

    }

    public override void GetInterstitial ()
    {
        Debug.Log("YXC" + this.GetType() + "获取插频");
        HZInterstitialAd.Fetch();
    }

    #endregion

    #region 判断视频和插屏是否加载好

    public override bool IsIncentivizedAvailable ()
    {

        if (HZIncentivizedAd.IsAvailable())
        {
            Debug.Log("YXC" + this.GetType() + " 视频加载好啦");
            return true;
        } else
        {
            Debug.Log("YXC" + this.GetType() + " 视频没加载好");
            GetIncentivizedVideo();
        }

        return false;
    }

    public override bool IsInterstitialAvailable ()
    {
        if (HZInterstitialAd.IsAvailable())
        {
            Debug.Log("YXC" + this.GetType() + " 插频加载好啦");
            return true;
        } else
        {
            Debug.Log("YXC" + this.GetType() + " 插频没加载好");
            GetInterstitial();
            return false;
        }
    }

    #endregion

    public void GetResource (GetResourceCallback getResourceCB = null)
    {
        #if UNITY_IPHONE
        if (getResourceCB != null)
        {
            getCallback = getResourceCB;
        }
        GetiOSAdsResource(GetResCallback);
        #endif
        #if UNITY_ANDROID


        #endif
    }

    [DllImport("__Internal")]
    static extern void GetiOSAdsResource (GetResourceCallback getCB);

    [AOT.MonoPInvokeCallback(typeof(GetResourceCallback))]
    static void GetResCallback (bool bo , string iconUrl , string bgPicUrl , string title , string content)
    {
        if (getCallback != null)
        {
            getCallback(bo, iconUrl, bgPicUrl, title, content);
        }
    }


    public delegate void GetResourceCallback(bool bo,string iconUrl,string bgPicUrl,string title,string content);

    static GetResourceCallback getCallback;


    public void ClickAds ()
    {

        ClickTheAds();
    }

    [DllImport("__Internal")]
    static extern void ClickTheAds ();

}
