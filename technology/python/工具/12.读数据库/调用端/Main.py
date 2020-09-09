from ReadDBAndSql import DBConnect
import FileInPath
import os
from VisitorFile import FileVisitor
SqlStr = "select icon_up,icon_down from cfg_buff_keyword"

if __name__ == '__main__':
    # 读数据库
    # listA = []
    # readDB = DBConnect()
    # results = readDB.Execute(SqlStr)
    # for row in results:
    #     # print("id=" + str(row[0]) + " icon=" + str(row[1]))
    #     if row[0] is not "":
    #         iconId = row[0].split('/')[1]
    #         if iconId not in listA:
    #             listA.append(iconId)
    #     if row[1] is not "":
    #         iconId = row[1].split('/')[1]
    #         if iconId not in listA:
    #             listA.append(iconId)

    # 查看路径下所有文件
    # listB = []
    # path = r"C:\working\p16s\devel\client\u3dprj_oversea\Assets\UI\ResourcesAB\Skill"
    # fileList = FileInPath.AllFileInPath(path)
    # for name in fileList:
    #     baseName = FileInPath.GetFileName(name, False)
    #     # print(baseName)
    #     if baseName not in listB:
    #         listB.append(baseName)

    # 求差集
    # diff = [i for i in listA if i not in listB]
    # print(diff)

    #正则匹配一个文件
    fileName = r"xxx.cs"
    regex = r"(\"Battle/.*\")"
    file = FileVisitor(fileName)
    ret = file.Regex(regex)
    for x in ret:
        print(x)
