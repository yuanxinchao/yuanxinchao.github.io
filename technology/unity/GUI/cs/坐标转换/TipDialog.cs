using System;
using UnityEngine;
/// <summary>
/// Tip类dialog 坐标转换以便紧贴物品显示详情
/// </summary>
public abstract class TipDialog : Dialog
{
    private Canvas canvas;
    private RectTransform _root;
    private Transform _dialogRect;//可以为空
    private Camera _origin;
    private Camera _newCamera;
    protected override void OnCreate()
    {
        canvas = transform.GetComponent<Canvas>();
        _root = transform.GetComponent<RectTransform>();
        if (canvas == null)
            throw new Exception("canvas should not null");
        _dialogRect = UI.Get<Transform>("__DiaRect");//设置该碰触区域不关闭
        if (_dialogRect != null)
        {
            if(_dialogRect.GetComponent<BoxCollider>() == null)
                throw new Exception("need BoxCollider Component");
        }
        _newCamera = canvas.worldCamera;
    }


    /// <summary>
    /// 设置tip的位置
    /// </summary>
    /// <param name="worldPos">物品世界坐标</param>
    /// <param name="tip">物品tip</param>
    /// <param name="direction">偏移方向</param>
    /// <param name="distance">偏移距离</param>
    /// <param name="origin">worldPos对应的camera不传则默认用的dialog camera</param>
    protected void SetTipPos(Vector3 worldPos, RectTransform tip, Direction direction, float distance,Camera origin = null)
    {
        _origin = origin;
        if (_origin == null)
            _origin = _newCamera;

        var p = tip.parent;
        if(p == null)
            throw new Exception("tip parent should not null");
        if (tip.parent != _root)
            throw new Exception("tip parent should == root");

        // 设置tip重心为0.5f,0.5f
        tip.pivot = new Vector2(0.5f,0.5f);
        
        var shift = Vector2.zero;
        switch (direction)
        {
           case Direction.Up:
                shift += new Vector2(0, distance + tip.rect.height/2);
                break;
           case Direction.Down:
                shift += new Vector2(0, -(distance + tip.rect.height / 2));
                break;
           case Direction.Left:
                shift += new Vector2( -(distance + tip.rect.width / 2),0);
                break;
           case Direction.Right:
                shift += new Vector2(distance + tip.rect.width / 2, 0);
                break;
        }
        Vector3 screenPos = _origin.WorldToScreenPoint(worldPos);

        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_root,
            screenPos, _newCamera, out local);

        tip.localPosition = local + shift;
        RegularPos(tip, shift);
       
    }

    private void RegularPos(RectTransform tip, Vector3 shift)
    {
        var reverse = -2*shift;
        var tipSize = tip.sizeDelta;
        var rootSize = _root.sizeDelta;

        //tip的y值小于下边界
        if (tip.localPosition.y - tipSize.y/2 < -rootSize.y/2f)
        {
            tip.localPosition = tip.localPosition + reverse;
        }
        //tip的y值 大于上边界
        if (tip.localPosition.y + tipSize.y / 2 > rootSize.y / 2f)
        {
            tip.localPosition = tip.localPosition +reverse;
        }

        //clamp tip的x坐标
        tip.localPosition = new Vector3(Mathf.Clamp(tip.localPosition.x, -rootSize.x / 2f + tipSize.x / 2, rootSize.x / 2f - tipSize.x / 2)
            , tip.localPosition.y
            , tip.localPosition.z);
    }
    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = canvas.worldCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            LayerMask mask = 1 << LayerMask.NameToLayer("UI");
            if (!Physics.Raycast(ray, out hitInfo, 1000, mask.value))
            {
                Close();
            }
            else
            {
                if (hitInfo.collider.transform != _dialogRect)
                {
                    Close();
                }
            }
        }
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}
