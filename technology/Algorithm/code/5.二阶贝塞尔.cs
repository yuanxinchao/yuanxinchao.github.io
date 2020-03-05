using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    //路径上总共放多少个点(算上开头和结尾)最少两个
    public static Vector3[] GetPathByBezier2(Vector3 startPos, Vector3 controlPos, Vector3 endPos, int pointCount)
    {
        Vector3[] path = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            path[i] = Bezier2(startPos, controlPos, endPos, t);
        }
        return path;
    }
    public static Vector3 Bezier2(Vector3 startPos, Vector3 controlPos, Vector3 endPos, float t)
    {
        return (1 - t) * (1 - t) * startPos + 2 * t * (1 - t) * controlPos + t * t * endPos;
    }

}
