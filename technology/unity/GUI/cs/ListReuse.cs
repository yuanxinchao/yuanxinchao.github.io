using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListReuse<TItem> where TItem:ListItem,new () 
{
    private RectTransform _content;
    private ScrollRect _scroll;
    private float _pfbHight;
    private float _space = 10;
    private ListView _list; 
    private int _prefabNum;

    private int _infoIndex;

    private Action<ListItem, int> _refresh; 
    public ListReuse(ScrollRect scroll, RectTransform content, GameObject model,Action<ListItem, int> refreshAc)
    {
        _refresh = refreshAc;
        _scroll = scroll;
        _content = content;
        var modelRect = model.GetComponent<RectTransform>();
        _pfbHight = modelRect.rect.height;

        _list = ListView.Create(_scroll, content, model);

        var viewport = content.parent.GetComponent<RectTransform>();
        _prefabNum = (int)(viewport.rect.height / (_pfbHight + _space)) + 3;//viewport所展示的pfb数量+3
        _scroll.onValueChanged.AddListener(ScrollValueChange);


        if(Math.Abs(content.anchorMax.y - 1) > 0.1f || Math.Abs(content.anchorMin.y - 1) > 0.1f)
            throw new Exception("content.anchor.min & max .y should =1");
        if (Math.Abs(content.pivot.y - 1) > 0.1f)
            throw new Exception("content.pivot.y should =1");
        if(Math.Abs(modelRect.pivot.y - 1) > 0.1f)
            throw new Exception("itempfb.pivot.y should =1");

    }

    private int _infoCount;

    public void SetInfoCount(int infoCount)
    {
        _infoCount = infoCount;

        _content.sizeDelta = new Vector2(_content.sizeDelta.x, (_pfbHight + _space) * _infoCount - _space);
        _content.anchoredPosition = new Vector2(_content.anchoredPosition.x,0);

        int count = _prefabNum < _infoCount ? _prefabNum : _infoCount;
        
        _list.Clear();
        for (int i = 0; i < count; i++)
        {
            ListItem item = _list.AddItem<TItem>();
            _refresh(item, i);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                CalculateYByIndex(i),
                item.transform.localPosition.z);
        }
        _infoIndex = 0;

    }

    private float CalculateYByIndex(int index)
    {
        return -index * (_space + _pfbHight);
    }

    private void ScrollValueChange(Vector2 arg0)
    {
        int index =(int)(_content.anchoredPosition.y/(_space + _pfbHight));

        while (index - 1 > _infoIndex && _infoIndex + _prefabNum < _infoCount)
        {
            int end = _infoIndex + _prefabNum;
            _list.RemoveBegin();
            ListItem item = _list.AddItem<TItem>();
            _refresh(item,end);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(end),
                                            item.transform.localPosition.z);
            _infoIndex++;
        }

        while (index - 1 < _infoIndex && _infoIndex - 1 >= 0)
        {
            _list.RemoveEnd();
            ListItem item = _list.Insert<TItem>(0);
            _refresh(item, _infoIndex - 1);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                            CalculateYByIndex(_infoIndex - 1),
                                            item.transform.localPosition.z);

            _infoIndex--;
        }
    }
    /// <summary>
    /// refreshAc 参数为刷新info某个index的action
    /// </summary>
    /// <param name="listRootTransform"></param>
    /// <param name="refreshAc"></param>
    /// <returns></returns>
    public static ListReuse<TItem> Create(Transform listRootTransform,Action<ListItem,int> refreshAc)
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

        if(content.GetComponent<LayoutGroup>()!=null)
            throw new Exception("content needn't LayoutGroup");
        if(content.GetComponent<ContentSizeFitter>()!=null)
            throw new Exception("content needn't ContentSizeFitter");

        ListReuse<TItem> list = new ListReuse<TItem>(scroll, content, itemModel, refreshAc);
        return list;
    }
}
