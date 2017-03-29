#!/bin/sh

baseDirForScriptSelf=$(cd "$(dirname "$0")"; pwd)
dirname=${baseDirForScriptSelf}/cutimg
filename=${baseDirForScriptSelf}/icon.png
echo "full path to currently executed script is : ${baseDirForScriptSelf}"
read -p "input your want size: " whichsize
echo $whichsize

mkdir -p $dirname
m_dir=$dirname/$whichsize.png
cp -f $filename $m_dir
sips -Z $whichsize $m_dir


