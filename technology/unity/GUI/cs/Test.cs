using System;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private RectTransform rect;
    private RectTransform parent;
	// Use this for initialization
	void Start ()
	{
	    rect = GetComponent<RectTransform>();
	    parent = transform.parent.GetComponent<RectTransform>();
        Vector3 leftlow = new Vector2(parent.rect.width * rect.anchorMin.x,parent.rect.height * rect.anchorMin.y);
        Debug.Log("====leftlow"+leftlow);
        Vector3 upright = new Vector2(parent.rect.width * rect.anchorMax.x,parent.rect.height * rect.anchorMax.y);
        Debug.Log("锚点：" + (leftlow + upright) / 2);
        Debug.Log("重心：" + rect.localPosition);
        Vector3 middle = new Vector3( (rect.localPosition.x+(0.5f-rect.pivot.x)*rect.rect.width),(rect.localPosition.y+(0.5f-rect.pivot.y)*rect.rect.height),0);
        Vector3 imgleftlow = new Vector3(rect.localPosition.x - rect.pivot.x * rect.rect.width,rect.localPosition.y-rect.pivot.y*rect.rect.height,0);
        Vector3 imgupright = new Vector3(rect.localPosition.x + (1-rect.pivot.x) * rect.rect.width, rect.localPosition.y + (1-rect.pivot.y) * rect.rect.height, 0);
        Debug.Log("图片中心：" +middle);
        Debug.Log("锚点距重心位置为：" + (rect.localPosition-(leftlow + upright) / 2));
        Debug.Log("锚点距中心位置为：" + (middle-(leftlow + upright) / 2));
        Debug.Log("长宽为:" + rect.rect.width + "," + rect.rect.height);
        Debug.Log("sizeDelta为:" + rect.sizeDelta);
        Debug.Log("sizeDelta为:" + (rect.offsetMax - rect.offsetMin));
        Debug.Log("rect.offsetMin=" + rect.offsetMin);
        Debug.Log("自己计算rect.offsetMin=" +(imgleftlow-leftlow));
        Debug.Log("rect.offsetMax=" + rect.offsetMax);
        Debug.Log("自己计算rect.offsetMax=" + (imgupright-upright));
	    Vector2 diff = (rect.offsetMax + rect.offsetMin)/2;
	    float disx =Convert.ToSingle(rect.sizeDelta.x*(0.5 - rect.pivot.x));
	    float disy =Convert.ToSingle(rect.sizeDelta.y*(0.5 - rect.pivot.y));
        Debug.Log("anchoredPosition=" + rect.anchoredPosition);
        Debug.Log("自己计算anchoredPosition" + (diff.x - disx) + "," + (diff.y - disy));
        Debug.Log("localPosition=" + rect.localPosition);

	}

    void Update()
    {

    }
}

