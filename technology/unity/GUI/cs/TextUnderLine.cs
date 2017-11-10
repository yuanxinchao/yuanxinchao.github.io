using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 文本控件,匹配坐标,可以添加回调
/// </summary>
[AddComponentMenu("UI/TextUnderLine", 10)]
public class TextUnderLine : Text, IPointerClickHandler
{
    /// <summary>
    /// 解析完最终的文本
    /// </summary>
    private string m_OutputText;

    /// <summary>
    /// 坐标信息列表
    /// </summary>
    private readonly List<CoordInfo> m_CoordInfos = new List<CoordInfo>();

    /// <summary>
    /// 文本构造器
    /// </summary>
    protected static readonly StringBuilder s_TextBuilder = new StringBuilder();

    ///坐标正则(,.，、)
    /// </summary>
    private static readonly Regex s_CoordRegex =
        new Regex(@"<color=([^>\n\s]+)>(.*?)(</color>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    
    /// <summary>
    /// 获取颜色
    /// </summary>
//    private static readonly Regex s_ColorRegex =
//        new Regex(@"<color=([^>\n\s]+)>(.*?)(</color>)", RegexOptions.Singleline);
    /*
    /// <summary>
    /// 带颜色的坐标
    /// </summary>
    private static readonly Regex s_ColorCoordRegex =
        new Regex(@"<color=([^>\n\s]+)>.*?\[?(\d{1,3})[,\.\uff0c\u3001](\d{1,3})\]?.*?</color>", RegexOptions.Singleline);
    */

    public Action<Vector2> _CoordCallback;

    public override void SetVerticesDirty()
    {
        base.SetVerticesDirty();
        m_OutputText = GetOutputText(text);
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        var orignText = m_Text;
        m_Text = m_OutputText;
        base.OnPopulateMesh(toFill);
        m_Text = orignText;

        UIVertex vert = new UIVertex();
        Vector3 refPos;
        // 处理坐标文本包围框
        foreach (var coordInfo in m_CoordInfos)
        {
            coordInfo.boxes.Clear();
            if (coordInfo.startIndex >= toFill.currentVertCount)
                continue;
        
            // 将坐标文本顶点索引坐标加入到包围框
            toFill.PopulateUIVertex(ref vert, coordInfo.startIndex);
            var pos = vert.position;
            refPos = pos;
           
            var bounds = new Bounds(pos, Vector3.zero);

            for (int i = coordInfo.startIndex, m = coordInfo.endIndex; i < m; i++)
            {

                if (i >= toFill.currentVertCount)
                {
                    break;
                }
                toFill.PopulateUIVertex(ref vert, i);
                pos = vert.position;
                if (i % 4 == 0)//判断是否换行
                {
                    UIVertex refVert = new UIVertex();
                    toFill.PopulateUIVertex(ref refVert, i + 3);
//                    Debug.Log("refVert.position.y=" + refVert.position.y + "bounds.min.y=" + bounds.min.y + "  fontSize/2=" + fontSize / 2f);
                    if (Math.Abs(refVert.position.y - bounds.min.y) > fontSize / 2f)
                    {
                        coordInfo.boxes.Add(new Rect(bounds.min, bounds.size));
                        bounds = new Bounds(pos, Vector3.zero);
                    }
                }
                bounds.Encapsulate(pos); // 扩展包围框
            }
            coordInfo.boxes.Add(new Rect(bounds.min, bounds.size));
        }

        TextGenerator _UnderlineText = new TextGenerator();
        var settings = GetGenerationSettings(Vector2.zero);
        settings.fontSize *= 2; //提高下划线的分辨率
        _UnderlineText.Populate("_", settings);
        IList<UIVertex> _TUT = _UnderlineText.verts;

        foreach (var coordInfo in m_CoordInfos)
        {
            if (coordInfo.startIndex >= toFill.currentVertCount)
                continue;

            for (int i = 0; i < coordInfo.boxes.Count; i++)
            {
                Vector3 _StartBoxPos = new Vector3(coordInfo.boxes[i].x, coordInfo.boxes[i].y, 0.0f) - new Vector3(0, fontSize * 0.05f, 0);
                Vector3 _EndBoxPos = _StartBoxPos + new Vector3(coordInfo.boxes[i].width, 0.0f, 0.0f);
                AddUnderlineQuad(toFill, _TUT, _StartBoxPos, _EndBoxPos,coordInfo.color);
            }
        }
    }


    readonly UIVertex[] m_TempVerts = new UIVertex[4];

    /// <summary>
    /// 添加下划线
    /// </summary>
    private void AddUnderlineQuad(VertexHelper _VToFill, IList<UIVertex> _VTUT, Vector3 _VStartPos, Vector3 _VEndPos,string _color)
    {
        Color c;
        ColorUtility.TryParseHtmlString(_color, out c);
        Vector3[] _TUnderlinePos = new Vector3[4];
        _TUnderlinePos[0] = _VStartPos;
        _TUnderlinePos[1] = _VEndPos;
        _TUnderlinePos[2] = _VEndPos + new Vector3(0, fontSize * 0.2f, 0);
        _TUnderlinePos[3] = _VStartPos + new Vector3(0, fontSize * 0.2f, 0);

        for (int i = 0; i < 4; ++i)
        {
            int tempVertsIndex = i & 3;
            m_TempVerts[tempVertsIndex] = _VTUT[i % 4];
            m_TempVerts[tempVertsIndex].color = c; ;
            m_TempVerts[tempVertsIndex].position = _TUnderlinePos[i];

            if (tempVertsIndex == 3)
                _VToFill.AddUIVertexQuad(m_TempVerts);
        }
    }

    /// <summary>
    /// 获取超链接解析后的最后输出文本
    /// </summary>
    /// <returns></returns>
    protected virtual string GetOutputText(string outputText)
    {
        s_TextBuilder.Length = 0;
        m_CoordInfos.Clear();
        var indexText = 0;
        foreach (Match match in s_CoordRegex.Matches(outputText))
        {
            //            var x = Convert.ToInt32(match.Groups[1].Value);
            //            var y = Convert.ToInt32(match.Groups[2].Value);
            //            if (x >= 500 || y >= 500)
            //                continue;

            s_TextBuilder.Append(outputText.Substring(indexText, match.Index - indexText));
            //类似于<i>的顶点的数据会在列表中,但是不会被渲染
            //因此这里记录顶点的时候不必考虑richtext格式的问题
            var coordinfo = new CoordInfo
            {

                startIndex = s_TextBuilder.Length * 4,
                endIndex = (s_TextBuilder.Length + match.Groups[0].Length) * 4 - 1,
                color = match.Groups[1].Value,
               
                //                coord = new Vector2(x, y),
            };
            m_CoordInfos.Add(coordinfo);
            s_TextBuilder.Append(match.Groups[0].Value);
            indexText = match.Index + match.Length;
        }
        s_TextBuilder.Append(outputText.Substring(indexText, outputText.Length - indexText));
        return s_TextBuilder.ToString();
    }

    /// <summary>
    /// 点击事件检测是否点击到坐标文本
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 lp;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out lp);

        foreach (var coordInfo in m_CoordInfos)
        {
            var boxes = coordInfo.boxes;
            for (var i = 0; i < boxes.Count; ++i)
            {
                if (boxes[i].Contains(lp))
                {
                    if (_CoordCallback != null)
                        _CoordCallback(coordInfo.coord);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 坐标信息类
    /// </summary>
    private class CoordInfo
    {
        public int startIndex;

        public int endIndex;

        public Vector2 coord;

        public readonly List<Rect> boxes = new List<Rect>();

        public string color;
    }
}
