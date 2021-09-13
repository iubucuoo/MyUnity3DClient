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
    GridGroup_MinPrep minPrepGroup;
    // Start is called before the first frame update
    void Start()
    {
        EventTriggerListener.Get(gameObject).onDown = OnPointerDown;
        EventTriggerListener.Get(gameObject).onUp = OnPointerUp;
    }
     void SetUse()
    {
        IsUse = true;
        PoolMgr.Recycle(minPrepGroup);//回收

        //待放格子区 检测是否可以放置 不能放的变灰 无法使用
        //如果不能放 执行相应的操作(比如游戏结束)

        //三个格子都用完了，刷新三个待放入的格子
        if (GridGroupMgr.Inst.IsOverPrep())
        {
            GridGroupMgr.Inst.RefreshPrepGridGroup();
        }
    }

    public void SetGridData(GridGroup_MinPrep v)
    {
        minPrepGroup = v;
    }
    void SetChildActive(bool sw)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(sw);
        }
    }
    public void OnPointerUp(GameObject eventData)
    {
        if (IsUse)
        {
            return;
        }
        Debug.Log("OnPointerUp   " + transform.name);
        DragingGridMgr.Inst.SetDragUp(this);
        if (GridGroupMgr.Inst.RefreshMainGrid())//如果当前可以放置 刷新主面板显示
        {
            SetUse();//设置当前待放入的group为使用过了
        }
        else
        {
            SetChildActive(true);//使用失败 跑一个回到原始位置的动画
        }
    }
    public void OnPointerDown(GameObject eventData)
    {
        if (IsUse )
        {
            return;
        }
        Debug.Log("OnPointerDown   " + transform.name);
        DragingGridMgr.Inst.SetDragDown(minPrepGroup);
        SetChildActive(false);
    }
    public void Reset()
    {
        IsUse = false;
    }
}
