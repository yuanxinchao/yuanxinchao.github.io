using System;
using UnityEngine;
using UnityEngine.UI;

public class SwichContentVertical : SwichContent
{

    protected VerticalLayoutGroup _layout;
    public SwichContentVertical(RectTransform content, Button previous, Button next,bool hideBtn = false)
        : base(content, previous, next, hideBtn)
    {
        _layout = content.GetComponent<VerticalLayoutGroup>();
        SkipLength = _view.rect.height;
        if (_layout == null)
            throw new Exception("_content should add component VerticalLayoutGroup");


        if (_scroll != null && _hideBtn)
            _scroll.onValueChanged.AddListener(data =>
            {
                SetBtnState(_content.anchoredPosition.y);
            });

    }
    public SwichContentVertical(RectTransform content)
        : base(content)
    {

        _layout = content.GetComponent<VerticalLayoutGroup>();
        if (_layout == null)
            throw new Exception("_content should add component VerticalLayoutGroup");

    }


    protected override float IndexToPos(int index)
    {
        float relative = GetIndexRelativePos(index);

        float anchorsY = _content.anchorMin.y;
        if (Math.Abs(anchorsY-1) > 0.01f)
            throw new Exception("_content.anchorMin.y should ==1");

        float origin = ContentBeginPos();
        float final = origin - relative;
        return final;
    }

    protected override float IndexToPosMiddle(int index)
    {
        float pos = IndexToPos(index);
        //+item 一半高 -_view 一半高
        pos = pos + _list[index].rect.height/2 - _view.rect.height / 2;
        return pos;
    }

    protected override int PosToIndex(float v)
    {
        return 0;
    }

    public override float ContentEndPos()
    {
        var end = ContentBeginPos() + GetContentLength() - _view.rect.height;
        return end;
    }

    protected override float ContentBeginPos()
    {
        float pivotY = _content.pivot.y;
        float origin = GetContentLength() * (pivotY - 1);
        return origin;
    }

    protected override int CalculateLastIndex()
    {
        float length = 0;
        length += _layout.padding.bottom;
        int i = _list.Count - 1;
        for (; i >= 0; i--)
        {
            length = length + _list[i].rect.height ;
            if(length >= _view.rect.height)
                break;
            length += _layout.spacing;
        }
        return i;
    }

    //相对位置
    protected override float GetIndexRelativePos(int index)
    {
        if (index < 0 || index >= _list.Count)
            throw new Exception("index out of range");

        float pos = 0;
        pos -= _layout.padding.top;
        for (int i = 0; i < _list.Count; i++)
        {
            if (index > i)
                pos -= _list[i].rect.height + _layout.spacing;
        }
        return pos;
    }

    protected override float GetContentLength()
    {
        if (_list.Count == 0)
            GetChildRectChilds();

        Span = _layout.padding.top;
        for (int i = 0; i < _list.Count; i++)
        {
            Span += _list[i].rect.height;
        }
        Span += _layout.spacing * (_list.Count - 1);
        Span += _layout.padding.bottom;
        return Span;
    }

    protected override bool CanScroll()
    {
        return  GetContentLength() > _view.rect.height;
    }

    protected override bool ExceedBeginBound(float pre, float begin)
    {
        return pre <= begin + 1;
    }

    protected override bool ExceedEndBound(float next, float end)
    {
        return next >= end - 1;
    }

    protected override float GetNextPos()
    {
        return _content.anchoredPosition.y + SkipLength;
    }

    protected override float GetPrePos()
    {
        return _content.anchoredPosition.y - SkipLength;
    }

    protected override Vector2 GetPos(float pos)
    {
        return new Vector2(_content.anchoredPosition.x, pos);
    }
}
