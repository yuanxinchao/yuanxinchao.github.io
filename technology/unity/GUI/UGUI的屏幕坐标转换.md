## UGUI的屏幕坐标转换
![UGUI1](./UGUIPic/UGUI1.png)  
在选择了Screen Match Mode为Match Height时，要把重写了方法的  

	public void  OnPointerDown (PointerEventData eventData)
	{
	}
**eventData**进行坐标转换，转换为UGUI Canvas画布上的对应坐标。  
**Match Height**时，以**高度1136像素**为例转换公式为  

    private Vector3 TransformToRelativePos (Vector3 originPos)
    {
        Vector3 transform = Vector3.zero;
        transform.x = (1136f / Screen.height) * (originPos.x - (Screen.width / 2f));
        transform.y = 1136f * (originPos.y / Screen.height - 0.5f);
        return transform;
    }

以**宽度750像素**为例转换公式为  

    /// <summary>
    /// 将鼠标点下去的坐标转换成相对坐标。PS:这里是matchwithwidth
    /// </summary>
    /// <returns>The to relative position.</returns>
    /// <param name="originPos">Origin position.</param>
    private Vector3 TransformToRelativePos(Vector3 originPos)
    {
        Vector3 transform = Vector3.zero;
        transform.x = (750f / Screen.width) * (originPos.x - (Screen.width / 2f));
        transform.y = Screen.height * (originPos.y / Screen.height - 0.5f);
        return transform;
    }


**附带世界坐标转屏幕坐标**

	Vector3 transPos = transCamera.WorldToScreenPoint(itemPos);
    transPos = TransformToRelativePos(transPos);