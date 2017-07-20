using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MaskableGraphic
{

    private static float _pointSize = 3f;//点的大小
    private static float _lineSize = 1f;//线的粗细
   
    private static Color _pointColor = Color.white;//点的颜色
    private static Color _lineColor = Color.yellow;//线的颜色
    private static Color _axisColor = Color.blue;//横纵坐标颜色

    private static Vector2 _origin = new Vector2(-150, -100);//折线图的初始偏移坐标


    private static readonly List<UIVertex[]> _pointList = new List<UIVertex[]>(32);//点的集合
    private static readonly List<UIVertex[]> _lineList = new List<UIVertex[]>(32);//折线的集合

    private static readonly List<UIVertex[]> _xAxis = new List<UIVertex[]>(32);//横坐标
    private static readonly List<UIVertex[]> _yAxis = new List<UIVertex[]>(32);//纵坐标

    private static readonly List<UIVertex[]> _unit = new List<UIVertex[]>(64);//刻度

    private static int _xNum=15;//横坐标数量
    private static int _yNum = 10;//纵坐标数量

    public Font font;
    IList<UIVertex> verts;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (font == null)
            return;
        vh.Clear();
        foreach (var x in _xAxis)
        {
            vh.AddUIVertexQuad(x);
        }
        foreach (var y in _yAxis)
        {
            vh.AddUIVertexQuad(y);
        }
        foreach (var unit in _unit)
        {
            vh.AddUIVertexQuad(unit);
        }

        foreach (var point in _pointList)
        {
            vh.AddUIVertexQuad(point); 
        }
        foreach (var line in _lineList)
        {
            vh.AddUIVertexQuad(line);
        }


  
    }
    //  1-------2
    //  |       |
    //  |       |
    //  0-------3
    public static void AddVertex(Vector2 v2, List<UIVertex[]> pointList,Color color)
    {
        UIVertex[] verts = new UIVertex[4];
        verts[0].position = new Vector3(v2.x - _pointSize/2f, v2.y - _pointSize/2f);
        verts[0].color = color;
        verts[1].position = new Vector3(v2.x - _pointSize/2f, v2.y + _pointSize/2f);
        verts[1].color = color;
        verts[2].position = new Vector3(v2.x + _pointSize/2f, v2.y + _pointSize/2f);
        verts[2].color = color;
        verts[3].position = new Vector3(v2.x + _pointSize/2f, v2.y - _pointSize/2f);
        verts[3].color = color;
        pointList.Add(verts);
    }

    public static void AddLine(Vector2 point1, Vector2 point2, List<UIVertex[]> lineList, Color color)
    {
        //线条粗细设置
        float k=0;
        float h = _lineSize / 2;
        float hx = 0;
        if (Math.Abs(point1.x - point2.x) > 0.1f)
        {
            k = (point1.y - point2.y)/(point1.x - point2.x);
            h = (float) Math.Sqrt((_lineSize/2*k)*(_lineSize/2*k) + (_lineSize/2)*(_lineSize/2));
        }
        else
        {
            h = 0;
            hx = _lineSize / 2;
        }

        //绘制线条四个点
        UIVertex[] verts = new UIVertex[4];
        verts[0].position = new Vector3(point1.x - hx, point1.y - h);
        verts[0].color = color;
        verts[1].position = new Vector3(point1.x + hx, point1.y + h);
        verts[1].color = color;
        verts[2].position = new Vector3(point2.x + hx, point2.y + h);
        verts[2].color = color;
        verts[3].position = new Vector3(point2.x - hx, point2.y - h);
        verts[3].color = color;
        lineList.Add(verts);
    }
    /// <summary>
    /// 画线,xUnit为x轴单位长度,yUnit为y轴单位长度
    /// </summary>
    /// <param name="pointList"></param>
    /// <param name="xUnit">x轴单位长度</param>
    /// <param name="yUnit">y轴单位长度</param>
    public static void Plot(List<Vector2> pointList,float xUnit=1f,float yUnit=1f)
    {
        ClearAll();
        PlotLineAndPoint(pointList, xUnit, yUnit);
    }
    public static void PlotLineAndPoint(List<Vector2> pointList, float xUnit, float yUnit)
    {
        SetAxis(xUnit, yUnit);
        for (int i = 0; i < pointList.Count; i++)
        {
            Vector2 transfer1 = new Vector2(pointList[i].x / xUnit, pointList[i].y / yUnit) + _origin;
            AddVertex(transfer1,_pointList,_pointColor);
            if (i + 1 < pointList.Count)
            {
                Vector2 transfer2 = new Vector2(pointList[i + 1].x / xUnit, pointList[i + 1].y / yUnit) + _origin;
                AddLine(transfer1, transfer2,_lineList,_lineColor);
            }
        }
    }

    public static void SetAxis(float xUnit, float yUnit)
    {

        List<Vector2> xAxisList = new List<Vector2>(32);
        for (int i = 0; i < _xNum; i++)
        {
            xAxisList.Add(new Vector2(i * 20, 0) + _origin);
            if (i > 0)
            {

                AddLine(xAxisList[i - 1], xAxisList[i], _xAxis, _axisColor);

                AddLine(new Vector2(xAxisList[i].x, xAxisList[i].y + 3), xAxisList[i], _unit, _axisColor);//刻度
            }
        }

        List<Vector2> yAxisList = new List<Vector2>(32);
        for (int i = 0; i < _yNum; i++)
        {
            yAxisList.Add(new Vector2(0, i * 20) + _origin);
            if (i > 0)
            {
                AddLine(yAxisList[i], yAxisList[i - 1], _yAxis, _axisColor);

                AddLine(yAxisList[i], new Vector2(yAxisList[i].x +3, yAxisList[i].y), _unit, _axisColor);//刻度
            }
        }
    }

    public static void ClearAll()
    {
        _pointList.Clear();
        _lineList.Clear();
        _xAxis.Clear();
        _yAxis.Clear();
        _unit.Clear();
    }
}
