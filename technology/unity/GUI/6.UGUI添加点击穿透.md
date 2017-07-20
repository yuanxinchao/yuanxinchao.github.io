## UGUI添加点击穿透
如果想要给一个图片添加点击的话，首要的前提就是勾选raycast target，但是一旦勾选了raycast target，点击事件就被拦截了，下层的ui是响应不到的，当然需要响应的情况不多见。  

>我的处理方法是：将前面一层的ui添加box collider(3d)，用射线碰撞去响应，而不是用ugui的点击事件。代码如下

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = canvas.worldCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (!Physics.Raycast(ray, out hitInfo))
            {
                Close();
            }
            else
            {
                if (hitInfo.collider.gameObject != _tipGameObject)
                {
                    Close(); 
                }
            }
        }
    }
	