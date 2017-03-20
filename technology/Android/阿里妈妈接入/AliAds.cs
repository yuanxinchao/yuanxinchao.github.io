using UnityEngine;
using System.Collections;
using System;

public class AliAds:BaseAds<AliAds>
{
    public static bool showBanner = false;

	
    public AliAds ()
    {
		
    }

    /// <summary>
    /// 第一个是bannerid，第二个是插屏Id
    /// </summary>
    /// <param name="key">Key.</param>
    public override void Init (params string[] key)
    {
        Debug.Log("YXC" + "  初始化阿里banner  key=" + key [0]);
        AliCallAct.Init(key);
    }

    #region  展示banner 视屏 插屏

    public override void PlayBanner (bool show)
    {

        Debug.Log("YXC" + "  展示banner  show=" + show);
        showBanner = show;
        AliCallAct.ShowBanner(show);
    }

    public override void ShowIncentVideo (Action ac = null)
    {

    }

    public override void ShowInterVideo (Action ac = null)
    {
    }

    public override void ShowInterstitial ()
    {
        AliCallAct.ShowInterstitial();
    }

    public override void ShowRewardedInterstitial ()
    {
    }

    #endregion

    public override void AdsCallback (string ev)
    {

    }

    #region 获取视频和插屏

    public override void GetIncentivizedVideo ()
    {
	
		
    }

    public override void GetInterstitial ()
    {

    }

    #endregion

    #region 判断视频和插屏是否加载好

    public override bool IsIncentivizedAvailable ()
    {	
        return false;
    }

    public override bool IsInterstitialAvailable ()
    {
        return false;
    }

    #endregion

}