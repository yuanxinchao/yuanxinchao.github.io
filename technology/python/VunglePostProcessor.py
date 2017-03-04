#!/usr/bin/python
# -*- coding: UTF-8 -*-

import sys
from mod_pbxproj import XcodeProject

fo = open("/Users/tomato2/Desktop/yuanxinchao/fastPackage/iOSAuto/log.txt", "wb")
fo.write( "Opened it \n");
pathToProjectFile = sys.argv[1] + '/Unity-iPhone.xcodeproj/project.pbxproj'
pathToNativeCodeFiles = sys.argv[2]
allSdkName = 'umengShareSdkvungleSdk'
project = XcodeProject.Load( pathToProjectFile )

for obj in list(project.objects.values()):
    if 'path' in obj:
        if project.path_leaf( 'System/Library/Frameworks/AdSupport.framework' ) == project.path_leaf(obj.get('path')):
            project.remove_file(obj.id)

if project.modified:
    project.save()

project.add_folder( pathToNativeCodeFiles, excludes=["^.*\.meta$"] )


fo.write( allSdkName+"\n");

# 系统库
project.add_file_if_doesnt_exist( 'System/Library/Frameworks/CoreGraphics.framework', tree='SDKROOT', parent='Frameworks' )
project.add_file_if_doesnt_exist( 'usr/lib/libsqlite3.dylib', tree='SDKROOT', parent='Frameworks' )

fo.write( "added usr/lib/libsqlite3.dylib"+"\n");

if "umengShareSdk" in allSdkName:
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/CoreTelephony.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/SystemConfiguration.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/ImageIO.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libc++.dylib', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libz.dylib', tree='SDKROOT', parent='Frameworks' )
	fo.write( "added umengShareSdk"+"\n");
if "talkingDataSdk" in allSdkName:
	pass
if "vungleSdk" in allSdkName:
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/StoreKit.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/AdSupport.framework', tree='SDKROOT', weak=True, parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'System/Library/Frameworks/WebKit.framework', tree='SDKROOT', parent='Frameworks' )
	project.add_file_if_doesnt_exist( 'usr/lib/libz.1.1.3.dylib', tree='SDKROOT', parent='Frameworks' )
	fo.write( "added vungleSdk"+"\n");

fo.write( "finish added all"+"\n");

project.add_other_ldflags(['-ObjC'])

if project.modified:
    project.save()

 
# 关闭打开的文件
fo.close()