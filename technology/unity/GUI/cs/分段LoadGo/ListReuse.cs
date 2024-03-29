﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListReuse<TItem> where TItem : ListItem, new()
{
    private RectTransform _content;
    private ScrollRect _scroll;
    private float _pfbHight;
    private float _space = 10;
    private ListView _list;
    private int _prefabNum;

    private int _infoIndex;

    private Action<ListItem, int> _refresh;
    public ListReuse(ScrollRect scroll, RectTransform content, GameObject model, Action<ListItem, int> refreshAc)
    {
        _refresh = refreshAc;
        _scroll = scroll;
        _content = content;
        var modelRect = model.GetComponent<RectTransform>();
        _pfbHight = modelRect.rect.height;

        _list = ListView.Create<TItem>(_scroll, content, model);

        var viewport = content.parent.GetComponent<RectTransform>();
        _prefabNum = (int)(viewport.rect.height / (_pfbHight + _space)) + 3;//viewport所展示的pfb数量+3
        _scroll.onValueChanged.AddListener(ScrollValueChange);


        if (Math.Abs(content.anchorMax.y - 1) > 0.1f || Math.Abs(content.anchorMin.y - 1) > 0.1f)
            throw new Exception("content.anchor.min & max .y should =1");
        if (Math.Abs(content.pivot.y - 1) > 0.1f)
            throw new Exception("content.pivot.y should =1");
        if (Math.Abs(modelRect.pivot.y - 1) > 0.1f)
            throw new Exception("itempfb.pivot.y should =1");

    }

    private int _infoCount;

    public void SetBeginAndRefresh(int infoCount)
    {
        _scroll.verticalNormalizedPosition = 1;
        _infoCount = infoCount;
        SetContentSize();
        SetIndexListInfo(0);
    }
    //info count 所有数据数量
    public void Refresh(int infoCount)
    {
        _infoCount = infoCount;
        SetContentSize();
        int index = NowIndex();

        SetIndexListInfo(index);

    }

    private void SetIndexListInfo(int index)
    {
        int count = _infoCount < _prefabNum ? _infoCount : _prefabNum;
        _list.Clear();
        int num = 0;
        for (int i = 0; i < count; i++)
        {
            if (i + index < _infoCount)
            {
                SetIndexInfo(i + index, true);
                num++;
            }
            else
                break;

        }
        for (; num < count; num++)
        {
            if (index - 1 >= 0)
            {
                SetIndexInfo(index - 1, false);
                index--;
            }
            else
                break;
        }
        _infoIndex = index;

    }

    private void SetIndexInfo(int index, bool addToEnd)
    {
        ListItem item;
        if (addToEnd)
            item = _list.AddItem<TItem>();
        else
            item = _list.InsertItem<TItem>(0);
        _refresh(item, index);
        item.transform.localPosition = new Vector3(item.transform.localPosition.x,
            CalculateYByIndex(index),
            item.transform.localPosition.z);
    }
    private void SetContentSize()
    {
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, GetLength(_infoCount));
    }

    //pfb count -> content length
    private float GetLength(int count)
    {
        return (_pfbHight + _space) * count - _space;
    }
    private float CalculateYByIndex(int index)
    {
        return -index * (_space + _pfbHight);
    }

    private void ScrollValueChange(Vector2 arg0)
    {
        int index = NowIndex();

        while (index - 1 > _infoIndex && _infoIndex + _prefabNum < _infoCount)
        {
            int end = _infoIndex + _prefabNum;
            _list.RemoveBegin();
            ListItem item = _list.AddItem<TItem>();
            _refresh(item, end);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(end),
                                            item.transform.localPosition.z);
            _infoIndex++;
        }

        while (index - 1 < _infoIndex && _infoIndex - 1 >= 0)
        {
            _list.RemoveEnd();
            ListItem item = _list.InsertItem<TItem>(0);
            _refresh(item, _infoIndex - 1);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(_infoIndex - 1),
                                            item.transform.localPosition.z);

            _infoIndex--;
        }
    }

    private int NowIndex()
    {
        int index = (int)(_content.anchoredPosition.y / (_space + _pfbHight));

        return Mathf.Clamp(index, 0, _infoCount - 1);
    }
    /// <summary>
    /// refreshAc 参数为刷新info某个index的action
    /// </summary>
    /// <param name="listRootTransform"></param>
    /// <param name="refreshAc"></param>
    /// <returns></returns>
    public static ListReuse<TItem> Create(Transform listRootTransform, Action<ListItem, int> refreshAc)
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

        if (content.GetComponent<LayoutGroup>() != null)
            throw new Exception("content needn't LayoutGroup");
        if (content.GetComponent<ContentSizeFitter>() != null)
            throw new Exception("content needn't ContentSizeFitter");

        ListReuse<TItem> list = new ListReuse<TItem>(scroll, content, itemModel, refreshAc);
        return list;
    }
}

public class ListReuse2
{
    private RectTransform _content;
    private ScrollRect _scroll;
    private float _pfbHight;
    private int _space;
    private int _prefabNum;
    private int _infoIndex;
    private ListView2 _list;
    private Action<GameObject, int> _refresh;
    private Func<int, string> _getNameByIndex;
    public ListReuse2(ScrollRect scroll, RectTransform content, float height, Func<int, string> getNameByIndex, Action<GameObject, int> refreshAc, Func<string, GameObject> getGoByName)
    {
        _refresh = refreshAc;
        _scroll = scroll;
        _content = content;
        _pfbHight = height;
        _getNameByIndex = getNameByIndex;

        var viewport = content.parent.GetComponent<RectTransform>();
        _prefabNum = (int)(viewport.rect.height / (_pfbHight + _space)) + 3;//viewport所展示的pfb数量+3
        _scroll.onValueChanged.AddListener(ScrollValueChange);
        _list = ListView2.Create(content, getGoByName);

        if (Math.Abs(content.anchorMax.y - 1) > 0.1f || Math.Abs(content.anchorMin.y - 1) > 0.1f)
            throw new Exception("content.anchor.min & max .y should =1");
        if (Math.Abs(content.pivot.y - 1) > 0.1f)
            throw new Exception("content.pivot.y should =1");

    }

    private int _infoCount;

    public void SetSpace(int space)
    {
        _space = space;
    }
    public void SetBeginAndRefresh(int infoCount)
    {
        _scroll.verticalNormalizedPosition = 1;
        _infoCount = infoCount;
        SetContentSize();
        SetIndexListInfo(0);
    }
    //info count 所有数据数量
    public void Refresh(int infoCount)
    {
        _infoCount = infoCount;
        SetContentSize();
        int index = NowIndex();

        SetIndexListInfo(index);

    }

    private void SetIndexListInfo(int index)
    {
        int count = _infoCount < _prefabNum ? _infoCount : _prefabNum;
        _list.Clear();
        int num = 0;
        for (int i = 0; i < count; i++)
        {
            if (i + index < _infoCount)
            {
                SetIndexInfo(i + index, true);
                num++;
            }
            else
                break;

        }
        for (; num < count; num++)
        {
            if (index - 1 >= 0)
            {
                SetIndexInfo(index - 1, false);
                index--;
            }
            else
                break;
        }
        _infoIndex = index;

    }

    private void SetIndexInfo(int index, bool addToEnd)
    {
        GameObject item;
        if (addToEnd)
            item = _list.AddItem(_getNameByIndex(index));
        else
            item = _list.InsertItem(_getNameByIndex(index));
        _refresh(item, index);
        item.transform.localPosition = new Vector3(item.transform.localPosition.x,
            CalculateYByIndex(index),
            item.transform.localPosition.z);
    }
    private void SetContentSize()
    {
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, GetLength(_infoCount));
    }

    //pfb count -> content length
    private float GetLength(int count)
    {
        return (_pfbHight + _space) * count - _space;
    }
    private float CalculateYByIndex(int index)
    {
        return -index * (_space + _pfbHight);
    }

    private void ScrollValueChange(Vector2 arg0)
    {
        int index = NowIndex();

        while (index - 1 > _infoIndex && _infoIndex + _prefabNum < _infoCount)
        {
            int end = _infoIndex + _prefabNum;
            _list.RemoveBegin();
            GameObject item = _list.AddItem(_getNameByIndex(end));
            _refresh(item, end);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(end),
                                            item.transform.localPosition.z);
            _infoIndex++;
        }

        while (index - 1 < _infoIndex && _infoIndex - 1 >= 0)
        {
            _list.RemoveEnd();
            GameObject item = _list.InsertItem(_getNameByIndex(_infoIndex - 1));
            _refresh(item, _infoIndex - 1);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(_infoIndex - 1),
                                            item.transform.localPosition.z);

            _infoIndex--;
        }
    }

    private int NowIndex()
    {
        int index = (int)(_content.anchoredPosition.y / (_space + _pfbHight));

        return Mathf.Clamp(index, 0, _infoCount - 1);
    }
    /// <summary>
    /// refreshAc 参数为刷新info某个index的action
    /// </summary>
    /// <param name="listRootTransform"></param>
    /// <param name="refreshAc"></param>
    /// <returns></returns>
    public static ListReuse2 Create(Transform listRootTransform, float height, Func<int, string> getNameByIndex, Action<GameObject, int> refreshAc, Func<string, GameObject> getGoByName)
    {

        var scroll = listRootTransform.GetComponent<ScrollRect>();

        if (!scroll)
            throw new Exception("list scrollRect is null! root gameobject: " + listRootTransform.gameObject.name);

        //总是认为ScrollView的Content在root->child0->child0位置
        var content = listRootTransform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        if (!content)
            throw new Exception("list content is null! root gameobject: " + listRootTransform.gameObject.name);

        if (content.GetComponent<LayoutGroup>() != null)
            throw new Exception("content needn't LayoutGroup");
        if (content.GetComponent<ContentSizeFitter>() != null)
            throw new Exception("content needn't ContentSizeFitter");

        

        ListReuse2 list = new ListReuse2(scroll, content,height,getNameByIndex, refreshAc,getGoByName);
        return list;
    }
}
