using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class OulaAds: BaseAds<OulaAds>
{
    public static bool showBanner = false;
    static Action<bool> finishtodo = null;
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

    public override void ShowIncentVideo (Action<bool> ac = null)
    {
        Debug.Log("YXC" + this.GetType() + "展示激励视频");
        finishtodo = ac;
        ShowRewardedInterstitial();
    }

    public override void ShowInterVideo (Action<bool> ac = null)
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

    public void AdsCallbackTemp (string param)
    {
        string[] datas = param.Split('|');
        switch (datas [0])
        {
            case "Banner":
                switch (datas [1])
                {
                    case "onADReceive":
                        Debug.Log("接收到Banner广告");
                        AdsCallback("LOADEDBANNER");
                        break;
                    case "onNoAD":
                        Debug.Log("无Banner广告");
                        break;
                    case "onADClick":
                        Debug.Log("Banner广告点击");
                        AdsCallback("BANNERCLICK");
                        break;
                    default:
                        break;
                }
                break;
            case "Insert":
                switch (datas [1])
                {
                    case "onADReceive":
                        Debug.Log("已加载插屏广告");
                        AdsCallback("LOADEDINTER");
                        break;
                    case "onADShow":
                        Debug.Log("已展示插屏广告");
                        AdsCallback("HIDDENINTER");
                        break;
                    case "onNoAD":
                        Debug.Log("无插屏广告");
                        AdsCallback("LOADINTERFAILED");
                        break;
                    case "onADReady":
                        Debug.Log("插屏广告Ready");
                        if (isInterAvaiableAc != null)
                        {
                            isInterAvaiableAc(true);
                        }
                        break;
                    case "onADNotReady":
                        Debug.Log("onADInsertNotReady");
                        if (isInterAvaiableAc != null)
                        {
                            isInterAvaiableAc(false);
                            GetInterstitial();
                        }
                        break;
                    default:
                        break;
                }
                break;
            case "Video":
                switch (datas [1])
                {
                    case "onADReward":
                        Debug.Log("给奖励");
                        AdsCallback("GiveReward");
                        break;
                    case "onADNoReward":
                        Debug.Log("不给奖励");
                        AdsCallback("NoReward");
                        break;
                    case "onADOpen":
                        Debug.Log("打开视频广告");
                        break;
                    case "onADClose":
                        Debug.Log("关闭视频广告");
                        break;
                    case "onADReady":
                        Debug.Log("视频广告Ready");
                        if (isIncentAvailableCb != null)
                        {
                            isIncentAvailableCb(true);
                        }
                        break;
                    case "onADNotReady":
                        Debug.Log("视频广告NotReady");
                        if (isIncentAvailableCb != null)
                        {
                            isIncentAvailableCb(false);
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public override void AdsCallback (string ev)
    {
        Debug.Log("YXC" + this.GetType() + "  回调信息  " + ev);

        switch (ev)
        {
            case "LOADEDBANNER":
                Debug.Log("YXC" + this.GetType() + "load banner video success");
                PlayBanner(showBanner);
                break;
            case "BANNERCLICK":
                Debug.Log("YXC" + this.GetType() + "  BANNERCLICK");
                AdsManager.EffectiveBack = false;//标记因视频进入后台无效
                OulaCallPlatform.LogWithOnlyKey(GlobalVar.BANNER_ADV);
                break;
            case "LOADEDINTER":
                Debug.Log("YXC" + this.GetType() + "load inter video success");
                break;
            case "LOADINTERFAILED":
                Debug.Log("YXC" + this.GetType() + "load inter video fail go to load again");
                GetInterstitial();
                break;
            case "GiveReward":
                if (finishtodo != null)
                    finishtodo(true);
                break;
            case "NoReward":
                if (finishtodo != null)
                    finishtodo(false);
                break;
            case "HIDDENREWARDED":

                break;
            case "HIDDENINTER":
                Debug.Log("YXC" + this.GetType() + "插屏播放完成，现在取下次视频");
                if (finishtodo != null)
                    finishtodo(true);
                #if oula
                IsInterstitialAvailableCb((bo) =>
                {
                    if (!bo)
                    {
                        GetInterstitial();
                    }
                });
                #else
                if (!IsInterstitialAvailable())
                {
                GetInterstitial();
                }
                #endif
                break;
            default:
                break;
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
        if (OulaAdsCallPlatform.IsRewardVideoReady())
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

    Action<bool> isInterAvaiableAc;

    public void IsInterstitialAvailableCb (Action<bool> ac)
    {
        isInterAvaiableAc = ac;
        OulaAdsCallPlatform.IsInterstitialReadyCb();
    }



    Action<bool> isIncentAvailableCb;

    public void IsIncentAvailableCb (Action<bool> ac)
    {
        isIncentAvailableCb = ac;
        OulaAdsCallPlatform.IsIncentAvailableCb();
    }

    #endregion

   






}
