using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class SpecificCheck : EditorWindow
{
    private static string myString = string.Empty;
    private static string note = string.Empty;
    private static string filter =  string.Empty;
    private static string test = string.Empty;
    [MenuItem("Tools/I2 Localization/检查特定pfb翻译")]
    public static void CheckPfbWin()
    {
        SpecificCheck window = (SpecificCheck)GetWindow(typeof(SpecificCheck));
        
    }
    void OnGUI()
    {
        myString = EditorGUILayout.TextField("输入pfb名称", myString);
        if (GUILayout.Button("查找"))
        {
            filter = myString + " t:GameObject";
            Check();
        }
        note = EditorGUILayout.TextField(note,GUILayout.ExpandHeight(true));
    }

    private static void Check()
    {
        note = string.Empty;
        var guids = AssetDatabase.FindAssets(filter);
        if (guids.Length == 0)
            note = "未找到pfb:" + myString;
        for (int i = 0; i < guids.Length; i++)
        {
            //            Debug.Log("guid=" + guids[i]);
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            Debug.Log("path=" + path);
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Text[] t = obj.GetComponentsInChildren<Text>(true);
            for (int j = 0; j < t.Length; j++)
            {
                if (t[j] != null)
                {
                    Debug.Log("检查：" + t[j].text);
                    QuickLocalize quick = t[j].GetComponent<QuickLocalize>();
                    if (quick == null)
                    {
                        note +=t[j].name+":"+ t[j].text + "\n";
                    }
                }
            }

        }
        note += "查找结束";
    }
}
