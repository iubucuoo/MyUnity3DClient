using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragingGridMgr
{
    bool Isdrag;
    Transform dragroot;
    Image mingrid;
    static DragingGridMgr _Instance;
    public static DragingGridMgr Inst
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new DragingGridMgr
                {
                    mingrid = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockdrag")
                };
            }
            return _Instance;
        }
    }

    public bool IsDrag { get { return Inst.Isdrag; } }

    public Transform DragRoot { get { return Inst.dragroot; } set { Inst.dragroot = value; } }

    public GroupBase gridData;
    void DestroyChild()
    {
        int childCount = DragRoot.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DragRoot.GetChild(i).gameObject.SetActive(false);
            //UnityEngine.Object.Destroy(DragRoot.GetChild(i).gameObject);
        }
    }

    public void AddDragGroup(GroupBase v)
    {
        DestroyChild();
        GridTools.CreatGrids(DragRoot, v, mingrid);
    }

    public void SetDragDown(GroupBase v)
    {
        Inst.Isdrag = true;
        Inst.gridData = v;
        AddDragGroup(v);
    }

    public void SetDragUp(GroupBase v)
    {
        Inst.Isdrag = false;
        Inst.gridData = null;
        DestroyChild();
        DragRoot.localPosition = GameGloab.OutScreenV2;
        GridGroupMgr.Inst.RefreshMainGrid(); //如果当前可以放置 刷新主面板显示
        //////////////////////////////////////刷新主面板显示时候执行该操作 GridGroupMgr.Inst.RevertswGrid();//还原预览的格子
        //////////////////////////////////////刷新主面板显示时候执行该操作 GridGroupMgr.Inst.ClearGrid(); //如果有可以销毁的 实现销毁并添加积分
        //待放格子区 检测是否可以放置 不能放的变灰 无法使用
    }
}
