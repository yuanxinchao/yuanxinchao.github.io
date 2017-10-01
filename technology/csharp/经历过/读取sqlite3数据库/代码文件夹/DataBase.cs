using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using SQLite4Unity3d;
#if !UNITY_EDITOR
using System.Collections;
#endif
public static class DataBase
{
    public static SQLiteConnection _connection = null;
    private const string DatabaseName = "test.db";
    private const string Key = "jedi@p16s"; //加密密码
    public static void Clear()
    {
        Close();
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        Debug.Log("delete db file: " + filepath);
        File.Delete(filepath);
    }
    public static bool Connect()
    {
        Debug.Log("db connect " + DatabaseName);

        //Debug.Log("sqlite3 version: " + SQLite3.LibVersionNumber());
        if (_connection != null)
        {
            Debug.Log("dataBase has connected, needn't connect again");
            return true;
        }


#if UNITY_EDITOR
        var dbPath = string.Format("Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
            var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Copy(loadDb, filepath);
#else
        throw new Exception("unsupport platform");
#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        Debug.Log("Final db PATH: " + dbPath);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

#if UNITY_EDITOR
        _connection.Trace = true;
#else
        _connection.Trace = false;
#endif
        return true;
    }
    public static List<T> Query<T>(string sql) where T : new()
    {
        return _connection.Query<T>(sql);
    }

    public static Dictionary<TKey, TValue> QueryAsDictionary<TKey, TValue>(string query, Func<TValue, TKey> func) where TValue : new()
    {
        return _connection.DeferredQuery<TValue>(query).ToDictionary(func);
    }

    public static Dictionary<TKey, TValue> TableAsDictionary<TKey, TValue>(Func<TValue, TKey> func) where TValue : new()
    {
        var e = _connection.DeferredQuery<TValue>("select * from " + typeof(TValue).Name);
        return e.ToDictionary(func);
    }

    public static List<TValue> TableAsList<TValue>() where TValue : new()
    {
        return Query<TValue>("select * from " + typeof(TValue).Name);
    }

    public static IEnumerable<TValue> Table<TValue>() where TValue : new()
    {
        return _connection.DeferredQuery<TValue>("select * from " + typeof(TValue).Name);
    }
    public static void Close()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }
}
