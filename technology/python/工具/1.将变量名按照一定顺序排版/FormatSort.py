#!/usr/bin/python
# -*- coding: UTF-8 -*-
import re


def IsMatch(e):
    x = re.search(r".*public const int.*= +([0-9]+);.*", e, re.MULTILINE)
    if x:
        return 1
    return 0


def SortLines(e):
    x = re.search(r".*public const int.*= +([0-9]+);.*", e, re.MULTILINE)
    print(e)
    if x:
        return int(x.group(1))


# 打开一个文件
# fo = open("/Users/tomato2/Desktop/yuanxinchao/AllProject/八分音符/xix.txt", "wb")
lineList = []
block = ""
with open("need.txt", "r+", encoding='UTF-8') as f:
    for line in f:
        if IsMatch(line) == 0:
            block = block + line
        else:
            block = block + line
            lineList.append(block)
            block = ""
    lineList.sort(key=SortLines)
    f.seek(0)
    f.truncate()
    for line in lineList:
        f.write(line)

f.close()
# 栗子--------------
# **************排版前*********
#     public const int AF = 13; //行动力
#     //行动力
#     public const int Tili = 13;
#
#     public const int Gold = 10; //氪金(元宝)
#     public const int Exp = 11; //领主经验
#     //基础资源
#     public const int Metal = 1;//金属
#     public const int Crystal = 2;//晶体
#     public const int Energy = 3;//能源
# **************排版后*********
#     //基础资源
#     public const int Metal = 1;//金属
#     public const int Crystal = 2;//晶体
#     public const int Energy = 3;//能源
#
#     public const int Gold = 10; //氪金(元宝)
#     public const int Exp = 11; //领主经验
#
#     public const int AF = 13; //行动力
#     //行动力
#     public const int Tili = 13;
