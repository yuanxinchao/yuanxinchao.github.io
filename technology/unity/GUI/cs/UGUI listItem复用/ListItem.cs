using System;
using UnityEngine;

#pragma warning disable 649
public abstract class ListItem
{
    public ListView List;

    public Transform transform { get { return gameObject.transform; }}

    public GameObject gameObject { get; set; }

    public string ItemModelType { get; set; }

    public virtual int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }

    public void RemoveFromList()
    {
        List.RemoveItem(this);
    }

    public void SetVisible(bool visible)
    {
        if (transform == null)
            throw new Exception("ListItemRoot is null!");
        transform.gameObject.SetActive(visible);
    }

    public abstract void OnCreate();

    /// <summary>
    /// 添加到ListView时
    /// </summary>
    public virtual void OnAddToList()
    {
    }

    /// <summary>
    /// 从ListView移除时
    /// </summary>
    public virtual void OnRemoveFromList()
    {
    }
}
