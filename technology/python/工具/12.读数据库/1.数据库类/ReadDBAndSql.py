# -*- coding:utf-8 -*-
'''
Created on 2017-12-25

@author: rex
'''
import sqlite3

DB_FILE = "p16.db"


class DBConnect:
    def __init__(self):
        self.cx_sqlite = sqlite3.connect(DB_FILE)
        self.cu_sqlite = self.cx_sqlite.cursor()

    def Execute(self, SqlStr):
        self.cu_sqlite.execute(SqlStr)
        results = self.cu_sqlite.fetchall()
        return results

    def CloseDB(self):
        self.cx_sqlite.commit()
        self.cu_sqlite.close()
        self.cx_sqlite.close()
