#!/usr/bin/python
# -*- coding: UTF-8 -*-

import sys
from mod_pbxproj import XcodeProject

fo = open("/Users/tomato2/Desktop/yuanxinchao/fastPackage/iOSAuto/log.txt", "wb")
fo.write( "打开Xcode工程 \n");
pathToProjectFile = sys.argv[1] + '/Unity-iPhone.xcodeproj/project.pbxproj'
pathToNativeCodeFiles = sys.argv[2]
allSdkName = 'umengShareSdk,talkingDataSdk,vungleSdk,applovinSdk'
project = XcodeProject.Load( pathToProjectFile )

for obj in list(project.objects.values()):
    if 'path' in obj:
        if project.path_leaf( 'System/Library/Frameworks/AdSupport.framework' ) == project.path_leaf(obj.get('path')):
            project.remove_file(obj.id)

if project.modified:
    project.save()




fo.write("需要配置的sdk有"+ allSdkName+"\n");

# 系统库
project.add_file_if_doesnt_exist( 'System/Library/Frameworks/CoreGraphics.framework', tree='SDKROOT', parent='Frameworks' )
project.add_file_if_doesnt_exist( 'usr/lib/libsqlite3.dylib', tree='SDKROOT', parent='Frameworks' )


def disableArc(filePath):
	fileId = project.get_file_id_by_path(filePath)
	fo.write("给filePath="+filePath+"   "+"fileId="+fileId+"  的文件加入-fno-objc-arc"+"\n")
	files = project.get_build_files(fileId)
	for f in files:
		f.add_compiler_flag('-fno-objc-arc')

fo.write( "added usr/lib/libsqlite3.dylib"+"\n")

if "umengShareSdk" in allSdkName:
	fo.write("开始加入友盟sdk需要的框架和依赖 " + "\n");
	#-----framework
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/CoreTelephony.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/SystemConfiguration.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/ImageIO.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libc++.dylib', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libz.dylib', tree='SDKROOT', parent='Frameworks' )
	#-----addfolder
	project.add_folder( pathToNativeCodeFiles+'/UMSocial', excludes=["^.*\.meta$"] )

	fo.write("完成加入友盟sdk需要的框架和依赖 "+"\n");
if "talkingDataSdk" in allSdkName:
	fo.write("开始加入talkingdata sdk需要的框架和依赖 " + "\n");
	#-----addfolder
	project.add_folder( pathToNativeCodeFiles+'/TalkData', excludes=["^.*\.meta$"] )
	fo.write("完成加入talkingdata sdk需要的框架和依赖 " + "\n");
	pass
if "vungleSdk" in allSdkName:
	fo.write("开始加入vungleSdk需要的框架和依赖 " + "\n");
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/StoreKit.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/AdSupport.framework', tree='SDKROOT', weak=True, parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/WebKit.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libz.1.1.3.dylib', tree='SDKROOT', parent='Frameworks' )
	#-----addfolder
	project.add_folder( pathToNativeCodeFiles+'/VungleSDK', excludes=["^.*\.meta$"] )
	fo.write("完成加入vungleSdk需要的框架和依赖 " + "\n");
if "applovinSdk" in allSdkName:
	fo.write("开始添加需要的-fno-objc-arc " + "\n");
	disableArc('Libraries/ALAdDelegateWrapper.m')
	disableArc('Libraries/ALInterstitialCache.m')
	disableArc('Libraries/ALManagedLoadDelegate.m')
	disableArc('Libraries/AppLovinUnity.mm')
	fo.write("完成添加需要的-fno-objc-arc " + "\n");



fo.write( "开始添加 -ObjC 标志"+"\n")

project.add_other_ldflags(['-ObjC'])
fo.write( "完成添加 -ObjC 标志"+"\n")
fo.write( "开始project保存"+"\n")
if project.modified:
    project.save()
fo.write( "完成project保存"+"\n")


fo.write( "关闭文件"+"\n")
# 关闭打开的文件
fo.close()
