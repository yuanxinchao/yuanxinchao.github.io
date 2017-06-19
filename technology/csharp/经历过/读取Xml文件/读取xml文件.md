## 读取xml文件
XML文件是一种常用的文件格式。今天就看看xml的三种常用读取方式。  

* 使用 Linq to Xml
* 使用 XmlDocument
* 使用 XmlTextReader

这里只说一下最好用的Linq to Xml。  
### 1.Linq to Xml ###
---
#####命名空间
    using System.Linq;
    using System.Xml.Linq;  
#####相关函数  
名称                       | 说明
--------------------------|----
XElement                  |表示一个 XML 元素。  
Elements(XName)           |按文档顺序返回用XName删选过的XElement集合
Load(Stream)              |载入一个xml文档  
XElement(XName, Object[]) |  新建一个  
Add(Object)               | 添加一个节点  
Save(Stream)              |保存
Remove()                  |从节点父级中删除此节点。
Attribute(XName)          | 返回`XElement`中指定`XName`的`XAttribute`类型
#####实现
了解完以上方法，实现就很容易了。

    // Use this for initialization
    private XElement xe;
    private string Local = "IpAddress.xml";
    void Start()
    {
		//载入
        xe = XElement.Load(Local);
        Debug.Log(xe.Name);
        //这里打印Socket
        //增
        AddNode();
        //删
        DeleteNode("华为云");
        //改
        ChangeNode("华为云", "1438");
        //查
        FindNode("腾讯云");

        //纯Linq实现 删
        DeleteNodeLinq("华为云");
    }

    void AddNode()
    {
        XElement ele = new XElement("net",
            new XAttribute("Type","公用网"),
            new XAttribute("Cp","华为云"),
            new XElement("PublicIp", "163.23.43.99"),
            new XElement("PrivateIp", "172.19.119.4"),
            new XElement("Position", "华东电信一区神圣天堂"),
            new XElement("Port", "11000"));
        xe.Add(ele);
        Debug.Log("已经添加了节点" + "华为云");
        xe.Save(Local);
    }

    void DeleteNodeLinq(string key)
    {
        List<XElement> elements = (from ele in xe.Elements("net")
                                         let xAttribute = ele.Attribute("Cp")
                                         where xAttribute != null && xAttribute.Value == key
                                         select ele).ToList();
        if (elements.Count == 0)
        {
            Debug.Log("未查询到节点" + key);
        }
        foreach (var ele in elements)
        {
            ele.Remove();
            Debug.Log("已经删除了节点" + key);
            xe.Save(Local);
        }
    }
    void DeleteNode(string key)
    {
        IEnumerable<XElement> elements = xe.Elements("net");
        foreach (var ele in elements)
        {
            XAttribute attribute = ele.Attribute("Cp");
            if (attribute != null && attribute.Value == key)
            {
                ele.Remove();
                Debug.Log("已经删除了节点" + key);
                xe.Save(Local);
            }
            else
            {
                Debug.Log("未查询到节点" + key);
            }
        }
    }

    void ChangeNode(string key,string aidValue)
    {
        IEnumerable<XElement> elements =xe.Elements("net");
        foreach (var ele in elements)
        {
            XAttribute attribute = ele.Attribute("Cp");
            if (attribute != null && attribute.Value == key)
            {
                XElement element = ele.Element("Port");
                if (element != null) element.Value = aidValue;
                Debug.Log("已经修改了节点" + key);
                xe.Save(Local);
            }
            else
            {
                Debug.Log("未查询到节点" + key);
            }
        }
    }

    void FindNode(string key)
    {
        IEnumerable<XElement> elements = xe.Elements("net");
        foreach (var ele in elements)
        {
            XAttribute attribute = ele.Attribute("Cp");
            if (attribute != null && attribute.Value == key)
            {
                Debug.Log("查询到了节点" + key);
                IEnumerable<XElement> AidElements = ele.Elements();
                foreach (var el in AidElements)
                {
                    Debug.Log(string.Format("{0} = {1}",el.Name,el.Value));
                }
            }
            else
            {
                Debug.Log("未查询到节点" + key);
            }
        }
    }
剩下的两种方式有点麻烦，以后再说.