# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import shutil, os
import re


def SetIconPath():
    # print "id=" + str(row[0]) + " icon"+str(row[1])
    trajectory_filename = r"C:\working\p16s\devel\client\u3dprj_oversea\Assets\UI\ResourcesAB\Skill"
    for file1 in os.listdir(trajectory_filename):
        path = os.path.join(trajectory_filename, file1)
        if os.path.isfile(path):
            trajectory_filename2 = r"C:\working\p16s\devel\client\u3dprj\Assets\UI\ResourcesAB\Skill"
            for root, dirs, files in os.walk(trajectory_filename2):
                for name in files:
                    path2 = os.path.join(root, name)
                    ss = path2.split('Skill')
                    lowPath = ss[1]
                    pathMoveTo = trajectory_filename + lowPath
                    if file1 == name:
                        makeDir = os.path.dirname(pathMoveTo)
                        if not os.path.exists(makeDir):
                            os.makedirs(makeDir)
                        shutil.move(path,pathMoveTo)

                # os.remove(path)


def CheckDB():
    global DB_NAME, DB_FILE
    SetIconPath()
    print "Check finish"


if __name__ == '__main__':
    import sys

    reload(sys)
    sys.setdefaultencoding('utf8')
    CheckDB()
