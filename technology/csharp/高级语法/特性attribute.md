## 特性attribute  
### [参考链接](http://blog.csdn.net/okvee/article/details/2610349) ###
### 1.几个重要的特性 ###
**Conditional:**起条件编译的作用，只有满足条件，才允许编译器对它的代码进行编译。一般在程序调的时候使用。  
**Dllmport:**用来标记非.NET的函数，表明该方法在一个外部的DLL中定义。  
**Obsolete:**这个属性用来标记当前的方法已经被废弃，不再使用了。  

Attribute类是在编译的时候被实例化的，而不是像通常的类那样在运行时候才实例化。
如：  

	#define DEBUG //这里定义条件
	using UnityEngine;
	using System;
	using System.Runtime.InteropServices;
	using System.Diagnostics; 

	[Conditional("DEBUG")]
    [Obsolete("use othermethod institead")]
    private static void DisplayDebugMessage()
    {
        UnityEngine.Debug.Log("开始Main子程序");
    }

    [DllImport("User32.dll")]
    public static extern int MessageBox(int hParent, string Message, string Caption, int Type);  
### 2.当然我们也可以创建自已的Attribute ###  

	[AttributeUsage(AttributeTargets.Class)]
	public class InformationAttribute:System.Attribute
	{
	    private string coder;//代码编辑者
	    private string date;//编写时间
	    private string comment;//检查结果
	
	    //参数构造器
	    public InformationAttribute(string coder,string date)
	    {
	        this.coder = coder;
	        this.date = date;
	    }
	    public string Coder { get { return coder; } set { coder = value; } }
	    public string Date { get { return date; } set { date = value; } }
	    public string Comment { get { return comment; } set { comment = value; } }
	}
	
	[Information("YXC", "2017-4-13",Comment = "no problem") ]
	public class BigPrj
	{
	    //.........
	}

	void Start () {
        Type t = typeof(BigPrj);
        object[] atts =  t.GetCustomAttributes(false);
        InformationAttribute infoatt;
        foreach (var att in atts)
        {

            infoatt =(InformationAttribute) att;
            Debug.Log("代码编辑者="+infoatt.Coder+ "\n编写时间=" + infoatt.Date + "\n检查结果=" + infoatt.Comment);
        }
    }

运行的结果为: 
代码编辑者=YXC  
编写时间=2017-4-13  
检查结果=no problem  
### 3.AttributeUsageAttribute中的3个属性（Property）说明： ###  
>* 1.ValidOn:该定位参数指定可在其上放置所指示的属性 (Attribute) 的程序元素。AttributeTargets 枚举数中列出了可在其上放置属性 (Attribute) 的所有可能元素的集合。可通过按位“或”运算组合多个 AttributeTargets 值，以获取所需的有效程序元素组合。  
* 2.AllowMultiple:该命名参数指定能否为给定的程序元素多次指定所指示的属性。  
* 3.Inherited:该命名参数指定所指示的属性能否由派生类和重写成员继承。