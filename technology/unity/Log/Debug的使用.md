## Debug的使用
#### 重中之重的debug的使用
> 1.debug在调用功能性函数的时候必须加。格式为**Debug.Log("YXC "+"函数功能"+必要标志位或参数)**  
> 
> 2.初始化统计(友盟或者Talking Data)或广告SDK时一定要**Debug出Key和ChannelId渠道**，以便定位问题。如果怕用户看到Log可以加限制条件。比如在某个局域网，某个地方连续点击，或某个确定硬件地址才会Debug信息。  
> 
> 3.在**android工程**里同样如此。