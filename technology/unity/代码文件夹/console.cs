using UnityEngine;
using System.Collections;

public class console: MonoBehaviour
{
    static console instance = null;
    static GameObject Obj_console;
	
    int iMetaHeight;
    int iMetaWidth;
    string message;

    public static console Getinstance ()
    {
        if (null == instance)
        {
            Obj_console = new GameObject();
            instance = Obj_console.AddComponent<console>();
        }
        return instance;
    }

    void OnGUI ()
    {


        GUI.skin.textArea.fontSize = Screen.width / 30;
        GUI.skin.textArea.fixedWidth = Screen.width / 3 * 2;

        GUILayout.TextArea(message);
        if (GUI.Button(new Rect(Screen.width / 4 * 3, 0, Screen.width / 4, Screen.height / 10), "Clearlog"))
        {
            message = "";
        }

    }

    public void print (string s)
    {
        message = message + s + "\n";
    }
}
