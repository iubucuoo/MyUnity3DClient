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
    public GridGroup_Prep prepData;
    void DestroyChild()
    {
        foreach (var v in prepData.Grid)
        {
            if (v.Status != 0)
            {
                PoolMgr.Inst.Release(v.Image.gameObject, prepData.g_type);
            }
        }
    }

    public void AddDragGroup(GroupBase v)
    {
        DestroyChild();
        v.CreatGrids(DragRoot);
    }

    public void SetDragDown(GroupBase v)
    {
        Inst.Isdrag = true;
        Inst.prepData = new GridGroup_Prep(v.DataArray);
        AddDragGroup(prepData);
    }

    public void SetDragUp(PrepAddGridGroup v)
    {
        DestroyChild();
        DragRoot.localPosition = GameGloab.OutScreenV2;
        Inst.Isdrag = false;
        Inst.prepData = null;
    }
}
