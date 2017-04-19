using UnityEngine;
public class Debuger
{
    private static bool enableLog = true;

    public static bool EnableLog
    {
        get
        {
            return enableLog;
        }

        set
        {
            enableLog = value;
        }
    }

    static public void Log(object message)
    {
        Log(message,null);
    }
    static public void Log(object message, Object context)
    {
        if (EnableLog)
        {
            Debug.Log("YXC  "+message, context);
        }
    }
}
