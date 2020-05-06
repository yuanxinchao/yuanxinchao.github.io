using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#pragma warning disable 649

public class ListView
{
    //列表项模板
    readonly Dictionary<string, GameObject> _listItemModels = new Dictionary<string, GameObject>(4);

    //列表项容器
    RectTransform Content { get; set; }

    //所有显示的列表项
    public readonly List<ListItem> _listItems = new List<ListItem>(8);

    //列表项对象池
    private readonly Dictionary<string, List<ListItem>> _freeList = new Dictionary<string, List<ListItem>>(8);

    private ScrollRect _scrollRect;

    public ScrollRect ScrollRect
    {
        get
        {
            if (_scrollRect == null)
            {
                _scrollRect = Content.parent.parent.GetComponent<ScrollRect>();
                if (_scrollRect == null)
                    throw new Exception("list scrollRect is null!");
            }
            return _scrollRect;
        }
        private set { _scrollRect = value; }
    }

    private  ListView()
    {
    }

    public void SetListContent(RectTransform content)
    {
        Content = content;
    }

    private T CreateItem<T>(GameObject itemModel, string itemModelType) where T : ListItem
    {
        var t = Object.Instantiate(itemModel).transform;
        t.SetParent(Content, false);
        var ret = (ListItem)Activator.CreateInstance(typeof(T));
        ret.gameObject = t.gameObject;
        ret.List = this;
        ret.ItemModelType = itemModelType;

        return (T)ret;
    }

    private T GetOrCreateItem<T>() where T : ListItem
    {
        ListItem ret = null;
        var itemModelType = typeof(T).FullName;

        var freeList = GetFreeList(itemModelType);
        var itemModel = GetItemModel(itemModelType);


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
            ret = CreateItem<T>(itemModel, itemModelType);
            ret.OnCreate();
        }

        ret.transform.localScale = Vector3.one;
        ret.gameObject.SetActive(true);
        ret.OnAddToList();

        return (T) ret;
    }

    private GameObject GetItemModel(string itemModelType)
    {
        GameObject ret;
        if (!_listItemModels.TryGetValue(itemModelType, out ret))
        {
            throw new Exception("list item model is null: " + itemModelType);
        };
        return ret;
    }

    //插入新的item，排在列表最前面
    public T InsertItem<T>(int itemModelType = -1) where T : ListItem
    {
        var item = GetOrCreateItem<T>();
        item.transform.SetAsFirstSibling(); //总是插在最前面
        _listItems.Insert(0, item);
        return item;
    }
    public T InsertItem_Index<T>(int index) where T : ListItem
    {
        var item = GetOrCreateItem<T>();
        item.transform.SetSiblingIndex(index + _listItemModels.Count); //插入指定位置
        _listItems.Insert(index, item);
        return item;
    }

    //插入新的item，排在列表最后面
    public T AddItem<T>() where T : ListItem
    {
        var item = GetOrCreateItem<T>();
        item.transform.SetAsLastSibling(); //总是插在最后面
        _listItems.Add(item);
        return item;
    }

    public void Clear()
    {
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveItemAt(i);
        }
    }

    public void RemoveBegin()
    {
        RemoveItemAt(0);
    }
    public void RemoveEnd()
    {
        RemoveItemAt(_listItems.Count - 1);
    }
    public void Sort()
    {
        _listItems.Sort();
        for (int i = 0; i < _listItems.Count; i++)
        {
            _listItems[i].transform.SetSiblingIndex(i);
        }
    }

    private List<ListItem> GetFreeList(string itemModelType)
    {
        List<ListItem> ret;
        if (!_freeList.TryGetValue(itemModelType, out ret))
        {
            ret = new List<ListItem>(8);
            _freeList.Add(itemModelType, ret);
        }
        return ret;
    }

    public void RemoveItemAt(int index)
    {
        var item = _listItems[index];
        var freeList = GetFreeList(item.ItemModelType);
        _listItems.RemoveAt(index);
        freeList.Add(item);
        item.SetVisible(false);
        item.OnRemoveFromList();
    }

    public void RemoveItem(ListItem item)
    {
        int index = -1;
        for (int i = 0; i < _listItems.Count; i++)
        {
            if (_listItems[i] == item)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
            RemoveItemAt(index);
        else
        {
            throw new Exception("Can't find listItem to remove");
        }
    }

    public void RemoveAllItems()
    {
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveItemAt(i);
        }
    }

    /// <summary>
    /// 添加一个新的Item模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="itemModel"></param>
    public void AddItemModel<T>(GameObject itemModel)
    {
        if(itemModel == null)
            throw new Exception("list item model gameobject is null: " + typeof(T).FullName);

        var key = typeof(T).FullName;

        if (_listItemModels.ContainsKey(key))
        {
            string msg = string.Format("The listItemModel {0} has added to listView, needn't add again;", key);
            throw new Exception(msg);
        }

        //模板总是设置为不可见
        itemModel.SetActive(false);
        //Debug.Log("add itemModel: " + key);
        _listItemModels.Add(key, itemModel);
    }

    private void Awake()
    {
    }

    private void OnDestroy(GameObject content)
    {
        Clear();
    }

    /// <summary>
    /// 创建ListView
    /// </summary>
    /// <typeparam name="T">ListItem模板类型</typeparam>
    /// <param name="listRootTransform">ListView根transform，总是有一个SrollRect组件在其上</param>
    /// <returns></returns>
    public static ListView Create<T>(Transform listRootTransform) where T : ListItem
    {
        Debug.Log("create listView, root gameobject is: " + listRootTransform.name);

        ListView list = new ListView();

        list.ScrollRect = listRootTransform.GetComponent<ScrollRect>();

        if(!list.ScrollRect)
            throw new Exception("list scrollRect is null! root gameobject: " + listRootTransform.gameObject.name);

        //总是认为ScrollView的Content在root->child0->child0位置
        list.Content = listRootTransform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        if(!list.Content)
            throw new Exception("list content is null! root gameobject: " + listRootTransform.gameObject.name);

        //listItem模板对象，认为其总是ScrollView Content下的第一个子对象
        var itemModel = list.Content.GetChild(0).gameObject;

        if(!itemModel)
            throw new Exception("list item model is null! root gameobject: " + listRootTransform.gameObject.name);

        list.AddItemModel<T>(itemModel);
        list.Awake();
        return list;
    }

    public static ListView Create<T>(ScrollRect scrollRect,RectTransform content,GameObject model = null) where T : ListItem
    {
        ListView list = new ListView();

        list.ScrollRect = scrollRect;

/*        if (!list.ScrollRect)
            throw new Exception("list scrollRect is null!");*/
        list.Content = content;

        if (!list.Content)
            throw new Exception("list content is null!");

        if (model == null)
        {
            //listItem模板对象,默认是ScrollView Content下的第一个子对象
            model = list.Content.GetChild(0).gameObject;

            if (!model)
                throw new Exception("list item model is null!");
        }

        list.AddItemModel<T>(model);
        list.Awake();
        return list;
    }
    public static ListView Create<T>(RectTransform content,GameObject model) where T : ListItem
    {
        ListView list = new ListView();

        list.Content = content;

        if (!list.Content)
            throw new Exception("list content is null!");

        if (model == null)
        {
            throw new Exception("list item model is null!");
        }

        list.AddItemModel<T>(model);
        list.Awake();
        return list;
    }
}



