using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class OulaAds: BaseAds<OulaAds>
{
    public static bool showBanner = false;
    static Action finishtodo = null;
    static string placementId;

    public OulaAds ()
    {

    }

    public override void Init (params string[] key)
    {

        Debug.Log("YXC" + this.GetType() + " Init key" + string.Join("-", key));
        OulaAdsCallPlatform.Init(key [0]);
        placementId = key [0];
        GetIncentivizedVideo();
        GetInterstitial();
    }

    #region  展示banner 视屏 插屏

    public override void PlayBanner (bool show)
    {
        showBanner = show;
        if (show)
        {
            Debug.Log("YXC" + this.GetType() + "  展示banner");
            OulaAdsCallPlatform.ShowBanner(show);
        } else
        {
            Debug.Log("YXC" + this.GetType() + "  隐藏banner");
            OulaAdsCallPlatform.ShowBanner(show);
        }
        //      AdsYuMiBannerUnity.setAdsYuMiViewHidden (!show);
    }

    public override void ShowIncentVideo (Action ac = null)
    {
        Debug.Log("YXC" + this.GetType() + "展示激励视频");
        finishtodo = ac;
        ShowRewardedInterstitial();
    }

    public override void ShowInterVideo (Action ac = null)
    {
        Debug.Log("YXC" + this.GetType() + "展示插屏视频");
        finishtodo = ac;
        ShowInterstitial();
    }

    public override void ShowInterstitial ()
    {
        OulaAdsCallPlatform.ShowInterstitial(AdsCallback);
    }

    public override void ShowRewardedInterstitial ()
    {
        OulaAdsCallPlatform.ShowRewardVideo(placementId, AdsCallback);
    }

    #endregion



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
        } else if (ev.Contains("HIDDENREWARDED") || ev.Contains("HIDDENINTER"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            Debug.Log("YXC" + this.GetType() + "视频或插屏播放完成，现在取下次视频");
            if (finishtodo != null)
                finishtodo();

            if (!IsIncentivizedAvailable())
            {
                GetIncentivizedVideo();
            }
            if (!IsInterstitialAvailable())
            {
                GetInterstitial();
            }
        }
    }

    #region 获取视频和插屏

    public override void GetIncentivizedVideo ()
    {
        Debug.Log("YXC" + this.GetType() + "获取视频");
        OulaAdsCallPlatform.LoadIncent();
    }

    public override void GetInterstitial ()
    {
        Debug.Log("YXC" + this.GetType() + "获取插频");
        OulaAdsCallPlatform.LoadInterstitial();
    }

    #endregion

    #region 判断视频和插屏是否加载好

    public override bool IsIncentivizedAvailable ()
    {
        if (OulaAdsCallPlatform.IsInterstitialReady())
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
        if (OulaAdsCallPlatform.IsInterstitialReady())
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

   






}
