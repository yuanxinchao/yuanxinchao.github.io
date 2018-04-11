using System.IO;
using UnityEditor;
using UnityEngine;
public class SpecificCheck : EditorWindow
{
    private static string myString = string.Empty;
    private static string note = string.Empty;
    private static string filter =  string.Empty;
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
            filter = myString + " t:texture2D";
            Check();
        }
        note = EditorGUILayout.TextField(note,GUILayout.ExpandHeight(true));
    }

    private static void Check()
    {
        note = string.Empty;
        int count = 0;
        var guids = AssetDatabase.FindAssets(filter);
        if (guids.Length == 0)
            note = "未找到:" + myString;
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            TextureImporter ti = (TextureImporter)AssetImporter.GetAtPath(path);
            string  platformString = "Android";
            if(ti.GetPlatformTextureSettings(platformString).format==TextureImporterFormat.RGBA32)
            {
                note = note + Path.GetFileName(path) + "\n";
                Debug.Log("path=" + path);
            }
            count++;

        }
        note += "查找结束查找了" + count;
    }
}
