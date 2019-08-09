using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

public class PrintBuffData
{
    public static void PrintBuff()
    {
        FieldInfo[] fieldInfos;
        fieldInfos = typeof(CommanderConst).GetFields();





        ClearData();
        AddTitle("属性id");
        AddTitle("xxx");
        AddTitle("xxx");
        AddTitle("xxx");
        AddTitle("xxx");
        FinishTitle();
        AddRowData("xxx");
        AddRowData("xxx");
        FinishRow();


        WriteString(_content);

    }



    static string _content = string.Empty;
    private static int _titleCol = 0;
    private static int _dataCol = 0;


    private static void AddRowData(string str)
    {
        _content += "|" + str;
        _dataCol++;
    }
    private static void FinishRow()
    {
        for (int i = _dataCol; i < _titleCol -1; i++)
        {
            _content += "|";
        }
        _content += "|";
        _content += "\n";
        _dataCol = 0;
    }

    private static void ClearData()
    {
        _content = string.Empty;
        _titleCol = 0;

    }
    private static void AddTitle(string str)
    {
        _content += "|" + str;
        _titleCol++;
    }
    private static void FinishTitle()
    {
        _content += "|";
        _content += "\n";
        for (int i = 0; i < _titleCol; i++)
        {
            _content += "|----";
        }
        _content += "|";
        _content += "\n";
    }

    private static void WriteString(string str)
    {
        var file = File.Open("buffData.md", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        Encoding e = Encoding.UTF8;
        byte[] content = e.GetBytes(str);
        file.SetLength(0);
        file.Write(content, 0, content.Length);
        file.Close();
    }


}
