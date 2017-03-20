package com.tj;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.Gravity;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.FrameLayout;

import com.alimama.listener.MMUInterstitialListener;
import com.alimama.listener.MMUListener;
import com.alimama.mobile.sdk.MMUSDK;
import com.alimama.mobile.sdk.config.BannerController;
import com.alimama.mobile.sdk.config.BannerProperties;
import com.alimama.mobile.sdk.config.InsertController;
import com.alimama.mobile.sdk.config.InsertProperties;
import com.alimama.mobile.sdk.config.MMUSDKFactory;
import com.alimama.mobile.sdk.config.MmuProperties;
import com.alimama.mobile.sdk.config.system.MMLog;
import com.unity3d.player.UnityPlayer;

public class Alimama {
	
	static Activity mActivity;
	static {
		mActivity = UnityPlayer.currentActivity;
		checkActivity();
	}
	
	private static void runInUI(Runnable runnable) {
		mActivity.runOnUiThread(runnable);
	}
	
	
	
	
	//alimama---------------------//
	
		static String alimm_APPID = null;
		static String alimm_BannerPosID = null;
		static String alimm_InsertPosID = null;
		
		private InsertProperties propertiesInsert;
	    private InsertController<?> mControllerInsert;
		private MMUSDK mmuSDKInsert;
		private BannerProperties properties;
	    private BannerController<?> mController;
		private MMUSDK mmuSDK;
		private ViewGroup nat;
		 MMUInterstitialListener adsMogoListenerInsert = new MMUInterstitialListener() {


		        @Override
		        public void onShowInterstitialScreen() {
		            // TODO Auto-generated method stub
		            MMLog.i("插屏广告展示在屏幕上");
		            Log.i("Alimama","YXC Alimama插屏广告展示在屏幕上 ");
		        }

		        @Override
		        public boolean onInterstitialStaleDated() {
		            // TODO Auto-generated method stub
		            return false;//返回 true：表示广告过期后不立即去请求新的广告
		            // false:表示过期后立即去请求新的广告
		        }

		        @Override
		        public void onInterstitialCloseAd(boolean isAutomaticClosing) {
		            // TODO Auto-generated method stub
		            MMLog.i("插屏广告被关闭");
		            Log.i("Alimama","YXC Alimama插屏广告被关闭 ");
		            UnityPlayer.UnitySendMessage("TJSDK", "FinishVideoCallBack", "playsuccess");
		        }

		        @Override
		        public boolean onInterstitialClickCloseButton() {
		            // TODO Auto-generated method stub
		            return false;//返回false:表示不拦截用户点击关闭按钮事件，
		            //true:表示拦截即关闭按钮不生效
		        }

		        @Override
		        public void onInterstitialClickAd() {
		            // TODO Auto-generated method stub
		            MMLog.i("插屏广告被点击");
		            Log.i("Alimama","YXC Alimama 插屏广告被点击 ");
		        }

		        @Override
		        public void onInitFinish() {
		            // TODO Auto-generated method stub
		            MMLog.i("插屏广告初始化完成");
		            Log.i("Alimama","YXC  Alimama插屏初始化完成 ");
		        }

				@Override
				public void onInterstitialReadyed() {
					// TODO Auto-generated method stub
					 MMLog.i("插屏广告预加载成功");
					 Log.i("Alimama","YXC  Alimama  加载好了插屏 ");
					 UnityPlayer.UnitySendMessage("TJSDK", "ReceiveVideo", "videoloaded");
				}

				@Override
				public void onInterstitialFailed() {
					// TODO Auto-generated method stub
					MMLog.i("插屏广告请求失败");
					 Log.i("Alimama","YXC  Alimama  插屏广告请求失败 ");
					UnityPlayer.UnitySendMessage("TJSDK", "ReceiveVideo", "videofail");
				}

		    };
		  //注意：请在Activity成员变量保存，使用匿名内部类可能导致回收
	    MMUListener adsMogoListener = new MMUListener() {

	        @Override
	        public void onRequestAd() {
	            // TODO Auto-generated method stub
	            MMLog.i("横幅广告开始请求");   
	            Log.i("Alimama","YXC  Alimama  横幅广告开始请求 ");
	        }

	        @Override
	        public void onReceiveAd(ViewGroup adView) {
	            // TODO Auto-generated method stub
	            MMLog.i("横幅广告请求成功");
	            Log.i("Alimama","YXC  Alimama  横幅广告请求成功 ");
	            UnityPlayer.UnitySendMessage("TJSDK", "ReceiveBanner", "bannerloaded");
	        }

	        @Override
	        public void onInitFinish() {
	            // TODO Auto-generated method stub
	            MMLog.i("横幅广告初始化完成");
	            Log.i("Alimama","YXC  Alimama  横幅广告初始化完成");
	        }

	        @Override
	        public void onFailedReceiveAd() {
	            // TODO Auto-generated method stub
	            MMLog.i("横幅广告请求失败");
	            Log.i("Alimama","YXC  Alimama  横幅广告请求失败");
	        }

	        @Override
	        public boolean onCloseAd() {
	            // TODO Auto-generated method stub
	            return false;//返回false:表示不拦截用户点击关闭按钮事件，
	            // true:表示拦截即关闭按钮不生效
	        }

	        @Override
	        public void onClickAd() {
	            // TODO Auto-generated method stub
	            MMLog.i("横幅广告被点击");
	            Log.i("Alimama","YXC  Alimama  横幅广告被点击");
	        }
	    };
	    

	    public void initYYB(final String bannerid,final String interid){
	        Log.i("Alimama","YXC  Alimama 注册activity");
		    //---------阿里妈妈--------//
			MMUSDKFactory.getMMUSDK().init(mActivity.getApplication());
		    MMUSDKFactory.registerAcvitity(MainActivity.class);
		    Log.i("Alimama","YXC  Alimama 完成注册activity");
	    	
	    	
		    runInUI(new Runnable(){

				@Override
				public void run() {
					 setBannerLayout();
					
				        Log.i("Alimama","YXC  Alimama 初始化应用宝");
			    	 String slotId = bannerid; //修改为自己的slotid
			         setupMMU(nat, slotId);
			         
				        Log.i("Alimama","YXC  Alimama 完成初始化应用宝");
			         
			         final String slotIdInter = interid;//注意：该广告位只做测试使用，请勿集成到发布版app中
			         setupMMUInter(slotIdInter);
				}
			});
	    }

	    public void ShowBanner(final boolean bo){
	        Log.i("Alimama","YXC  Alimama 调用ShowBanner");
	        runInUI(new Runnable(){

				@Override
				public void run() {
					if(bo){
				        Log.i("Alimama","YXC  Alimama 展示banner");
			    		mController.show();	
					}
			    	else
			    		mController.close();
				}
			});
	    }
	    public void ShowInterstitial(){
	    	
	    	runInUI(new Runnable(){

				@Override
				public void run() {
					 if(mControllerInsert != null){
						 mControllerInsert.show(false);
					        Log.i("Alimama","YXC  Alimama 展示插屏");
					 }
				}
			});
	    	
	    }
	    public void loadInterstitial(){
	        Log.i("Alimama","YXC  Alimama 加载插屏");
	    	mControllerInsert.loadAd();
	    }
	    
	    private void setupMMUInter(String slotIdInter){
	    mmuSDKInsert = MMUSDKFactory.getMMUSDK();
	    mmuSDKInsert.init(mActivity.getApplication());//初始化SDK,该方法必须保证在集成代码前调用，可移到程序入口处调用
	    propertiesInsert = new InsertProperties(mActivity,slotIdInter);
	    propertiesInsert.setShowMask(true); //设置弹出层背景是否半透明
	    propertiesInsert.setCanThrough(false); //设置弹出层背景是否能被点穿,false表示不能被点穿，默认值false
	    propertiesInsert.setManualRefresh(false);//设置手动刷新模式，false表示不开启手动刷新模式，默认值false
	    propertiesInsert.setAcct(MmuProperties.ACCT.VIEW);
	    propertiesInsert.setMMUInterstitialListener(adsMogoListenerInsert);
	    
	    mmuSDKInsert.attach(propertiesInsert); //初始化插屏
	    
	    mControllerInsert =  propertiesInsert.getController(); //获取插屏控制器
	    
	    }
	    private void setupMMU(ViewGroup nat, String slotId) {
	        mmuSDK = MMUSDKFactory.getMMUSDK();
	        Log.i("Alimama","YXC  Alimama setupMMU1");
	        mmuSDK.init(mActivity.getApplication());//初始化SDK,该方法必须保证在集成代码前调用，可移到程序入口处调用
	        Log.i("Alimama","YXC  Alimama setupMMU2");
	        properties = new BannerProperties(mActivity,slotId, nat);
	        Log.i("Alimama","YXC  Alimama setupMMU3");
	        properties.setStretch(true); //设置横幅广告宽度是否自适应屏幕
	        Log.i("Alimama","YXC  Alimama setupMMU4");
	        properties.setManualRefresh(false);//设置为手动刷新模式，默认为false，关闭手动刷新模式
	        Log.i("Alimama","YXC  Alimama setupMMU5");
	        properties.setMMUListener(adsMogoListener);
	        Log.i("Alimama","YXC  Alimama setupMMU6");
	        properties.setAcct(MmuProperties.ACCT.VIEW);
	        Log.i("Alimama","YXC  Alimama setupMMU7");
	        mmuSDK.attach(properties); //开始加载广告
	        Log.i("Alimama","YXC  Alimama setupMMU8");
	        mController =  properties.getController(); //获取横幅控制器
	        Log.i("Alimama","YXC  Alimama setupMMU9");
	        mController.close();
	    }
	    public void setBannerLayout(){
	        Log.i("Alimama","YXC  Alimama 创建布局来承载广告条");
			// 创建布局来承载广告条
	    	nat = new FrameLayout(mActivity);
			
			 WindowManager mWindowManager = (WindowManager) mActivity.getSystemService(Context.WINDOW_SERVICE);
	         WindowManager.LayoutParams mWmParams = new WindowManager.LayoutParams();
	         mWmParams.flags = WindowManager.LayoutParams.FLAG_NOT_TOUCH_MODAL| WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE;
	         //非常重要，决定广告条不会被拉伸
	         mWmParams.height = WindowManager.LayoutParams.WRAP_CONTENT;
	         //非常重要，决定广告条不会被拉伸
	         mWmParams.width = WindowManager.LayoutParams.MATCH_PARENT;
	         mWmParams.alpha = 1.0F;
	         mWmParams.format = 1;
	         mWmParams.gravity = Gravity.BOTTOM|Gravity.CENTER;
	         mWindowManager.addView(nat, mWmParams);
//			layout = new FrameLayout(MainActivity.this);
//			FrameLayout.LayoutParams mWmParams = new FrameLayout.LayoutParams(LayoutParams.MATCH_PARENT,LayoutParams.WRAP_CONTENT,Gravity.BOTTOM|Gravity.CENTER);
//			this.addContentView(layout, mWmParams);
		}
	    
		private static void checkActivity() {
			assert mActivity != null : "在Alimama类中, mActivity为null.";
		}
}
