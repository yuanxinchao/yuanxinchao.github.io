## Debug的使用
#### 重中之重的debug的使用
> 1.debug在调用功能性函数的时候必须加。格式为**Debug.Log("YXC "+"函数功能"+必要标志位或参数)**  
> 
> 2.初始化统计(友盟或者Talking Data)或广告SDK时一定要**Debug出Key和ChannelId渠道**，以便定位问题。如果怕用户看到Log可以加限制条件。比如在某个局域网，某个地方连续点击，或某个确定硬件地址才会Debug信息。  
> 
> 3.在**android工程**里同样如此。  

---
#### 打印一个类里的变量或者属性和值 ####
在调试程序的时候经常要打印出一个类里的变量和属性的值，这时候如果一个一个打印就太麻烦了，可以考虑重写tostring方法用反射取出里面的名称和值，如下所示。FieldInfo 对应取变量，PropertyInfo对应取属性。

    public override string ToString()
    {
        StringBuilder ret = new StringBuilder(10);
        FieldInfo[] vars = this.GetType().GetFields(BindingFlags.Public |
                                                    BindingFlags.Static | 
                                                    BindingFlags.Instance | 
                                                    BindingFlags.NonPublic);
        PropertyInfo[] props = this.GetType().GetProperties(BindingFlags.Public |
                                                            BindingFlags.Static |
                                                            BindingFlags.Instance |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.DeclaredOnly);
        for (int i = 0; i < vars.Length; i++)
        {
            ret.Append(string.Format("[{0}={1}]", vars[i].Name, vars[i].GetValue(this)));
        }
        ret.Append("\n");
        for (int i = 0; i < props.Length; i++)
        {
            ret.Append(string.Format("[{0}={1}]", props[i].Name, props[i].GetValue(this, null)));
        }
        return ret.ToString();
    }

