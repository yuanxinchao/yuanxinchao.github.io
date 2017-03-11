package com.tj;

import java.util.Locale;

import android.content.Context;
import android.util.Log;
public class Location {
	
	 static Location instance;
	 static Context context;
	  public static Location Instance(Context ctx)
	  {
	    context = ctx;
	    if (instance == null) {
	      instance = new Location();
	    }
	    return instance;
	  }

	  public String GetLanguage()
	  {
		  String language =  Locale.getDefault().toString();
		  Log.i("get language","YXC GetLanguage"+language);
		  return language;
	  }
}
