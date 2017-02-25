package com.tj;

import android.os.Bundle;
import android.util.Log;

import com.qq.e.ads.interstitial.AbstractInterstitialADListener;
import com.qq.e.ads.interstitial.InterstitialAD;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainAdsActivity extends UnityPlayerActivity {

	InterstitialAD iad;
	String interKey;
	String gdtAppid;
	boolean IsInterReady = false;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		Log.i("AD_DEMO","onADCreate");
	}

	public void Init(String gdtAppid, String interKey) {
		Log.i("AD_DEMO","onADInitWith"+interKey+ gdtAppid);
		this.interKey = interKey;
		this.gdtAppid =gdtAppid;
		getIAD().setADListener(listenInterstitial);
	}
	AbstractInterstitialADListener listenInterstitial=new AbstractInterstitialADListener() {

		@Override
		public void onNoAD(int arg0) {
			Log.i("AD_DEMO", "onADloadFail:" + arg0);
			IsInterReady = false;
			UnityPlayer.UnitySendMessage("TjSdk","onGdtEventReceived","LOADREWARDEDFAILED");  
		}

		@Override
		public void onADReceive() {
			Log.i("AD_DEMO", "onADReceive");
			IsInterReady = true;
		}
		public void onADClosed() {
			Log.i("AD_DEMO", "onADClose");
			UnityPlayer.UnitySendMessage("TjSdk","onGdtEventReceived","HIDDENREWARDED");  
		}
	};
	public void GetInterstitial() {
		Log.i("AD_DEMO", "onADloadAds");

		this.runOnUiThread(new Runnable() 
		{
			@Override
			public void run() {
				iad.loadAD();
			}
		});
	}

	public boolean IsInterstitialAvailable() {
		Log.i("AD_DEMO", "onADIsLoad "+IsInterReady);
		return IsInterReady;
	}

	public void ShowInterstitial() {
		this.runOnUiThread(new Runnable() 
		{
			@Override
			public void run() {
				Log.i("AD_DEMO", "onADShowInterstitial");
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
			iad = new InterstitialAD(this, gdtAppid,
					interKey);
		}
		return iad;
	}
}
