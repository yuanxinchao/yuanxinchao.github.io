# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import sqlite3
import os
import re

DB_FILE = "E:\xxx\xxx.db"

def DeleTalentIcon(cu_sqlite):
    cu_sqlite.execute("select xxx from cfg_xx_xx")
    results = cu_sqlite.fetchall()
    for row in results:
        trajectory_filename = r"E:\xxx\ShipIcons"
        if not HasThisPic(trajectory_filename,"ship_" + str(row[0])+".png"):
            print "not has this pic " + "ship_" + str(row[0])+".png"


def HasThisPic(pathName,picName):
    for root, dirs, files in os.walk(pathName):
        for name in files:
            if name == picName:
                return True

    return False


def CheckDB():
    global DB_NAME, DB_FILE
    cx_sqlite = sqlite3.connect(DB_FILE)
    cu_sqlite = cx_sqlite.cursor()
    DeleTalentIcon(cu_sqlite)
    cx_sqlite.commit()
    cu_sqlite.close()
    cx_sqlite.close()
    print "Check finish"


if __name__ == '__main__':
    import sys

    reload(sys)
    sys.setdefaultencoding('utf8')
    if os.path.exists(DB_FILE):
        CheckDB()
    else:
        print ("ERROR")
