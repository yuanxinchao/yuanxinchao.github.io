# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''

import sqlite3
import os

DB_FILE = "p16.db"


def GetMainKey(itemType, itemId):
    return str(itemType) + "|" + str(itemId);


class Item:
    def __init__(self, itemType, itemId):
        self.itemType = itemType
        self.itemId = itemId

    def ToString(self):
        return GetMainKey(self.itemType, self.itemId)


class Node:
    NodeCount = 0

    def __init__(self, item):
        self.item = item
        self.left = None
        self.right = None
        Node.NodeCount += 1


class Side:
    SideCount = 0

    def __init__(self):
        self.start = None
        self.end = None
        self.left = None
        self.right = None
        Side.SideCount += 1


class ItemType:
    ShipEquip = 100
    Ship = 200
    Radar = 300
    CaptEquip = 400
    Capt = 500
    KrGift = 600
    Common = 700


ItemAllType = [ItemType.Common,
               ItemType.ShipEquip,
               ItemType.Ship,
               ItemType.Radar,
               ItemType.CaptEquip,
               ItemType.Capt,
               ItemType.KrGift,
               ]

itemMainNodes = {}


def IsItemInitemList(itemType, itemId):
    for node in itemMainNodes.values():
        if itemType == node.item.itemType and itemId == node.item.itemId:
            return 1

    return 0


def GetItemId(s):
    s0 = int(s.split("|")[0])
    s1 = int(s.split("|")[1])
    if s0 in ItemAllType:
        return s1
    return s0


def GetItemType(s):
    s0 = int(s.split("|")[0])
    if s0 in ItemAllType:
        return s0
    return ItemType.Common


def PrintItemNode(node):
    print(node.item.ToString(), end="===")
    side = node.right
    while side is not None:
        print(side.end.ToString(), end=",")
        side = side.right


def PrintItemVerseNode(node):
    if 300101 <= node.item.itemId <= 301305:
        print(node.item.ToString(), end=" ")
        side = node.left
        while side is not None:
            print(side.start.ToString(), end=" ")
            side = side.left
            if side is not None:
                print(",", end="")
        print()


def PrintItemNodes():
    for node in itemMainNodes.values():
        PrintItemNode(node)
        print()


def PrintItemVerseNodes():
    for node in itemMainNodes.values():
        PrintItemVerseNode(node)


def SetNodeSide(itemType, itemId, strs):
    ss = strs.split(',')
    # 无节点 return
    if len(ss) <= 0:
        return
    if IsItemInitemList(itemType, itemId) == 0:
        node = Node(Item(itemType, itemId))
        itemMainNodes[GetMainKey(itemType, itemId)] = node
    else:
        node = itemMainNodes[GetMainKey(itemType, itemId)]
    temp = node

    ThisNodeKeys = []
    for s in ss:
        sideItemId = GetItemId(s)
        sideItemType = GetItemType(s)
        key = GetMainKey(sideItemType, sideItemId)
        # 重复节点不再添加
        if key in ThisNodeKeys:
            continue

        if key not in itemMainNodes.keys():
            n = Node(Item(sideItemType, sideItemId))
            itemMainNodes[key] = n

        side = Side()
        side.start = node.item
        side.end = itemMainNodes[key].item

        temp.right = side
        temp = temp.right

        ThisNodeKeys.append(key)


def SetVerseNodeSide():
    for node in itemMainNodes.values():
        temp = node
        key = node.item
        for n in itemMainNodes.values():
            side = n.right
            while side is not None:
                if side.end == key:
                    temp.left = side
                    temp = temp.left
                side = side.right


def SetNodes(results, itemType):
    for row in results:
        itemId = row[0]
        strs = str(row[1])
        SetNodeSide(itemType, itemId, strs)


def cfg_item_use(cu_sqlite):
    cu_sqlite.execute("select itemid,items from cfg_item_use")
    results = cu_sqlite.fetchall()
    SetNodes(results, ItemType.Common)


def cfg_gift_package(cu_sqlite):
    cu_sqlite.execute("select id,contents from cfg_gift_package")
    results = cu_sqlite.fetchall()
    SetNodes(results, ItemType.KrGift)


# def cfg_manufactory(cu_sqlite):
#     cu_sqlite.execute("select eid,resolveitem from cfg_manufactory")
#     results = cu_sqlite.fetchall()
#     SetNodes(results, ItemType.ShipEquipId)

SplitCreateSql = {
    'cfg_item_get': "CREATE TABLE IF NOT EXISTS `cfg_item_get` (`id` int, `path` varchar, PRIMARY KEY (`id`))"
}
SplitInsertSql = {
    'cfg_item_get': "INSERT into cfg_item_get VALUES ("
}


def CreateTable(cu_sqlite):
    cu_sqlite.execute(SplitCreateSql["cfg_item_get"])
    cu_sqlite.execute("DELETE FROM cfg_item_get")
    for node in itemMainNodes.values():
        if 300101 <= node.item.itemId <= 301305:
            side = node.left
            path = ""
            while side is not None:
                if side.start.itemType == ItemType.Common:
                    path = path + str(side.start.itemId)
                else:
                    path = path + GetMainKey(side.start.itemType, side.start.itemId)
                if side.left is not None:
                    path = path + ","
                side = side.left
            print(path)
            sqlInsert = "INSERT into cfg_item_get VALUES (" + str(node.item.itemId) + ",\"" + path + "\")"
            cu_sqlite.execute(sqlInsert)
    pass


def OrthogonalLinkedList(cu_sqlite):
    cfg_item_use(cu_sqlite)
    cfg_gift_package(cu_sqlite)
    # cfg_manufactory(cu_sqlite)
    # print "cfg_item_use " + " itemid=" + str(row[0]) + " items" + str(row[1])
    SetVerseNodeSide()
    PrintItemVerseNodes()

    CreateTable(cu_sqlite)


def CheckDB():
    global DB_NAME, DB_FILE
    cx_sqlite = sqlite3.connect(DB_FILE)
    cu_sqlite = cx_sqlite.cursor()
    OrthogonalLinkedList(cu_sqlite)
    cx_sqlite.commit()
    cu_sqlite.close()
    cx_sqlite.close()
    print("Check finish")


if __name__ == '__main__':
    dic = {}
    if os.path.exists(DB_FILE):
        CheckDB()
    else:
        print("ERROR")
