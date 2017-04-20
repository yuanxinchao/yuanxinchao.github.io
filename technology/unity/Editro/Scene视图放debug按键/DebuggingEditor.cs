using UnityEditor;
using UnityEngine;

//自定义Tset脚本
[CustomEditor(typeof(DebuggingGui))] 
//请继承Editor
public class DebuggingEditor : Editor
{

    void OnSceneGUI ()
    {
        //得到test脚本的对象
        DebuggingGui test = (DebuggingGui)target;

//        //绘制文本框
//        Handles.Label(test.transform.position + Vector3.up * 2,
//            test.transform.name + " : " + test.transform.position.ToString());

        //开始绘制GUI
        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical("box");

        if (GUILayout.Button("死掉", GUILayout.Width(Screen.width / 10), GUILayout.Height(Screen.height / 20)))
        {
            Debug.Log("死掉");
            if (ReviveView.NowReviveTimes < ReviveView.AllReviveTimes && TjSdk.IsIncentivizedAvailable())
            {
                GameManage.instance.SetLoadFalse();
                //                    Debug.Log("YXC" + "  可以看视频复活，展示看视频复活弹框  ");
                GameManage.instance.ShowDieNote();
            } else
            {
                //                    Debug.Log("YXC" + "  不可复活 视频＝  " + TjSdk.IsIncentivizedAvailable());
                ReviveView.NowReviveTimes = 0;
                GameManage.instance.DiePrompt();
            }
        }
       
        GUILayout.BeginVertical();
        GUILayout.EndArea();

        Handles.EndGUI();
    }

}
