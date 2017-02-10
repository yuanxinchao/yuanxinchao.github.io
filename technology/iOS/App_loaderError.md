##ipa提交Application loader常见错误

###错误1
* ERROR ITMS-90474: "Invalid Bundle. iPad Multitasking support requires these orientations: 'UIInterfaceOrientationPortrait,UIInterfaceOrientationPortraitUpsideDown,UIInterfaceOrientationLandscapeLeft,UIInterfaceOrientationLandscapeRight'. Found 'UIInterfaceOrientationPortrait,UIInterfaceOrientationLandscapeLeft,UIInterfaceOrientationLandscapeRight' in bundle "bundle_Name"

#####解决办法
![Solve1](./ErrorImage/Solve1.png)
#####或
Edit your plist file from xcode and add those lines
>		<key>UIRequiresFullScreen</key>
>		<true/>

---
    
###错误2
* ERROR ITMS-90086: "Missing 64-bit support. iOS apps submitted to the App Store must include 64-bit support and be built with the iOS 8 SDK or later. We recommend using the default "Standard Architectures" build setting for "Architectures" in Xcode, to build a single binary with both 32-bit and 64-bit support."
#####解决办法  
![Solve2](./ErrorImage/Solve2.png)  
将Architectures改为同时兼容arm7和arm64打包。