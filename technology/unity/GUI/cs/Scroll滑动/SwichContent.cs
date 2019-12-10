using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class SwichContent
{
    protected RectTransform _content;
    protected float Span;//content size fitter 的宽度或长度

    protected bool _hideBtn = true;//是否在滑动尽头隐藏按钮

    protected float SkipLength;
    protected Button _previous;
    protected Button _next;

    public int Index;
    public float MvTime = 0.5f;
    protected List<RectTransform> _list;

    protected RectTransform _view;
    private int _lastIndex;

    protected ScrollRect _scroll;

    private bool _hasButton;
    protected SwichContent(RectTransform content, Button previous, Button next,bool hideBtn)
    {
        _content = content;
        _previous = previous;
        _next = next;
        _hideBtn = hideBtn;

        _list = new List<RectTransform>();

        _hasButton = true;
        _previous.onClick.AddListener(MovePrevious);
        _next.onClick.AddListener(MoveNext);

        if(content.parent == null)
            throw new Exception("content should have a view parent");
        _view = content.parent.GetComponent<RectTransform>();
        if (_view == null)
            throw new Exception("content parent should have a RectTransform component");
        if(_previous == null)
            throw new Exception("_previous btn should not null");
        if(_next == null)
            throw new Exception("_next btn should not null");
        if (_view.parent != null)
        {
            _scroll = _view.parent.GetComponent<ScrollRect>();
        }

    }
    protected SwichContent(RectTransform content)
    {
        _content = content;
        _view = content.parent.GetComponent<RectTransform>();
        if (_view == null)
            throw new Exception("content parent should have a RectTransform component");
        _list = new List<RectTransform>();
        _hasButton = false;
    }

    public virtual void MovePrevious()
    {
        StopScroll();
        FinishTween();

        if (!CanScroll())
            return;
        var prePos = GetPrePos();
        var begin = ContentBeginPos();
        if (ExceedBeginBound(prePos, begin))
        {
            prePos = begin;
        }
        ShowPos(prePos, true);
        SetBtnState(prePos);
    }

    public virtual void MoveNext()
    {
        StopScroll();
        FinishTween();

        if (!CanScroll())
            return;
        var nextPos = GetNextPos();
        var end = ContentEndPos();
        if (ExceedEndBound(nextPos, end))
        {
            nextPos = end;
        }
        ShowPos(nextPos, true);
        SetBtnState(nextPos);
    }

    protected Tween _tween;

    public virtual void Show(int index, bool anim = false)
    {
        Index = index;
        GetChildRectChilds();
        float v = IndexToPos(index);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);//强制刷新一次
        SetPos(v, anim);
    }
    public virtual void ShowIndexToMiddle(int index, bool anim = false)
    {
        Index = index;
        GetChildRectChilds();
        float v = IndexToPosMiddle(index);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);//强制刷新一次
        SetPos(v, anim);
    }
    public virtual void ShowPos(float pos,bool anim = false)
    {
        GetChildRectChilds();
        Index = PosToIndex(pos);
        SetPos(pos, anim);
    }

    protected virtual void SetPos(float pos, bool anim)
    {
        var end = ContentEndPos();
        if (ExceedEndBound(pos, end))
        {
            pos = end;
        }
        var begin = ContentBeginPos();
        if (ExceedBeginBound(pos, begin))
        {
            pos = begin;
        }

        Vector2 v = GetPos(pos);
        if (anim)
        {
            _tween = _content.DOAnchorPos(v, MvTime).SetEase(Ease.OutCubic);
        }
        else
        {
            _content.anchoredPosition = v;
        }
        SetBtnState(pos);
    }

    protected void FinishTween()
    {
        if (_tween != null && _tween.IsPlaying())
            _tween.Complete(true);
    }

    protected virtual void SetBtnState(float pos)
    {
        if (!_hideBtn)
            return;
        if (!_hasButton)
            return;
        var end = ContentEndPos();
        var begin = ContentBeginPos();
        if (ExceedEndBound(pos, end))
        {
            SetNext(false);
        }
        else
        {
            SetNext(true);
        }
        if (ExceedBeginBound(pos, begin))
        {
            SetPre(false);
        }
        else
        {
            SetPre(true);
        }
    }

    protected void SetPre(bool pre)
    {
        _previous.gameObject.SetActive(pre);
    }
    protected void SetNext(bool next)
    {
        _next.gameObject.SetActive(next);
    }
    protected abstract float IndexToPos(int index);
    protected abstract float IndexToPosMiddle(int index);
    protected abstract int PosToIndex(float v);

    public abstract float ContentEndPos();
    protected abstract float ContentBeginPos();
    protected abstract int CalculateLastIndex();
    //相对位置
    protected abstract float GetIndexRelativePos(int index);
    protected abstract float GetContentLength();
    protected abstract bool CanScroll();
    protected abstract bool ExceedBeginBound(float pre,float begin);
    protected abstract bool ExceedEndBound(float next,float end);
    protected abstract float GetNextPos();
    protected abstract float GetPrePos();
    protected abstract Vector2 GetPos(float pos);
    protected void StopScroll()
    {
        if(_scroll!=null)
            _scroll.StopMovement();
    }

    protected void GetChildRectChilds()
    {
        _list.Clear();
        for (int i = 0; i < _content.childCount; i++)
        {
            var rect = _content.GetChild(i) as RectTransform;
            if (rect == null || !rect.gameObject.activeSelf)
                continue;

            _list.Add(rect);
        }
    }
}
