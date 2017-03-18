package com.tj;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class RateApp{
	static Activity mActivity;
	static {
		mActivity = UnityPlayer.currentActivity;
		checkActivity();
	}
	public static void ShowNote(String title,String note,String confirm,String back){
		new  AlertDialog.Builder(mActivity)  
		.setTitle(title)  
		.setMessage(note)  
		.setPositiveButton(confirm ,new DialogInterface.OnClickListener(){

			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				Log.i("RateApp","YXC   用户点击了确定按钮");
				GoRateApp();
			}
		})                  
		.setNegativeButton(back ,new DialogInterface.OnClickListener(){

			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				Log.i("RateApp","YXC   用户点击了取消按钮");
				
			}
		}) 
		.show();
	}
	
	public static void GoRateApp(){
		Log.i("RateApp","YXC   调用去评价app appName="+mActivity.getPackageName());
		String mAddress = "market://details?id="+mActivity.getPackageName();   
		Intent marketIntent = new Intent("android.intent.action.VIEW");
		marketIntent.setData(Uri.parse(mAddress ));
		mActivity.startActivity(marketIntent);
	}
	private static void checkActivity() {
		assert mActivity != null : "在RateApp类中, mActivity为null.";
	}
}
