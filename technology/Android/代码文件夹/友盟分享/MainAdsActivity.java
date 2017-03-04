package com.tj;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import cn.m4399.operate.OperateCenterConfig;
import cn.m4399.operate.SingleOperateCenter;
import cn.m4399.recharge.RechargeOrder;

import com.qq.e.ads.interstitial.AbstractInterstitialADListener;
import com.qq.e.ads.interstitial.InterstitialAD;
import com.umeng.socialize.Config;
import com.umeng.socialize.PlatformConfig;
import com.umeng.socialize.UMShareAPI;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainAdsActivity extends UnityPlayerActivity{

	@Override
	protected void onCreate(Bundle arg0) {
		// TODO Auto-generated method stub
		super.onCreate(arg0);
		
	}
	//--------广电通---------//
	InterstitialAD iad;
	String interKey;
	String gdtAppid;
	boolean IsInterReady = false;

	

	public void Init(String gdtAppid, String interKey) {
		Log.i("AD_DEMO","YXC"+"  onADInitWith"+gdtAppid +"    "+ interKey);
		this.interKey = interKey;
		this.gdtAppid =gdtAppid;
		getIAD().setADListener(listenInterstitial);
	}
	AbstractInterstitialADListener listenInterstitial=new AbstractInterstitialADListener() {

		@Override
		public void onNoAD(int arg0) {
			Log.i("AD_DEMO", "YXC"+"  onADloadFail:" + arg0);
			IsInterReady = false;
			UnityPlayer.UnitySendMessage("TjSdk","onGdtEventReceived","LOADREWARDEDFAILED");  
		}

		@Override
		public void onADReceive() {
			Log.i("AD_DEMO", "YXC"+"  onADReceive");
			IsInterReady = true;
		}
		public void onADClosed() {
			Log.i("AD_DEMO", "YXC"+"  onADClose");
			UnityPlayer.UnitySendMessage("TjSdk","onGdtEventReceived","HIDDENREWARDED");  
		}
	};
	@SuppressLint("NewApi")
	public void GetInterstitial() {
		Log.i("AD_DEMO","YXC"+"  onADloadAds");

		this.runOnUiThread(new Runnable() 
		{
			@Override
			public void run() {
				iad.loadAD();
			}
		});
	}

	@SuppressLint("NewApi")
	public boolean IsInterstitialAvailable() {
		Log.i("AD_DEMO", "YXC"+"  onADIsLoad "+IsInterReady);
		return IsInterReady;
	}

	public void ShowInterstitial() {
		this.runOnUiThread(new Runnable() 
		{
			@Override
			public void run() {
				Log.i("AD_DEMO","YXC"+"  onADShowInterstitial");
				iad.show();

				IsInterReady = false;
			}
		});
	}

	public void closeAsPopup() {
		if (iad != null) {
			iad.closePopupWindow();
		}
	}

	private InterstitialAD getIAD() {
		if (iad == null) {
			iad = new InterstitialAD(this, gdtAppid,interKey);
		}
		return iad;
	}
	//----------------------//

	
//	友盟回调以及初始化key
    public void InitUmengKey(String QqKey,String QqSecret,String WeixinKey,String WeixinSecret,String SinaKey,String SinaSecret) {
    	Log.e("umeng","YXC   初始化Key");
	     Config.shareType = "u3d";
	     UMShareAPI.get(this);
	     Config.REDIRECT_URL = "http://sns.whalecloud.com/sina2/callback";
    	
        PlatformConfig.setSinaWeibo(SinaKey, SinaSecret);
        PlatformConfig.setQQZone(QqKey, QqSecret);
        PlatformConfig.setWeixin(WeixinKey,WeixinSecret);
	}
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	    // TODO Auto-generated method stub
	    super.onActivityResult(requestCode, resultCode, data);
	    UMShareAPI.get(this).onActivityResult(requestCode, resultCode, data);
	}
	
	

}
