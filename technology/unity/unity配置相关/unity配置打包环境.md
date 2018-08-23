### Android sdk 
有关Android sdk 可能会出现 Unable to list target platforms. 的错误。是因为安卓工具包里的tools文件夹下的bat命令更新后老版本的unity不能识别，找一个旧版本替换掉就好了。见[链接](https://www.cnblogs.com/we-hjb/archive/2017/04/27/6776371.html "链接")  
### jdk  
当出现CommandInvokationFailure: Failed to build apk.并且提示路径为jdk路径时，可以考虑是否jdk版本过高，如果是jdk9就有可能会报错，下载老版本jdk8替换路径即可 [链接](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
