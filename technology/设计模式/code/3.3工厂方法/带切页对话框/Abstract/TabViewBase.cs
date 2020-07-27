
public class TabViewBase
{
    public int TabId { get; set; }
    public int TabIndex { get; set; }

    protected bool IsCreated = false;

    public virtual void OnCreate()
    {
        IsCreated = true;
        InitEventListener();
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnClose()
    {
    }

    public virtual void InitEventListener()
    {
    }

    /// <summary>
    /// 清理注册的事件
    /// </summary>
    public virtual void ClearEventListener()
    {
    }

    public virtual void OnDestroy()
    {

    }
}