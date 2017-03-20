package com.tj;

import android.app.Activity;
import android.util.Log;

import com.qq.e.ads.interstitial.AbstractInterstitialADListener;
import com.qq.e.ads.interstitial.InterstitialAD;
import com.unity3d.player.UnityPlayer;


public class GdtAds {
	static Activity mActivity;
	InterstitialAD iad;
	String interKey;
	String gdtAppid;
	boolean IsInterReady = false;
	static {
		mActivity = UnityPlayer.currentActivity;
		checkActivity();
	}
	
	private static void runInUI(Runnable runnable) {
		mActivity.runOnUiThread(runnable);
	}
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
	public void GetInterstitial() {
		Log.i("AD_DEMO","YXC"+"  onADloadAds");

		runInUI(new Runnable() 
		{
			@Override
			public void run() {
				iad.loadAD();
			}
		});
	}

	public boolean IsInterstitialAvailable() {
		Log.i("AD_DEMO", "YXC"+"  onADIsLoad "+IsInterReady);
		return IsInterReady;
	}

	public void ShowInterstitial() {
		runInUI(new Runnable() 
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
			iad = new InterstitialAD(mActivity, gdtAppid,interKey);
		}
		return iad;
	}
	public static void updateOwnerActivity(Activity activity) {
		mActivity = activity;
	}

	private static void checkActivity() {
		assert mActivity != null : "在GdtAds类中, mActivity为null.";

	}
}
