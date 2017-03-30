
#!/bin/sh

baseDirForScriptSelf=$(cd "$(dirname "$0")"; pwd)
dirname=${baseDirForScriptSelf}/copy
mkdir -p $dirname
read -p "input your want searchpath: " searchpath
echo $searchpath
find $searchpath -name  '*10.0.1.aar' -print -exec cp '{}' $dirname \;