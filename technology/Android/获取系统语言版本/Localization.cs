using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  Manage TextMap And Query Of Text
 *
 *	by Xuanyi
 *
 */


public class Localization
{
    /* Language Types */
    public const string CHINESE = "Localization/Chinese.json";
    public const string ENGLISH = "Localization/English.json";
    public const string Traditional = "Localization/Traditional.json";
    private string _language;
    public const string LANGUAGE_CHINESE = "CN";
    public const string LANGUAGE_ENGLISH = "EN";
    public const string LANGUAGE_JAPANESE = "JP";
    public const string LANGUAGE_FRENCH = "FR";
    public const string LANGUAGE_GERMAN = "GE";
    public const string LANGUAGE_ITALY = "IT";
    public const string LANGUAGE_KOREA = "KR";
    public const string LANGUAGE_RUSSIA = "RU";
    public const string LANGUAGE_SPANISH = "SP";
    public static string info;

    public string Language
    {
        get
        {
            return _language;
        }
        set
        {
            _language = value;
            TextAsset asset = Resources.Load<TextAsset>(_language);
            Debug.Log("asset.text");
            _languageNode = SimpleJSON.JSON.Parse(asset.text);
        }
    }

    private SimpleJSON.JSONNode _languageNode;

    private Localization ()
    {

        #if UNITY_EDITOR
        Debug.Log("YXC" + "  设置当前语言为英文  ");
        Language = ENGLISH;
        return;
        #endif

        string language = GetLanguageAB(Application.systemLanguage);
        if (language == "CN")
        {
            AndroidJavaObject Context;
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                Context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.tj.Location"))
            {
                if (pluginClass != null)
                {
                    AndroidJavaObject SystemInfo = pluginClass.CallStatic<AndroidJavaObject>("Instance", Context);
                    info = SystemInfo.Call<string>("GetLanguage");
                }
            }
            if (info.Contains("TW"))
            {
                language = "TR";
            }
        }

        if (language == "CN")
        {
            Debug.Log("YXC" + "  设置当前语言为中文简体  ");
            Language = CHINESE;
        } else if (language == "TR")
        {
            Debug.Log("YXC" + "  设置当前语言为中文繁体  ");
            Language = Traditional;
        } else
        {
            Debug.Log("YXC" + "  设置当前语言为英文  ");
            Language = ENGLISH;
        }

    }

    public string GetText (string id)
    {
        return _languageNode [id];
    }

    string GetLanguageAB (SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.Afrikaans:
            case SystemLanguage.Arabic:
            case SystemLanguage.Basque:
            case SystemLanguage.Belarusian:
            case SystemLanguage.Bulgarian:
            case SystemLanguage.Catalan:
                return LANGUAGE_ENGLISH;
            case SystemLanguage.Chinese:
                //      case SystemLanguage.ChineseTraditional:
                //      case SystemLanguage.ChineseSimplified:
                return LANGUAGE_CHINESE;
            case SystemLanguage.Czech:
            case SystemLanguage.Danish:
            case SystemLanguage.Dutch:
            case SystemLanguage.English:
            case SystemLanguage.Estonian:
            case SystemLanguage.Faroese:
            case SystemLanguage.Finnish:
                return LANGUAGE_ENGLISH;
            case SystemLanguage.French:
                return LANGUAGE_FRENCH;
            case SystemLanguage.German:
                return LANGUAGE_GERMAN;
            case SystemLanguage.Greek:
            case SystemLanguage.Hebrew:
            case SystemLanguage.Icelandic:
            case SystemLanguage.Indonesian:
                return LANGUAGE_ENGLISH;
            case SystemLanguage.Italian:
                return LANGUAGE_ITALY;
            case SystemLanguage.Japanese:
                return LANGUAGE_JAPANESE;
            case SystemLanguage.Korean:
                return LANGUAGE_KOREA;
            case SystemLanguage.Latvian:
            case SystemLanguage.Lithuanian:
            case SystemLanguage.Norwegian:
            case SystemLanguage.Polish:
            case SystemLanguage.Portuguese:
            case SystemLanguage.Romanian:
                return LANGUAGE_ENGLISH;
            case SystemLanguage.Russian:
                return LANGUAGE_RUSSIA;
            case SystemLanguage.SerboCroatian:
            case SystemLanguage.Slovak:
            case SystemLanguage.Slovenian:
                return LANGUAGE_ENGLISH;
            case SystemLanguage.Spanish:
                return LANGUAGE_SPANISH;
            case SystemLanguage.Swedish:
            case SystemLanguage.Thai:
            case SystemLanguage.Turkish:
            case SystemLanguage.Ukrainian:
            case SystemLanguage.Vietnamese:
            case SystemLanguage.Unknown:
                return LANGUAGE_ENGLISH;
        }
        return LANGUAGE_CHINESE;
    }
}

