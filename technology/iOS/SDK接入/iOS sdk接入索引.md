##  iOS sdk接入索引  
###  [检查项]
#####  1.所有的SDK接入后都需要在other link flag 加入[-ObjC](../Xcode工程配置/Unity Build xcode工程注意点.md) 标志
#####  2.所有的SDK接入后都需要添加对应的framework
##### 3.添加对应的info.plist需要的东西
###  [可选项]
#####  1.如打包失败可尝试将BuildSetting Search Path 里含有的引号去掉
#####  2.接入applovin时需要compile source里applovin对应文件添加在 [-fno-objc-arc](../Xcode工程配置/Unity Build xcode工程注意点.md) 标志  
###  [接入]
* <a href="#友盟分享">友盟分享</a>  
* <a href ="#友盟统计">友盟统计</a>  
* <a href ="#阿里妈妈">阿里妈妈</a>  
* <a href ="#Applovin">Applovin</a>  
* <a href ="#Heyzap">Heyzap</a>  
* <a href ="#Inmobi">Inmobi</a>  
* <a href ="#hola">Hola欧拉合作</a>  
* <a href ="#GameCenter">GameCenter</a>  
* <a href ="#iOS内购">iOS内购</a>  
* <a href ="#本地推送">本地推送</a>  
* <a href ="#评价">评价</a>  
* <a href ="#获取设备信息">获取设备信息</a>  


### <a id="友盟分享">友盟分享</a> 
[详细接入文档](Integration manual/SDK_Um_Share.md)  
##### info.plist
	<key>LSApplicationQueriesSchemes</key>
	<array>
	<!-- 微信 URL Scheme 白名单-->
	<string>wechat</string>
	<string>weixin</string>
	<!-- 新浪微博 URL Scheme 白名单-->
	<string>sinaweibohd</string>
	<string>sinaweibo</string>
	<string>sinaweibosso</string>
	<string>weibosdk</string>
	<string>weibosdk2.5</string>
	<!-- QQ、Qzone URL Scheme 白名单-->
	<string>mqqapi</string>
	<string>mqq</string>
	<string>mqqOpensdkSSoLogin</string>
	<string>mqqconnect</string>
	<string>mqqopensdkdataline</string>
	<string>mqqopensdkgrouptribeshare</string>
	<string>mqqopensdkfriend</string>
	<string>mqqopensdkapi</string>
	<string>mqqopensdkapiV2</string>
	<string>mqqopensdkapiV3</string>
	<string>mqqopensdkapiV4</string>
	<string>mqzoneopensdk</string>
	<string>wtloginmqq</string>
	<string>wtloginmqq2</string>
	<string>mqqwpa</string>
	<string>mqzone</string>
	<string>mqzonev2</string>
	<string>mqzoneshare</string>
	<string>wtloginqzone</string>
	<string>mqzonewx</string>
	<string>mqzoneopensdkapiV2</string>
	<string>mqzoneopensdkapi19</string>
	<string>mqzoneopensdkapi</string>
	<string>mqqbrowser</string>
	</array>  
---
	<key>NSAppTransportSecurity</key>
	<dict>
	<key>NSAllowsArbitraryLoads</key>
	<true/>
	</dict>  
---
	<key>CFBundleURLTypes</key>
	  <array>
	      <dict>
	          <key>CFBundleTypeRole</key>
	          <string>Editor</string>
	          <key>CFBundleURLName</key>
	          <string>QQ分享</string>
	          <key>CFBundleURLSchemes</key>
	          <array>
	              <string>QQ41ecb0ca</string>
	          </array>
	      </dict>
	      <dict>
	          <key>CFBundleTypeRole</key>
	          <string>Editor</string>
	          <key>CFBundleURLName</key>
	          <string>QQ空间</string>
	          <key>CFBundleURLSchemes</key>
	          <array>
	              <string>tencent1106030794</string>
	          </array>
	      </dict>
	      <dict>
	          <key>CFBundleTypeRole</key>
	          <string>Editor</string>
	          <key>CFBundleURLName</key>
	          <string>微信</string>
	          <key>CFBundleURLSchemes</key>
	          <array>
	              <string>wxd48f07b4b4bad42a</string>
	          </array>
	      </dict>
	      <dict>
	          <key>CFBundleTypeRole</key>
	          <string>Editor</string>
	          <key>CFBundleURLName</key>
	          <string>新浪微博</string>
	          <key>CFBundleURLSchemes</key>
	          <array>
	              <string>wb667496208</string>
	          </array>
	      </dict>
	  </array>
##### Framework:  
###### 系统库
	libsqlite3.tbd
	CoreGraphics.framework  
 
###### 微信(完整版)-精简版无需添加以下依赖库
	SystemConfiguration.framework  
	CoreTelephony.framework  
	libsqlite3.tbd  
	libc++.tbd  
	libz.tbd  

###### QQ(完整版)-精简版无需添加以下依赖库
	SystemConfiguration.framework
	libc++.tbd  
###### 	新浪微博(完整版)-精简版无需添加以下依赖库
	SystemConfiguration.framework
	CoreTelephony.framework
	ImageIO.framework
	libsqlite3.tbd
	libz.tbd 
###### 	Twitter
	CoreData.framework 

### <a id="友盟统计">友盟统计</a>  
[详细接入文档](Integration manual/SDK_Um_Analyze.md)  
##### info.plist
	<key>NSAppTransportSecurity</key>
	<dict>
	    <key>NSAllowsArbitraryLoads</key>
	    <ture />
	</dict>
##### Framework: 
	libz.dylib(libz.tbd)
### <a id="阿里妈妈">阿里妈妈</a>  
[详细接入文档](Integration manual/SDK_阿里妈妈AFP广告 之banner.md)  
##### info.plist

##### Framework: 
	UTDIDframework
	.
	.
	.
	其余的根据聚合平台添加
### <a id="Applovin">Applovin</a>
[详细接入文档](Integration manual/SDK_Applovin.md)  
##### info.plist

##### Framework: 
	AdSupport
	AVFoundation
	CoreGraphics
	CoreMedia
	CoreTelephony
	StoreKit (NEW)
	SystemConfiguration
	UIKit
	WebKit (OPTIONAL)
 
### <a id="Heyzap">Heyzap</a>  
[详细接入文档](Integration manual/SDK_Heyzap.md)  
##### info.plist
	<key>NSAppTransportSecurity</key>
	<dict>
	    <key>NSAllowsArbitraryLoads</key>
	    <ture />
	</dict>
##### Framework: 
###### heyzap基础库
	CoreLocation;
	MediaPlayer;
	MessageUI;
	MobileCoreServices;
	QuartzCore;
	Security;
	CoreTelephony;
###### admob
	AdSupport;
	AudioToolbox;
	CoreGraphics;
	CoreMedia;
	CoreMotion;
	CoreVideo;
	Foundation;
	GLKit;
	GoogleMobileAds;
	OpenGLES;
	SafariServices;
	StoreKit;
	SystemConfiguration;
	WebKit;
###### 	applovin
	AdSupport
	AVFoundation
	CoreGraphics
	CoreMedia
	StoreKit (NEW)
	SystemConfiguration
	UIKit
	WebKit (OPTIONAL)
###### 	inmobi
	AVFoundation;
	AdSupport;
	AudioToolbox;
	CoreGraphics;
	EventKit;
	EventKitUI;
	SafariServices;
	Social;
	StoreKit;
	SystemConfiguration;
	UIKit;
	WebKit;
###### unityads
	AdSupport;
	CoreGraphics;
	StoreKit;
	SystemConfiguration;
###### vungle
	AVFoundation;
	AdSupport;
	AudioToolbox;
	CFNetwork;
	CoreGraphics;
	CoreMedia;
	StoreKit;
	SystemConfiguration;
### <a id="Inmobi">Inmobi</a>  
[详细接入文档](Integration manual/SDK_Inmobi.md)  
##### info.plist
##### Framework: 
	AVFoundation;
	AdSupport;
	AudioToolbox;
	CoreGraphics;
	EventKit;
	EventKitUI;
	SafariServices;
	Social;
	StoreKit;
	SystemConfiguration;
	UIKit;
	WebKit;
### <a id="hola">欧拉合作广告SDK</a>  
[详细接入文档](https://coding.net/u/holaverse/p/avid.lyRewardSdk/git)  
##### info.plist
	<key>NSAppTransportSecurity</key>
	<dict>
			<key>NSAllowsArbitraryLoads</key>
			<true/>
	</dict>
---
	<key>NSCalendarsUsageDescription</key>
	<string>Some ad content may create a calendar event.</string>
	<key>NSCameraUsageDescription</key>
	<string>Some ad content may access camera to take picture.</string>
	<key>NSPhotoLibraryUsageDescription</key>
	<string>Some ad content may require access to the photo library.</string>
##### Framework: 
	QuartzCore.framework
	MediaPlayer.framework
	libsqlite3.tbd
	libz.tbd
	CoreMedia.framework
	CoreGraphics.framework
	CFNetwork.framework
	WebKit.framework (Optional)
	WatchConnectivity.framework (Optional)
	SystemConfiguration.framework
	StoreKit.framework
	Social.framework
	MessageUI.framework
	JavaScriptCore.framework (Optional)
	EventKit.framework
	CoreTelephony.framework
	AVFoundation.framework
	AudioToolbox.framework
	AdSupport.framework
### <a id="GameCenter">GameCenter</a>
[详细接入文档](Integration manual/SDK_Gamecenter.md)  
##### info.plist
##### Framework:
### <a id="iOS内购">iOS内购</a>  
##### info.plist
##### Framework:
	StoreKit.framework
### <a id="本地推送">本地推送</a>  
### <a id="评价">评价</a>  
### <a id="获取设备信息">获取设备信息</a>  