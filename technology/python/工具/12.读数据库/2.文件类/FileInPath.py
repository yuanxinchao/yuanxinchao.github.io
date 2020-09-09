# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import os


# 某一路径下的所有文件
def AllFileInPath(path):
    ret = []

    # os.walk(path) 返回的结果是
    # 一个包含三维数组的list
    # 数组1：当前所在文件夹   数组2：当前文件夹下的文件夹  数组3：当前文件夹下的文件
    for root, dirs, files in os.walk(path):
        for name in files:
            n = os.path.join(root, name)
            ret.append(n)
    return ret


# 某一路径下的所有文件夹
def AllDirsInPath(path):
    ret = []
    for dirs in os.walk(path):
        for name in dirs:
            ret.append(name)

    return ret


# 获取一个带路径文件的名字 (withSuffix)是否带有后缀名
def GetFileName(file, withSuffix):
    if withSuffix:
        return os.path.basename(file)

    # 循环去除所有后缀
    fileName = os.path.basename(file)
    name = os.path.splitext(fileName)[0]
    suffix = os.path.splitext(fileName)[1]
    while suffix is not "":
        name = os.path.splitext(name)[0]
        suffix = os.path.splitext(name)[1]

    return name


# 获取一个带路径文件的路径
def GetFilePath(file):
    return os.path.dirname(file)
