####1. 删除Xcode中多余的证书provisioning profile
>手动删除
>

xcode5 provisioning profile path： ~/Library/MobileDevice/Provisioning Profiles

####2.	LSApplicationQueriesSchemes
>iOS9之后对于**canOpenURL**方法会去搜索info.plist里key为LSApplicationQueriesSchemes的可跳转白名单。如果没有声明的话这个方法会返回NO。
>

Apple官网的说法:
>If your app is linked on or after iOS 9.0, you must declare the URL schemes you want to pass to this method. Do this by using the LSApplicationQueriesSchemes array in your Xcode project’s Info.plist file. For each URL scheme you want your app to use with this method, add it as a string in this array.

>**If your (iOS 9.0 or later) app calls this method using a scheme you have not declared, the method returns NO,** whether or not an appropriate app for the scheme is installed on the device.

>If your app is linked against an earlier version of iOS but is running in iOS 9.0 or later, you can call this method on **50** distinct URL schemes. After hitting this limit, subsequent calls to this method return NO. If a user reinstalls or upgrades the app, iOS resets the limit.

>Unlike this method, **the openURL: method is not constrained by the LSApplicationQueriesSchemes requirement**: If an app that handles a scheme is installed on the device, the openURL: method works, whether or not you have declared the scheme.
iOS 9 brings some changes regarding of canOpenURL: in UIApplication class.
>

值得一提的事openURL不会受此限制。  
#### 3.Build Failed: error: make directory File exists 或者是 directory empty  
记住：沉着冷静，处变不惊，仔细分析。  
提示路径问题，缺少某个文件，思路：去build path看看是否有多余或不存在路径，去compile file 里看一下有没有重复添加或者未添加，看一下缺少的文件在工程的位置。相信你一定能解决的
####  4.could not locate device support files
xcode未找到ios对应系统支持库所致。解决方案，更新xcode或者库[https://www.jianshu.com/p/a11ac42f75c3](https://www.jianshu.com/p/a11ac42f75c3)
