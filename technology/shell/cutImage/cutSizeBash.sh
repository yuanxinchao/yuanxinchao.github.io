#!/bin/sh

baseDirForScriptSelf=$(cd "$(dirname "$0")"; pwd)
dirname=${baseDirForScriptSelf}/cutimg
filename=${baseDirForScriptSelf}/icon.png
echo "full path to currently executed script is : ${baseDirForScriptSelf}"
name_array=("Icon-72.png" "Icon-76.png" "Icon-120.png" "Icon-144.png" "Icon-152.png" "Icon-180.png" "Icon.png" "Icon@2x.png")
size_array=("72" "76" "120" "144" "152" "180" "57" "114")
mkdir -p $dirname

for ((i=0;i<${#name_array[@]};++i)); do
    m_dir=$dirname/${name_array[i]}
    cp -f $filename $m_dir
    sips -Z ${size_array[i]} $m_dir
done

