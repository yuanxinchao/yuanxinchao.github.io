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
        _list = new List<RectTransform>();
        _hasButton = false;
    }

    public virtual void MovePrevious()
    {
        StopScroll();
        FinishTween();
    }

    public virtual void MoveNext()
    {
        StopScroll();
        FinishTween();
    }

    private Tween _tween;
    public virtual void Show(int index,bool anim = false)
    {
        Index = index;
        GetChildRectChilds();
        Vector2 v = IndexToPos(index);
        SetPos(v, anim);
    }
    public virtual void ShowPos(Vector2 v,bool anim = false)
    {
        GetChildRectChilds();
        Index = PosToIndex(v);
        SetPos(v, anim);
    }

    private void SetPos(Vector2 v, bool anim)
    {
        if (anim)
        {
            _tween = _content.DOAnchorPos(v, MvTime).SetEase(Ease.OutCubic);
        }
        else
        {
            _content.anchoredPosition = v;
            SetBtnState(v.y);
        }
    }

    protected void FinishTween()
    {
        if (_tween != null && _tween.IsPlaying())
            _tween.Complete(true);
    }

    protected abstract void SetBtnState(float refPos);
    protected void SetButtonState(bool pre, bool next)
    {
        SetPre(pre);
        SetNext(next);
    }

    protected void SetPre(bool pre)
    {
        if (!_hideBtn)
            return;
        if (!_hasButton)
            return;
        _previous.gameObject.SetActive(pre);
    }
    protected void SetNext(bool next)
    {
        if (!_hideBtn)
            return;
        if (!_hasButton)
            return;
        _next.gameObject.SetActive(next);
    }
    protected abstract Vector2 IndexToPos(int index);
    protected abstract int PosToIndex(Vector2 v);

    protected abstract float ContentEndPos();
    protected abstract float ContentBeginPos();
    protected abstract int CalculateLastIndex();
    //相对位置
    protected abstract float GetIndexRelativePos(int index);
    protected abstract float GetContentLength();
    protected abstract bool CanScroll();
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
