using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using I2.Loc;
using JediEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeTextFinder : EditorWindow
{
    [MenuItem("Tools/I2 Localization/查找含指定key的本地化脚本位置")]
    public static void OnShow()
    {
        var w = GetWindow<LocalizeTextFinder>(false, "查找quickLocalize");
        w.wantsMouseMove = false;
        w.ShowPopup();
    }

    void Awake()
    {
        //load source if no source exists
        Tools_NGUI_to_I2.LoadGlobalSource();
    }

    private static string _input = "输入要查找的翻译key";
    private void OnGUI()
    {
        _input = GUILayout.TextField(_input);

        if (GUILayout.Button("开始查找"))
        {
            Search(_input);
        };
    }

    private static void Search(string keyname)
    {
        //搜索所有localize和quickLocalize组件
        JdSearchTool.SearchInAssets<GameObject>(path =>
        {
            var texts = JdSearchTool.GetComponentByPath<Text>(path);
            if (texts != null && texts.Length > 0)
            {
                //将所有包含中文的key添加到列表
                foreach (var text in texts)
                {
                    var localize = text.GetComponent<QuickLocalize>();
                    if (localize != null && localize._term.Contains(keyname))
                    {
                        Debug.Log("找到文本："+localize._term+"\t文本所在位置：" + path);
                    }
                }
            }
            return null;
        });
        Debug.Log("查找结束");
    }
}
