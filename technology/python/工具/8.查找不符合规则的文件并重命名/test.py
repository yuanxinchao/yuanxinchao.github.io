# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import os
def BianLiDir(dir):
    for file1 in os.listdir(dir):
        path = os.path.join(dir, file1)
        if os.path.isdir(path):
            BianLiDir(path)
        else:
            if path.endswith(".png"):
                noExtension = os.path.splitext(file1)[0]
                extension = os.path.splitext(file1)[1]
                array = noExtension.split('_')
                last = array[len(array) -1]
                name = ""
                if not last.isdigit():
                    name = array[0]+"_"+array[2]+"_"+"8"+extension
                    newPath = os.path.join(dir, name)
                    os.rename(path, newPath)

if __name__ == '__main__':
    import sys

    reload(sys)
    sys.setdefaultencoding('utf8')
    trajectory_filename = r"E:\xx\xxx"
    BianLiDir(trajectory_filename)
