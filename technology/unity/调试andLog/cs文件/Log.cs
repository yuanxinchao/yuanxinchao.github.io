using System;
using UnityEngine;

public class Log: MonoBehaviour
{
    static Log instance = null;
    static GameObject Obj_console;
	
    int iMetaHeight;
    int iMetaWidth;
    string message = string.Empty;
    public Action _yesCallback;
    public static Log Inst()
    {
        if (null == instance)
        {
            Obj_console = new GameObject {name = "Log"};
            instance = Obj_console.AddComponent<Log>();
        }
        return instance;
    }

    void OnGUI ()
    {
        GUI.skin.textArea.fontSize = Screen.width / 20;
        GUI.skin.textArea.fixedWidth = Screen.width / 6 * 5;
        GUILayout.TextArea(message);

        if (GUI.Button(new Rect(Screen.width / 6*5, 0, Screen.width / 6, Screen.height / 20), "Ok"))
        {
            message = "";
            Close();
            Destroy(this);
        }
    }

    private void Close()
    {
        if (_yesCallback!=null)
            _yesCallback();
    }
    public void Print (string s)
    {
        message = message + s + "\n";
    }

    public Log SetYesCallback(Action callback)
    {
        _yesCallback = callback;
        return this;
    }
}
