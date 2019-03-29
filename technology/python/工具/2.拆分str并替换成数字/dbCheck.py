# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import sqlite3
import os
import re

DB_FILE = "p16.db"


def ProcessDesc(cu_sqlite):
    lineList = []
    descList = []
    descListKey = []
    cu_sqlite.execute("select id,desc_list from cfg_commander_equip")
    results = cu_sqlite.fetchall()
    for row in results:
        ss = str(row[1]).split(',')
        for s in ss:
            if s in lineList:
                pass
            else:
                lineList.append(s)

        nums = ""
        for i in range(0,len(ss)):
            nums = nums + str(lineList.index(ss[i]))
            if i != len(ss)-1:
                nums = nums + ","

        id = row[0]
        descListKey.append(id)
        descList.append(nums)

    for i in range(0, len(lineList)):
        print str(i) + " \t" + str(lineList[i])

    for i in range(0, len(descListKey)):
        print str(descListKey[i]) + "\t" + str(descList[i])


def CheckDB():
    global DB_NAME, DB_FILE
    cx_sqlite = sqlite3.connect(DB_FILE)
    cu_sqlite = cx_sqlite.cursor()
    # CheckShowShip(cu_sqlite)
    # CheckShipShopUnlock(cu_sqlite)
    ProcessDesc(cu_sqlite)
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
		
# 0 	xxx
# 1 	xxxxx
# 2 	xx
# 3 	xxxxx
# -----------------------
# 510   xxx,xxxxx,xx
#         ↓
# 510	0,1,2
# 530	3,4,5
# 540	0,1,5
# 560	3,4,5
# 1110	6,7,8
# 1130	3,4,7
