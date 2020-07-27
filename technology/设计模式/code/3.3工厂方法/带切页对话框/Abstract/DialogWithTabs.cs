using System.Collections.Generic;

public abstract class DialogWithTabs 
{

    protected List<TabViewBase> tabViews = new List<TabViewBase>();

    public int GetCurrentTabIndex()
    {
        return -1;
    }

    public virtual void SwitchTab(int tabIndex)
    {
    }

    protected int TabCount;

    protected virtual void OnTabSwitch(int oldTabIndex, int newTabIndex)
    {

    }
    public virtual void ShowTabView(TabViewBase tabView)
    {

    }

    public virtual void CloseTabView(TabViewBase tabView)
    {

    }

    public virtual int TabIndexToId(int tabIndex)
    {
        return tabIndex;
    }

    public virtual int TabIdToIndex(int tabId)
    {
        return tabId;
    }
}
