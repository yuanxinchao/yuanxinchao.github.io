using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
public class TabHelper
{
    public List<ITab> _tabs = new List<ITab>(4);
    private int _currentSpId = -1;
    public Action<int, int> OnTabSwitch { get; set; }

    public void Finish()
    {
        var tab = GetTabBySpId(_currentSpId, true);
        if (tab == null)
        {
            if (_tabs.Count > 0)
                Click(_tabs[0]);
        }
        else
        {
            Click(tab);
        }
    }
    public void Clear()
    {
        for (int i = _tabs.Count - 1; i >= 0; i--)
        {
            RemoveIndex(i);
        }
    }
    public void ClearCurrentSpId()
    {
        _currentSpId = -1;
    }
    public void ClickBySpId(int spId)
    {
        if (_currentSpId != -1 && _currentSpId != spId)
        {
            var lastTab = GetTabBySpId(_currentSpId, true);
            if (lastTab != null)
            {
                lastTab.Unselect();
            }
        }

        if (_currentSpId != spId)
        {
            ITab tab = GetTabBySpId(spId);
            tab.Select();
        }

        if (OnTabSwitch != null)
            OnTabSwitch(_currentSpId, spId);

        _currentSpId = spId;
    }
    public void Click(ITab tab)
    {
        ClickBySpId(tab.SpId);
    }
    public void ClickByIndex(int index)
    {
        Click(_tabs[index]);
    }
    private void AddTab(ITab tab)
    {
        Insert(_tabs.Count, tab);
    }
    public void AddTab(ITab tab,int spId)
    {
        tab.SpId = spId;
        AddTab(tab);
    }
    public void Insert(int index, ITab tab)
    {
        if (index > _tabs.Count)
            throw new Exception("can't insert to list, index=" + index + " count=" + _tabs.Count);

        _tabs.Insert(index, tab);

        for (int i = index; i < _tabs.Count; i++)
        {
            _tabs[i].Index = i;
        }

        if (tab.SpId == _currentSpId)
        {
            tab.Select();
        }
        else
        {
            tab.Unselect();
        }
    }
    public void RemoveSpId(int spId)
    {
        ITab tab = GetTabBySpId(spId);
        Remove(tab);
    }
    public void Remove(ITab tab)
    {
        int index = _tabs.IndexOf(tab);
        if (index == -1)
            throw new Exception("can't remove tab SpId=" + tab.SpId);

        RemoveIndex(index);
    }

    public void RemoveIndex(int index)
    {
        _tabs.RemoveAt(index);
        for (int i = index; i < _tabs.Count; i++)
        {
            _tabs[i].Index = i;
        }
    }

    public ITab GetTabBySpId(int spId, bool canBeNull = false)
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            if (_tabs[i].SpId == spId)
                return _tabs[i];
        }
        if (canBeNull)
            return null;

        throw new Exception("can't find tabBtn spId=" + spId);
    }

    public List<ITab> GetTabList()
    {
        return _tabs;
    }

    public void Close()
    {
        var tab = GetTabBySpId(_currentSpId, true);
        if (tab != null)
            tab.Unselect();
    }
}
public class TabBtnHelper
{
    private TabHelper _tabHelper;
    private TabBtnPool _createTab;

    public Action<int, int> OnTabSwitch {get { return _tabHelper.OnTabSwitch; }set { _tabHelper.OnTabSwitch = value; }
    }
    public TabBtnHelper(RectTransform root, GameObject model)
    {
        _tabHelper = new TabHelper();
        _createTab = new TabBtnPool(root, model);
    }

    public T AddTab<T>(int spId) where T : Behaviour, ITab
    {
        T tab = _createTab.Get<T>(spId);
        _tabHelper.AddTab(tab, spId);
        tab.transform.gameObject.SetActive(true);
        tab.transform.GetComponent<Button>().onClick.SetListener(() => _tabHelper.Click(tab));
        return tab;
    }

    public void Clear()
    {
        var tabs = _tabHelper.GetTabList();
        for (int i = tabs.Count - 1; i >= 0; i--)
        {
            RemoveIndex(i);
        }
    }

    public void RemoveIndex(int index)
    {
        var tab = _tabHelper.GetTabList()[index];
        _createTab.Put(tab);
        _tabHelper.RemoveIndex(index);
    }
    public void Finish()
    {
        _tabHelper.Finish();
    }
    public List<ITab> GetTabList()
    {
        return _tabHelper.GetTabList();
    }
}
public abstract class TabViewBase2 : Behaviour, ITab
{
    public int SpId { get; set; }
    public int Index { get; set; }
    public virtual void Select()
    {
        transform.gameObject.SetActive(true);
    }

    public virtual void Unselect()
    {
        transform.gameObject.SetActive(false);
    }
}

public interface ITab
{
    int SpId { get; set; }
    int Index { get; set; }

    void Select();

    void Unselect();

}
public class TabBtnPool
{
    private GameObject _model;
    private RectTransform _root;

    private Stack<ITab> _tabFreeList = new Stack<ITab>();

    public TabBtnPool(RectTransform root, GameObject model)
    {
        _root = root;
        _model = model;
        _model.gameObject.SetActive(false);
    }
    public T Create<T>() where T : Behaviour, ITab
    {
        var tab = Object.Instantiate(_model, _root);
        T t = tab.AddBehaviour<T>();
        return t;
    }
    public void Put(ITab tab) 
    {
        ((Behaviour)tab).transform.gameObject.SetActive(false);
        _tabFreeList.Push(tab);
    }

    public T Get<T>(int spId) where T : Behaviour, ITab
    {
        T ret = null;
        int count = _tabFreeList.Count;
        if (count > 0)
        {
            ret = (T)_tabFreeList.Pop();
        }
        if (ret == null)
        {
            ret = Create<T>();
        }
        return ret;
    }
}
