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


    public override void MovePrevious()
    {
        base.MovePrevious();
        if (!CanScroll())
            return;

        var prePos = _content.anchoredPosition.y - SkipLength;
        var origin = ContentBeginPos();
        float aimY = 0;
        if (prePos <= origin + 1)
        {
            aimY = origin;
        }
        else
        {
            aimY = prePos;
        }
        ShowPos(new Vector2(_content.anchoredPosition.x, aimY), true);
        SetBtnState(aimY);
    }

    public override void MoveNext()
    {
        base.MoveNext();
        if (!CanScroll())
            return;

        var nextPos = _content.anchoredPosition.y + SkipLength;
        var end = ContentEndPos();
        float aimY = 0;
        if (nextPos >= end - 1)
        {
            aimY = end;
        }
        else
        {
            aimY = nextPos;
        }

        ShowPos(new Vector2(_content.anchoredPosition.x, aimY), true);
        SetBtnState(aimY);
    }

    public override void Show(int index, bool anim = false)
    {
        base.Show(index,anim);
    }

    protected override void SetBtnState(float pos)
    {
        var end = ContentEndPos();
        var origin = ContentBeginPos();
        if (pos >= end - 1)
        {
            SetNext(false);
        }
        else
        {
            SetNext(true);
        }
        if (pos <= origin + 1)
        {
            SetPre(false);
        }
        else
        {
            SetPre(true);
        }
    }

    protected override Vector2 IndexToPos(int index)
    {
        float relative = GetIndexRelativePos(index);

        float anchorsY = _content.anchorMin.y;
        if (Math.Abs(anchorsY-1) > 0.01f)
            throw new Exception("_content.anchorMin.y should ==1");

        float origin = ContentBeginPos();
        float final = origin - relative;
        return new Vector2(_content.anchoredPosition.x, final);
    }

    protected override int PosToIndex(Vector2 v)
    {
        return 0;
    }

    protected override float ContentEndPos()
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
        pos += _layout.padding.top;
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
}
