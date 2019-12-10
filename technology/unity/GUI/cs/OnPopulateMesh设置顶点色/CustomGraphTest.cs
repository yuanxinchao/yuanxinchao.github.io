using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGraphTest : MaskableGraphic
{

    private UIVertex[] _quad = new UIVertex[4];//一个
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        // prepare vertices
        vh.Clear();

        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        s_WhiteTexture = (Texture2D)material.mainTexture;

        UIVertex v = UIVertex.simpleVert;
        v.position = new Vector3(0,0,0);
        v.color = new Color(1,0,0,0.5f);
        v.uv0 = new Vector2(0, 0);
        _quad[0] = v;
        v = UIVertex.simpleVert;
        v.position = new Vector3(0, 50, 0);
        v.color = new Color(0, 1, 0,0.5f);
        v.uv0 = new Vector2(0,1);
        _quad[1] = v;
        v = UIVertex.simpleVert;
        v.position = new Vector3(60, 60, 0);
        v.color = new Color(0, 1, 1, 0.5f);
        v.uv0 = new Vector2(1, 1);
        _quad[2] = v;
        v = UIVertex.simpleVert;
        v.position = new Vector3(50, 0, 0);
        v.color = new Color(1, 1, 1, 0.5f);
        v.uv0 = new Vector2(1, 0);
        _quad[3] = v;



        vh.AddUIVertexQuad(_quad);
    }
}
