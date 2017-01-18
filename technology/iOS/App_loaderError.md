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
    
