#!/bin/sh

fileName="haha"
while [ "$fileName" != "exit" ]
do
    read -p "input your want open folder: " fileName
	if [ "$fileName" = "blog" ];
	then
		open /Users/tomato2/Work/gitoschina/YuanXCblog/yuanxinchao.github.io/technology/iOS
	fi

	if [ "$fileName" = "sdk" ];
	then
		open /Users/tomato2/Desktop/yuanxinchao/AllProject/SDK
	fi

	if [ "$fileName" = "soundmove" ];
	then
		open /Users/tomato2/Work/Unity/SoundMoveiOS
	fi

	if [ "$fileName" = "tjsdk" ];
	then
		open /Users/tomato2/Work/gitoschina/TjSdk
	fi

	if [ "$fileName" = "mima" ];
	then
		open /Users/tomato2/Desktop/yuanxinchao/账号密码/账号密码.txt
	fi

	if [ "$fileName" = "plist" ];
	then
		open /Users/tomato2/Desktop/yuanxinchao/账号密码/plist.txt
	fi
done