using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResuse : MonoBehaviour
{
    private List<int> data = new List<int>();

    private ListReuse3<Item> _list;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            data.Add(i);
        }

        _list = ListReuse3<Item>.Create(transform,RefreshIndex,ScrollDirectionEnum.Horizontal);
        _list.Refresh(data.Count);

    }

    // Update is called once per frame
    private int i;
    void Update()
    {
        i++;
        if (i == 100)
        {
            data.Clear();
            for (int j = 0; j < 1000; j++)
            {
                data.Add(j);
            }
            _list.RefreshAndJumpTo(data.Count,463,false,true);
        }
    }

    public void RefreshIndex(ListItem item,int index)
    {
        ((Item)item).Refresh(data[index]);
    }
}

public class Item : ListItem
{
    private Text t;
    public override void OnCreate()
    {
        t = transform.Find("Text").GetComponent<Text>();
    }

    public void Refresh(int data)
    {
        t.text = data.ToString();
    }
}