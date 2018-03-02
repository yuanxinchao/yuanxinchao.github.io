using System;
using Assets.Explain;
using UnityEngine;

public class Test : MonoBehaviour
{
    private string str1 = "(def x (+ 4 5))";
    private string str2 = "(if (> 9 8) (* (+ (* (+ x 6) 4) 100) 38) 110)";
	// Use this for initialization
	void Start ()
	{
        System.Diagnostics.Stopwatch gameDuration = new System.Diagnostics.Stopwatch(); ;

        SScope env = new SScope(null);
	    env.Init();
        gameDuration.Start();
        Debug.Log(env.Evaluate(str1));
        Debug.Log(env.Evaluate(str2));

        gameDuration.Stop();
        Debug.Log("计算所需时长为：" + gameDuration.Elapsed.TotalMilliseconds);
	}
}

