package com.tj;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class ExitPrompt{
	static Activity mActivity;
	static {
		mActivity = UnityPlayer.currentActivity;
		checkActivity();
	}
	public static void ExitPromptGame(String title,String note,String confirm,String back){
		new  AlertDialog.Builder(mActivity)  
		.setTitle(title)  
		.setMessage(note)  
		.setPositiveButton(confirm ,new DialogInterface.OnClickListener(){

			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				Log.i("ExitPrompt","YXC   用户点击了确定退出按钮");
				exitGameProcess(mActivity);
			}
		})                  
		.setNegativeButton(back ,new DialogInterface.OnClickListener(){

			@Override
			public void onClick(DialogInterface dialog, int which) {
				// TODO Auto-generated method stub
				Log.i("ExitPrompt","YXC   用户点击了取消退出按钮");
				
			}
		}) 
		.show();
	}
	public static void exitGameProcess(Activity paramActivity)
	  {
	    try
	    {
	      if (!paramActivity.isFinishing()) {
	        paramActivity.finish();
	      }
	      System.exit(0);
	      return;
	    }
	    catch (Exception localException) {}
	  }
	
	
	
	private static void checkActivity() {
		assert mActivity != null : "在ExitPrompt类中, mActivity为null.";
	}
}
