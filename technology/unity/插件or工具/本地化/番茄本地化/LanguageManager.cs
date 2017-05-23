using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;


public class LanguageManager : MonoBehaviour
{
    public static Dictionary<string, string> LanguageDictionary = new Dictionary<string, string>();
    public static string[] Languages = null;
    public static bool isInit = false;
    public static string LanguageKey
    {
        get { return PlayerPrefs.GetString("LanguageKey", DefaultLanguage); }
        set { PlayerPrefs.SetString("LanguageKey", value); }
    }

    public static List<LanguagePictureItem> LPItems = new List<LanguagePictureItem>();

    public TextAsset[] languageFiles;

    public static string DefaultLanguage
    {
        get
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Arabic:
                    return "阿拉伯语";
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "简体中文";
                case SystemLanguage.ChineseTraditional:
                    return "繁体中文";
                default:
                    return "英文";
            }
        }
    }
    void Start()
    {
        Init(languageFiles);
    }
    public static void Init(TextAsset[] files)
    {
        if (isInit) return;
        isInit = true;


        foreach (var flie in files)
        {
            string[] datas = flie.text.Trim().Split('\n');
            bool head = true;
            foreach (var data in datas)
            {
                string[] strs = data.Trim().Split((char)9);
                if (head)
                {
                    head = false;
                    Languages = strs;
                }
                else
                {
                    for (int i = 1; i < Languages.Length; i++)
                    {
                        LanguageDictionary[Languages[i] + "|" + strs[0].Replace("\\n", "\n")] = strs[i].Trim('"').Replace("\\n", "\n");
                    }
                }
            }
        }
        //foreach (var item in LanguageDictionary)
        //{
        //    Debug.Log(item.Key + "     " + item.Value);
        //}
    }

    public static string Get(string k)
    {
        if (string.IsNullOrEmpty(k)) return k;
        k = k.Trim('\r');
        string s = null;
        if (LanguageDictionary.TryGetValue(LanguageKey + "|" + k, out s))
            return s;
        if (LanguageKey != Languages[0])
            Debug.Log(LanguageKey + "|" + k + "       no find");
        return k;
    }

    public static void Select(int id)
    {
        LanguageKey = Languages[id];
        RefreshUI();
    }

    public static int LanguageId
    {
        get
        {
            for (int i = 0; i < Languages.Length; i++)
            {
                if (LanguageKey == Languages[i])
                    return i;
            }
            return 0;
        }
    }

    public static void RefreshUI()
    {
        foreach (var item in LPItems)
        {
            item.RefreshUI();
        }
    }
}