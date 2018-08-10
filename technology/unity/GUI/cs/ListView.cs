using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ListView
{
    private RectTransform _content;
    private ScrollRect _scroll;
    private GameObject _model;


    public readonly List<ListItem> _listItems = new List<ListItem>(8);
    public readonly Stack<ListItem> _freeList = new Stack<ListItem>(8); 


    public ListView(ScrollRect scroll, RectTransform content, GameObject model)
    {
        _scroll = scroll;
        _content = content;
        _model = model;
       
    }

    public static ListView Create(ScrollRect scroll, RectTransform content, GameObject model)
    {
        ListView list = new ListView(scroll, content, model);
        return list;
    }

    public void Clear()
    {
        
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveIndex(i);
        }
    }
    public T AddItem<T>() where T : ListItem, new()
    {
        T t = GetOrCreat<T>();
        t.transform.SetAsLastSibling();
        t.transform.gameObject.SetActive(true);
        _listItems.Add(t);

        return t;
    }
    public T Insert<T>(int index) where T : ListItem, new()
    {
        T t = GetOrCreat<T>();
        t.transform.SetSiblingIndex(index);
        t.transform.gameObject.SetActive(true);
        _listItems.Insert(index, t);

        return t;
    }

    private T GetOrCreat<T>() where T : ListItem, new()
    {
        T t;
        if (_freeList.Count > 0)
        {
            var itme = _freeList.Pop();
            t = itme as T;
        }
        else
        {
            GameObject go = Object.Instantiate(_model, _content, true);
            t = new T();
            t.InitInf(go.transform);
        }
        return t;
    }
    public void RemoveItem(ListItem itme)
    {
        itme.transform.gameObject.SetActive(false);
        _listItems.Remove(itme);
        _freeList.Push(itme);
    }
    public void RemoveEnd()
    {
        RemoveIndex(_listItems.Count - 1);
    }
    public void RemoveBegin()
    {
        RemoveIndex(0);
    }
    public void RemoveIndex(int index)
    {
        var item = _listItems[index];
        _listItems.RemoveAt(index);
        _freeList.Push(item);
        item.transform.gameObject.SetActive(false);
    }
}