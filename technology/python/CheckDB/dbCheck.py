# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import sqlite3
import os
import re

DB_FILE = "p16.db"


def sqlTypeToCSharpType(sqlType):
    if sqlType == "int":
        return "int"
    elif sqlType in ["varchar", "text"]:
        return "string"
    elif sqlType in ["float"]:
        return "float"
    elif sqlType in ["bigint"]:
        return "long"
    else:
        return sqlType


# 匹配 , | 格式 的字串
Format = r'^(/d+\|/d+,)*/d+\|/d+$'
# cfg_ship_display showShip
showShip = r'^([0-9]{8}\|[01],)*[0-9]{8}\|[01]$'
# cfg_ship_shop_unlock unit_pos
unitPos = r'^([0-9]{8}\|[01],)*[0-9]{8}\|[01]$'


# 验证cfg_ship_display 格式
def CheckShowShip(cu_sqlite):
    cu_sqlite.execute("select id,show_ship from cfg_ship_display")
    showShips = cu_sqlite.fetchall()
    for row in showShips:
        matchObj = re.match(showShip, row[1])
        if matchObj:
            pass
        else:
            print "cfg_ship_display->show_ship doesn't match rule" + " id=" + str(row[0])

        result = row[1].split(',')
        ships = set()
        for s in result:
            ship = s.split('|')
            if ship[0] in ships:
                print "cfg_ship_display->show_ship repeat shipid=" + ship[0] + " id=" + str(row[0])
            else:
                ships.add(ship[0])

# 验证cfg_ship_shop_unlock 格式
def CheckShipShopUnlock(cu_sqlite):
    cu_sqlite.execute("select ship_id,unit_pos from cfg_ship_shop_unlock")
    results = cu_sqlite.fetchall()
    for row in results:
        matchObj = re.match(r'^([0-6](\|-*[0-9]+){4},)*[0-6](\|-*[0-9]+){4}$', row[1])
        if matchObj:
            pass
        else:
            print "cfg_ship_shop_unlock->unit_pos doesn't match rule" + " shipid=" + str(row[0])

def CheckDB():
    global DB_NAME, DB_FILE
    cx_sqlite = sqlite3.connect(DB_FILE)
    cu_sqlite = cx_sqlite.cursor()
    CheckShowShip(cu_sqlite)
    CheckShipShopUnlock(cu_sqlite)
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
