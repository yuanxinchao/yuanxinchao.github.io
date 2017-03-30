#!/usr/bin/python
# -*- coding: UTF-8 -*-

# 导入内置math模块
import math
import plistlib
import datetime
import time


with open('./KeyInfo.xml','rb') as fp0:
    KeyList = plistlib.readPlist(fp0);

with open('./BaseInfo.xml','rb') as fp1:
    BaseList = plistlib.readPlist(fp1);

with open('./Info.plist','rb') as fp2:
    InfoList = plistlib.readPlist(fp2);


InfoList =dict(KeyList.items()+BaseList.items()+InfoList.items());
plistlib.writePlist(InfoList, './Info.plist')





# aPPict=dict(
#         hehe="xczcxsafa",
#         heihei=u'M\xe4ssig, Ma\xdf',
#         huhu=True,
#         hoho=False,
#         CFBundleURLSchemes =("QQ4sadas1ebb9ab",),
#     ),
# aPPict2=dict(
#         hehe="<hello & hi there!>",
#         heihei=u'axiba',
#         huhu=True,
#         hoho=False,
#         CFBundleURLSchemes =("asdasdsadfdsf!!!",),
#     ),
# heihei["CFBundleURLTypes"]=haha;
# unicode keys are possible, but a little awkward to use: