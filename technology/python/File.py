#!/usr/bin/python
# -*- coding: UTF-8 -*-
 
# 打开一个文件
# fo = open("/Users/tomato2/Desktop/yuanxinchao/AllProject/八分音符/xix.txt", "wb")
stringss = 'hahah'
fo = open("/Users/tomato2/Desktop/yuanxinchao/fastPackage/iOSAuto/log.txt", "wb")
fo.write( stringss+"www.runoob.com!\nVery good site!\n");
 
# 关闭打开的文件
fo.close()