using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class SaveLog:MonoBehaviour
{
    static SaveLog instance = null;
    private static string outPath;

    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();

    void Start ()
    {
        //Application.persistentDataPath Unity中只有这个路径是既可以读也可以写的。
        outPath = Application.persistentDataPath + "/outLog.txt";
        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(outPath))
        {
            File.Delete(outPath);
        }
        console.Getinstance().print(outPath);

        //在这里做一个Log的监听
        Application.RegisterLogCallback(HandleLog);
    }

    void HandleLog (string logString , string stackTrace , LogType type)
    {
        mWriteTxt.Add(logString);
        if (type == LogType.Log)
        {
            WriteResult(logString, stackTrace);
        }
    }
    //这里我把错误的信息保存起来，用来输出在手机屏幕上
    static public void WriteResult (string logString , string stackTrace)
    {
        List<string> mWriteTxt = new List<string>();
        string TempStr = null;
        TempStr = "记录时间：" + System.DateTime.Now;
        mWriteTxt.Add(TempStr);
        TempStr = "错误信息：" + logString;
        mWriteTxt.Add(TempStr);
        TempStr = "堆栈信息：" + stackTrace;
        mWriteTxt.Add(TempStr);
        string[] temp = mWriteTxt.ToArray();

        foreach (string t in temp)
        {
            using (StreamWriter writer = new StreamWriter(outPath, true, Encoding.UTF8))
            {
                writer.WriteLine(t);
            }
        }
    }
}
