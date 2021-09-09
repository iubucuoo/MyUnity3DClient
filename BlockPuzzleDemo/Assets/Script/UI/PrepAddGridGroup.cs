using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrepAddGridGroup : MonoBehaviour
{
    public Transform Root;
    public bool IsUse;
    [SerializeField]
    GroupBase gridData;
    // Start is called before the first frame update
    void Start()
    {
        EventTriggerListener.Get(gameObject).onDown = OnPointerDown;
        EventTriggerListener.Get(gameObject).onUp = OnPointerUp;
    }
     public void SetUse()
    {
        IsUse = true;
        //所有的子类消失
        foreach (var v in gridData.Grid)
        {
            //obj.transform.parent = transform;
            //obj.gameObject.SetActive(false);
            if (v.Status != 0)
                PoolMgr.Inst.Release(v.Image.gameObject,v.g_type);
        }
    }
    
    public void SetGridData(GroupBase v)
    {
        gridData = v;
    }
    public void OnPointerUp(GameObject eventData)
    {
        if (IsUse)
        {
            return;
        }
        DragingGridMgr.Inst.SetDragUp(this);
        Debug.Log("OnPointerUp   " + transform.name);

        if (GridGroupMgr.Inst.RefreshMainGrid())//如果当前可以放置 刷新主面板显示
        {
            //////////////////////////////////////刷新主面板显示时候执行该操作 GridGroupMgr.Inst.RevertswGrid();//还原预览的格子
            //////////////////////////////////////刷新主面板显示时候执行该操作 GridGroupMgr.Inst.ClearGrid(); //如果有可以销毁的 实现销毁并添加积分
            SetUse();//使用过了
        }

        //待放格子区 检测是否可以放置 不能放的变灰 无法使用
    }
    public void OnPointerDown(GameObject eventData)
    {
        if (IsUse)
        {
            return;
        }
        DragingGridMgr.Inst.SetDragDown(gridData);
        Debug.Log("OnPointerDown   " + transform.name);
    }
    public void Reset()
    {
        gridData = null;
        IsUse = false;
    }
}
