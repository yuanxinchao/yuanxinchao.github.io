using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
public class AddUnderLine : BaseMeshEffect
{
    private Text _text;
    private static readonly Regex _inputRegex = new Regex(@">.*?</Color>", RegexOptions.Singleline);
    readonly UIVertex[] m_TempVerts = new UIVertex[4];
    private int _startIndex;
    private int _endIndex;
    private int _fontSize;
    private int _wrapLine;
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        if (_text == null)
        {
            _text = GetComponent<Text>();
        }
        if (_text.fontSize != _fontSize)
        {
            _fontSize = _text.fontSize;
        }
        _wrapLine = 0;
        MatchCollection matchs = _inputRegex.Matches(_text.text);
        if (matchs.Count > 0)
        {
            foreach (Match match in _inputRegex.Matches(_text.text))
            {
                _startIndex = match.Index * 6;
                _endIndex = (match.Value.Length) * 6 + _startIndex - 1;
                _startIndex = _startIndex + 6;//去除 > 这个
            }

            List<UIVertex> vertexList = new List<UIVertex>();
            vh.GetUIVertexStream(vertexList);
            //        ModifyVertices(vertexList);

            vh.Clear();
            vh.AddUIVertexTriangleStream(vertexList);
            SetUnderLine2(vertexList, vh);
        }
    }
    void SetUnderLine2(List<UIVertex> vertexList, VertexHelper vh)
    {
        List<Vector2[]> underLines2 = new List<Vector2[]>();
        int count = vertexList.Count;
        if (count > 0)
        {
            //是否换行
            Vector2 start = vertexList[_startIndex + 4].position;
            Vector2 end;
            float lineY = start.y;
            //            float cHight = vertexList[_startIndex].position.y - vertexList[_startIndex + 4].position.y;//一个字的高度
            int i;
            for (i = _startIndex + 6; i < _endIndex; i++)//从下一个字开始
            {
                //                Debug.Log("i==" + i + vertexList[i].position.y + "lineY" + vertexList[i].position.y);
                if (i % 6 == 4)
                {
                    //                    Debug.Log("hight=" + cHight + "start.y - vertexList[i].position.y:" + (start.y - vertexList[i].position.y));
                    if (start.y - vertexList[i].position.y > _fontSize / 2)//判断是否换行,
                    {
                        _wrapLine++;
                        if (_wrapLine >= 3)//大于4行会报错
                            return;
                        end = new Vector2(vertexList[i - 7].position.x, lineY);//点3的坐标
                        underLines2.Add(new Vector2[2] { start, end });
                        start = vertexList[i].position;//换行后start位置变化
                        lineY = start.y;
                    }
                    if (vertexList[i].position.y < lineY)//下划线取每行中最小的yPos
                    {
                        lineY = vertexList[i].position.y;
                    }
                }
            }
            end = new Vector2(vertexList[_endIndex - 2].position.x, lineY);//点3的坐标
            underLines2.Add(new Vector2[2] { start, end });
            var underLines4 = ConvertToUnderLines4(underLines2);
            //计算下划线的位置
            TextGenerator underlineText = new TextGenerator();
            underlineText.Populate("|", _text.GetGenerationSettings(new Vector2(0, 0)));
            IList<UIVertex> _tUT = underlineText.verts;
            foreach (var line in underLines4)
            {
                //            Debug.Log("_startIndex=" + _startIndex);
                //            Debug.Log("_endIndex=" + _endIndex);
                //绘制下划线
                for (int j = 0; j < 4; j++)
                {
                    m_TempVerts[j] = _tUT[j];
                    m_TempVerts[j].position = line[j];
                    //                    Debug.Log(m_TempVerts[j].position);
                    m_TempVerts[j].color = new Color(1, 153 / 255.0f, 0, 1);
                    if (j == 3)
                    {
                        vh.AddUIVertexQuad(m_TempVerts);
                    }
                }
            }

        }

    }

    private List<Vector3[]> ConvertToUnderLines4(List<Vector2[]> underLines2)
    {
        List<Vector3[]> underLines4 = new List<Vector3[]>(2);
        foreach (Vector2[] v in underLines2)
        {
            Vector3[] _ulPos = new Vector3[4];
            _ulPos[0] = new Vector3(v[0].x, v[1].y, 0);
            _ulPos[1] = new Vector3(v[0].x, v[1].y - _fontSize / 10f, 0);
            _ulPos[2] = new Vector3(v[1].x, v[1].y - _fontSize / 10f, 0);
            _ulPos[3] = new Vector3(v[1].x, v[1].y, 0);
            underLines4.Add(_ulPos);
        }
        return underLines4;
    }
}