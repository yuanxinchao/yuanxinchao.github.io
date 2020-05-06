using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class ListReuse3<TItem> where TItem:ListItem
{
    private static RectOffset DefaultPadding = new RectOffset();

    //创建一个可以复用的list
    public static ListReuse3<TItem> Create(Transform listRootTransform, 
        Action<ListItem, int> refreshIndex,
        ScrollDirectionEnum scrollDirection,
        float spacing = 10.0f,
        float jumpToTs =0.3f,
        RectOffset padding = null)
    {

        var scroll = listRootTransform.GetComponent<ScrollRect>();

        if (!scroll)
            throw new Exception("list scrollRect is null! root gameobject: " + listRootTransform.gameObject.name);

        //总是认为ScrollView的Content在root->child0->child0位置
        var content = listRootTransform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        if (!content)
            throw new Exception("list content is null! root gameobject: " + listRootTransform.gameObject.name);

        //listItem模板对象，认为其总是ScrollView Content下的第一个子对象
        var itemModel = content.GetChild(0).gameObject;

        if (!itemModel)
            throw new Exception("list item model is null! root gameobject: " + listRootTransform.gameObject.name);

        if(refreshIndex == null)
            throw new Exception("refreshIndex should not null! root gameobject: " + listRootTransform.gameObject.name);

        if (content.GetComponent<LayoutGroup>() != null)
            throw new Exception("content needn't LayoutGroup");
        if (content.GetComponent<ContentSizeFitter>() != null)
            throw new Exception("content needn't ContentSizeFitter");

        ListReuse3<TItem> list = new ListReuse3<TItem>(scroll, content,itemModel, refreshIndex,scrollDirection,spacing,jumpToTs,padding);
        list._list = ListView.Create<TItem>(scroll, content, itemModel);
        return list;
    }


    private ScrollRect _scrollRect;
    private RectTransform _content;
    //垂直取高度 水平取宽度
    private float _viewsize;
    private float _itemsize;
    private ScrollDirectionEnum _scrollDirection;
    private Action<ListItem, int> _refreshIndex;
    private float _spacing;
    private RectOffset _padding;
    private ListView _list;
    private int _dataCount;
    private float _jumpToTs;
    private float _scrollPosition
    {
        get
        {
            if (_scrollDirection == ScrollDirectionEnum.Horizontal)
                return _content.anchoredPosition.x;
            else
            {
                return _content.anchoredPosition.y;
            }

        }
    }

    //refreshAc 回调为刷新某个index的item信息
    private ListReuse3(ScrollRect scroll,
        RectTransform content,
        GameObject model,
        Action<ListItem, int> refreshIndex,
        ScrollDirectionEnum scrollDirection,
        float spacing,
        float jumpToTs,
        RectOffset padding = null)
    {
        if (padding == null)
            padding = DefaultPadding;
 

        _scrollRect = scroll;
        _content = content;
        _scrollDirection = scrollDirection;
        _refreshIndex = refreshIndex;
        _spacing = spacing;
        _padding = padding;
        _jumpToTs = jumpToTs;
        var viewport = content.parent.GetComponent<RectTransform>();
        if (scrollDirection == ScrollDirectionEnum.Vertical)
        {
            _viewsize = viewport.rect.height;
            _itemsize = model.GetComponent<RectTransform>().rect.height;
        }
        else
        {
            _viewsize = viewport.rect.width;
            _itemsize = model.GetComponent<RectTransform>().rect.width;
        }

        if (scrollDirection == ScrollDirectionEnum.Vertical)
        {
            if (Math.Abs(content.anchorMax.y - 1) > 0.1f || Math.Abs(content.anchorMin.y - 1) > 0.1f)
                throw new Exception("content.anchor.min & max .y should =1");
            if (Math.Abs(content.pivot.y - 1) > 0.1f)
                throw new Exception("content.pivot.y should =1");
//            if (Math.Abs(viewport.pivot.y - 1) > 0.1f)
//                throw new Exception("viewport.pivot.y should =1");

            content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0);
        }
        if (scrollDirection == ScrollDirectionEnum.Horizontal)
        {
            if (Math.Abs(content.anchorMax.x) > 0.1f || Math.Abs(content.anchorMin.x) > 0.1f)
                throw new Exception("content.anchor.min & max .x should =0");
            if (Math.Abs(content.pivot.x) > 0.1f)
                throw new Exception("content.pivot.x should =0");
//            if (Math.Abs(viewport.pivot.x) > 0.1f)
//                throw new Exception("content.pivot.x should =0");

            content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0);
        }


        _scrollRect.onValueChanged.AddListener(ScrollValueChange);
    }

    //可见区域
    private int _startIndex;
    private int _endIndex = -1;

    private void ScrollValueChange(Vector2 arg0)
    {
        SetVisibleItemInfo();
    }
    //获取可见区域的起止index
    private void CalculateCurrentActiveCellRange(out int startIndex, out int endIndex)
    {
        // get the positions of the scroller
        float startPosition;
        float endPosition;

        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            startPosition = _scrollPosition;
            endPosition = _scrollPosition + _viewsize;
        }
        else
        {
            startPosition = -_scrollPosition;
            endPosition = -_scrollPosition + _viewsize;
        }

        // calculate each index based on the positions
        startIndex = GetCellIndexAtPosition(startPosition);
        endIndex = GetCellIndexAtPosition(endPosition);
    }
    private float GetCellPosByIndex(int index)
    {
        float pos;
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            var before = _padding.top;
            //计算的下边缘
            pos =-((index + 1) * _itemsize + index * _spacing + before);
            //变成上边缘
            pos += _itemsize;
        }
        else
        {
            var before = _padding.left;
            pos = (index + 1) * _itemsize + index * _spacing + before;
            //边冲左边缘
            pos -= _itemsize;
        }
        return pos;
    }
    //某个positon 的index
    private int GetCellIndexAtPosition(float pos)
    {
        //落在间隙暂定算下一个格子
        float before;
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
            before = _padding.top;
        else
        {
            before = _padding.left;
        }

        return Mathf.FloorToInt((pos - before + _spacing) / (_itemsize + _spacing));
    }
    //设置可见区域信息
    private void SetVisibleItemInfo()
    {
        int newStartIndex;
        int newEndIndex;
        CalculateCurrentActiveCellRange(out newStartIndex, out newEndIndex);
        newEndIndex = Mathf.Min(_dataCount-1, newEndIndex);
        newStartIndex = Mathf.Max(0, newStartIndex);
        int i;
        //多余的格子回收
        for (i = _startIndex; i < Mathf.Min(_endIndex + 1,newStartIndex); i++)
        {
            _list.RemoveBegin();
        }

        for (i = _endIndex; i > Mathf.Max(_startIndex -1, newEndIndex); i--)
        {
            _list.RemoveEnd();
        }


        if (newStartIndex > _endIndex || newEndIndex < _startIndex)
        {
            for (i = newStartIndex; i <= newEndIndex; i++)
            {
                AddIndexItem(i,true);
            }
        }
        else
        {
            //不足的格子新增
            for (i = _startIndex - 1; i >= newStartIndex; i--)
            {
                AddIndexItem(i,false);
            }
            for (i = _endIndex + 1; i <= newEndIndex; i++)
            {
                AddIndexItem(i,true);
            }
        }

        _startIndex = newStartIndex;
        _endIndex = newEndIndex;
    }
    
    private void RefreshVisibleItemInfo()
    {
        var shown = _list._listItems;
        for (int i = 0; i < shown.Count; i++)
        {
            _refreshIndex(shown[i], i + _startIndex);
        }
    }
    //数据数量
    public void Refresh(int dataCount)
    {
        _dataCount = dataCount;
        SetContentSize();
        ClapContentPos();
        SetVisibleItemInfo();
        RefreshVisibleItemInfo();
    }
    //总数据量 跳到索引 动画
    public void RefreshAndJumpTo(int dataCount,int index,bool anim = true,bool middle = false)
    {
        Refresh(dataCount);
        JumpTo(index,anim,middle);
    }

    private void JumpTo(float from,float to,bool anim)
    {
        _scrollRect.StopMovement();
        if (!anim)
        {
            if (_scrollDirection == ScrollDirectionEnum.Vertical)
            {
                _content.anchoredPosition = new Vector2(_content.anchoredPosition.x,to);
            }
            else
            {
                _content.anchoredPosition = new Vector2(to,_content.anchoredPosition.y);
            }
        }
        else
        {
            if (_scrollDirection == ScrollDirectionEnum.Vertical)
            {
                _content.anchoredPosition = new Vector2(_content.anchoredPosition.x,from);
                _content.DOAnchorPosY(to, _jumpToTs);
            }
            else
            {
                _content.anchoredPosition = new Vector2(from,_content.anchoredPosition.y);
                _content.DOAnchorPosX(to, _jumpToTs);
            }
        }
    }
    private void JumpTo(float to,bool anim)
    {
        JumpTo(GetScrollCurrnetPos(),to,anim);
    }
    private void JumpTo(int index,bool anim,bool middle)
    {
        JumpTo(GetScrollPos(index,middle),anim);
    }

    private float GetScrollPos(int index,bool middle = false)
    { 
        float pos = GetCellPosByIndex(index);
        //content pos 和index的本地坐标相反
        pos = -pos;
        if (middle)
        {
            if (_scrollDirection == ScrollDirectionEnum.Vertical)
            {
                pos = pos - _viewsize / 2 + _itemsize / 2;
            }
            else
            {
                pos = pos + _viewsize / 2 - _itemsize / 2;
            }
        }
        return GetClapedAnchorPos(pos);
    }

    public void MoveView(int i)
    {
        var to = GetClapedAnchorPos(GetScrollCurrnetPos() + _viewsize* i);
        JumpTo(to,true);
    }
    private float GetScrollCurrnetPos()
    {
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
           return _content.anchoredPosition.y;
        }
        else
        {
            return _content.anchoredPosition.x;
        }

    }
    public void SetBeginAndRefresh(int dataCount)
    {
        _scrollRect.verticalNormalizedPosition = 1;
        Refresh(dataCount);
    }

    private void ClapContentPos()
    {
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            var clapedPos = GetClapedAnchorPos(GetScrollCurrnetPos());
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x,clapedPos);
        }
        else
        {
            var clapedPos = GetClapedAnchorPos(GetScrollCurrnetPos());
            _content.anchoredPosition = new Vector2(clapedPos,_content.anchoredPosition.y);
        }

        //调整_content的大小会影响界面原有item的位置
        SetVisibleItemPos();
    }

    private void SetVisibleItemPos()
    {
        var shown = _list._listItems;
        for (int i = 0; i < shown.Count; i++)
        {
            SetIndexItemPos(shown[i],_startIndex + i);
        }
    }
    //传入scroll pos 返回一个合理的不超框的pos
    private float GetClapedAnchorPos(float aimPos)
    {
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            var max = Mathf.Max(0, _content.rect.height - _viewsize);
            return Mathf.Clamp(aimPos,0,max);
        }
        else
        {
            var min =Mathf.Min(0,-_content.rect.width + _viewsize);
            return Mathf.Clamp(aimPos,min,0);
        }
    }
    private void SetContentSize()
    {
        var space = Mathf.Max(0,_dataCount - 1) * _spacing;
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            var h = _dataCount * _itemsize + space + _padding.top + _padding.bottom;
            _content.sizeDelta = new Vector2(_content.sizeDelta.x,h);
        }
        else
        {
            var w = _dataCount * _itemsize + space + _padding.left + _padding.right;
            _content.sizeDelta = new Vector2(w,_content.sizeDelta.y);
        }
    }
    private void AddIndexItem(int index, bool addToEnd)
    {
        ListItem item;
        if (addToEnd)
        {
            item = _list.AddItem<TItem>();
        }
        else
        {
            item = _list.InsertItem<TItem>(0);
        }
        _refreshIndex(item,index);
        item.gameObject.name = "Index" + index;

        SetIndexItemPos(item, index);
    }

    private void SetIndexItemPos(ListItem item,int index)
    {
        var pos = GetCellPosByIndex(index);
        if (_scrollDirection == ScrollDirectionEnum.Vertical)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, pos, item.transform.localPosition.z);
        }
        else
        {
            item.transform.localPosition = new Vector3(pos, item.transform.localPosition.y, item.transform.localPosition.z);
        }
    }
}

public enum ScrollDirectionEnum
{
    Vertical,
    Horizontal
}