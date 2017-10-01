using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSql : MonoBehaviour
{
    private static List<cfg_achievement> _data1 = new List<cfg_achievement>();
    private static Dictionary<int, cfg_map> _data2 = new Dictionary<int, cfg_map>();
    public Text _text;

    void Start()
    {
        DoInit();
        string str = string.Empty;
        foreach (var ach in _data2)
        {
            str += ach.Value.desc+"\n";
            Debug.Log(ach.Value.desc);
        }
        foreach (var ach in _data1)
        {
            str += ach.desc + "\n";
            Debug.Log(ach.desc);
        }
//        foreach (var ach in _data3)
//        {
//            str += ach.Name + "\n";
//            Debug.Log(ach.Name);
//        }
        _text.text = str;
    }
    private static void DoInit()
    {
        DataBase.Connect();
        _data1 = DataBase.TableAsList<cfg_achievement>();
        _data2 = DataBase.TableAsDictionary<int, cfg_map>(v => v.id);
    }
}
