
using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorWindowTest : EditorWindow
{

    private bool WantsMouseMove = false;

    [MenuItem("Example/Window postion")]  
    static void Init ()
    {  
        EditorWindowTest window = (EditorWindowTest)EditorWindow.GetWindow(typeof(EditorWindowTest));  
        window.position = new Rect(100, 100, 300, 300);  // 窗口的坐标  
    }

    void OnGUI ()
    {  
        GUILayout.Space(20);  
        if (GUILayout.Button("ClearSave"))  //在窗口上创建一个按钮  
        {  
            GameData.NeedLoadGame = false;
            Debug.Log("ClearSave");  
            Tutorial.Tutor = true;
        }  
    }
}  