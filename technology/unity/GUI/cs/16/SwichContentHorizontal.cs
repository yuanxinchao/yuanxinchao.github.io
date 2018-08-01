using System;
using UnityEngine;
using UnityEngine.UI;

public class SwichContentHorizontal : SwichContent
{

    protected HorizontalLayoutGroup _layout;
    public SwichContentHorizontal(RectTransform content, Button previous, Button next,bool hideBtn = false)
        : base(content, previous, next, hideBtn)
    {
        _layout = content.GetComponent<HorizontalLayoutGroup>();
        SkipLength = _view.rect.width;
        if (_layout == null)
            throw new Exception("_content should add component LayoutGroup");


        if (_scroll != null && _hideBtn)
            _scroll.onValueChanged.AddListener(data =>
            {
                SetBtnState(_content.anchoredPosition.x);
            });
    }    
    public SwichContentHorizontal(RectTransform content)
        : base(content)
    {

        _layout = content.GetComponent<HorizontalLayoutGroup>();
        if (_layout == null)
            throw new Exception("_content should add component LayoutGroup");

    }


    public override void MovePrevious()
    {
        base.MovePrevious();
        if (!CanScroll())
            return;

        var prePos = _content.anchoredPosition.x + SkipLength;
        var origin = ContentBeginPos();
        float aimX = 0;
        if (prePos >= origin - 1)
        {
            aimX = origin;
        }
        else
        {
            aimX = prePos;
        }

        ShowPos(new Vector2(aimX, _content.anchoredPosition.y), true);
        SetBtnState(aimX);
    }

    public override void MoveNext()
    {       
        base.MoveNext();
        if(!CanScroll())
            return;

        var nextPos = _content.anchoredPosition.x - SkipLength;
        var end = ContentEndPos();
        float aimX = 0;
        if (nextPos <= end + 1)
        {
            aimX = end;
            ShowPos(new Vector2(end, _content.anchoredPosition.y), true);
        }
        else
        {
            aimX = nextPos;
        }

        ShowPos(new Vector2(aimX, _content.anchoredPosition.y), true);
        SetBtnState(aimX);
    }

    public override void Show(int index, bool anim = false)
    {
        base.Show(index,anim);
    }


    protected override void SetBtnState(float pos)
    {
        var end = ContentEndPos();
        var origin = ContentBeginPos();
        if (pos <= end + 1)
        {
            SetNext(false);
        }
        else
        {
            SetNext(true);
        }
        if (pos >= origin - 1)
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

        float anchorsX = _content.anchorMin.x;
        if(Math.Abs(anchorsX) > 0.01f)
            throw new Exception("_content.anchorMin.x should ==0");

        float origin = ContentBeginPos();
        float final = origin - relative;
        return new Vector2(final, _content.anchoredPosition.y);
    }

    protected override int PosToIndex(Vector2 v)
    {
        return 0;
    }


    protected override float ContentEndPos()
    {
        var end = ContentBeginPos() - GetContentLength() + _view.rect.width;
        return end;
    }

    protected override float ContentBeginPos()
    {
        float pivotX = _content.pivot.x;
        float origin = GetContentLength() * pivotX;
        return origin;
    }

    protected override int CalculateLastIndex()
    {
        float length = 0;
        length += _layout.padding.right;
        int i = _list.Count - 1;
        for (; i >= 0; i--)
        {
            length = length + _list[i].rect.width;
            if (length >= _view.rect.height)
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
        pos += _layout.padding.left;
        for (int i = 0; i < _list.Count; i++)
        {
            if (index > i)
                pos += _list[i].rect.width + _layout.spacing;
        }
        return pos;
    }
    //这里Span = _content.rect.width，content size fitter 刷新不及时所以自己计算
    protected override float GetContentLength()
    {
        if (_list.Count == 0)
            GetChildRectChilds();

        Span = _layout.padding.left;
        for (int i = 0; i < _list.Count; i++)
        {
            Span += _list[i].rect.width;
        }
        Span += _layout.spacing * (_list.Count - 1);
        Span += _layout.padding.right;
        return Span;
    }

    protected override bool CanScroll()
    {
        return GetContentLength() > _view.rect.width;
    }
}
