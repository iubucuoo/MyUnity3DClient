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
                    mingrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block")
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
            UnityEngine.Object.Destroy(DragRoot.GetChild(i).gameObject);
        }
    }

    public void AddDragGroup(GroupBase v)
    {
        DestroyChild();
        GridTools.AddGrids(DragRoot, v, mingrid);
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
    }
}
