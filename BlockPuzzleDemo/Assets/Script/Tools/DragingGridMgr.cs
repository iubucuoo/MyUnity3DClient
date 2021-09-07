using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragingGridMgr
{
    bool Isdrag;
    GroupBase gridData;
    Transform dragroot;
    Image mingrid;
    static DragingGridMgr _Instance;
    public static DragingGridMgr Instance
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

    public bool IsDrag { get { return Instance.Isdrag; } }

    public Transform DragRoot { get { return Instance.dragroot; } set { Instance.dragroot = value; } }

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
        Instance.Isdrag = true;
        Instance.gridData = v;
        AddDragGroup(v);
    }

    public void SetDragUp(GroupBase v)
    {
        Instance.Isdrag = false;
        Instance.gridData = null;
        DestroyChild();
        DragRoot.localPosition = GameGloab.OutScreenV2;
    }
}
