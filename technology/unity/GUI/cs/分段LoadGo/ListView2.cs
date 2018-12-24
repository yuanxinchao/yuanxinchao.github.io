using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#pragma warning disable 649

public class ListView2
{

    private Func<string,GameObject> _getGoByName;
        //列表项容器
    RectTransform Content { get; set; }

    //所有显示的列表项
    public readonly List<GameObject> _listItems = new List<GameObject>(8);

    //列表项对象池
    private readonly Dictionary<string, List<GameObject>> _freeList = new Dictionary<string, List<GameObject>>(8);

    public void Clear()
    {
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveItemAt(i);
        }
    }
    public void RemoveItemAt(int index)
    {
        var item = _listItems[index];
        var freeList = GetFreeList(item.name);
        _listItems.RemoveAt(index);
        freeList.Add(item);
        item.SetActive(false);
    }
    private List<GameObject> GetFreeList(string name)
    {
        List<GameObject> ret;
        if (!_freeList.TryGetValue(name, out ret))
        {
            ret = new List<GameObject>(8);
            _freeList.Add(name, ret);
        }
        return ret;
    }
    public GameObject AddItem(string name)
    {
        var item = GetOrCreateItem(name);
        item.transform.SetAsLastSibling(); //总是插在最后面
        _listItems.Add(item);
        return item;
    }
    public GameObject InsertItem(string name)
    {
        var item = GetOrCreateItem(name);
        item.transform.SetAsFirstSibling(); //总是插在最前面
        _listItems.Insert(0, item);
        return item;
    }
    public void RemoveBegin()
    {
        RemoveItemAt(0);
    }
    public void RemoveEnd()
    {
        RemoveItemAt(_listItems.Count - 1);
    }
    private GameObject GetOrCreateItem(string name)
    {
        GameObject ret = null;
        var freeList = GetFreeList(name);
        var itemModel = GetItemModel(name);


        //先到缓存池中找
        int count = freeList.Count;
        if (count > 0)
        {
            ret = freeList[count - 1];
            freeList.RemoveAt(count - 1);
        }

        //没找到则创建新的
        if (ret == null)
        {
            ret = CreateItem(itemModel, name);
        }

        ret.transform.localScale = Vector3.one;
        ret.SetActive(true);

        return ret;
    }
    private GameObject GetItemModel(string name)
    {
        return _getGoByName(name);
    }
    private GameObject CreateItem(GameObject itemModel, string name)
    {
        var t = Object.Instantiate(itemModel);
        t.name = name;
        t.transform.SetParent(Content, false);
        return t;
    }
    public static ListView2 Create(RectTransform content, Func<string, GameObject>  getGoByName)
    {
        ListView2 list = new ListView2();

        list.Content = content;
        list._getGoByName = getGoByName;
        if (!list.Content)
            throw new Exception("list content is null!");

        return list;
    }
}



