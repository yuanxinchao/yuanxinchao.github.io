# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import sqlite3
import os
import re

DB_FILE = "p16.db"

def DeleTalentIcon(cu_sqlite):
    cu_sqlite.execute("select id,icon from cfg_commander_talent")
    results = cu_sqlite.fetchall()
    for row in results:
        # print "id=" + str(row[0]) + " icon"+str(row[1])
        trajectory_filename = r"C:\working\p16s\devel\client\u3dprj\Assets\UI\ResourcesAB\Skill"
        for root, dirs, files in os.walk(trajectory_filename):
            for name in files:
                path = os.path.join(root, name)
                if name == str(row[1]) + ".jpg" or name == str(row[1]) + ".jpg.meta":
                    print(path)
                    # os.remove(path)




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
