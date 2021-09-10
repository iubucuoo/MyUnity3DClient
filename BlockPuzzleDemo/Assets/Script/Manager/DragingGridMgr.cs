using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragingGridMgr
{
    static DragingGridMgr _Instance;
    public static DragingGridMgr Inst
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new DragingGridMgr();
            }
            return _Instance;
        }
    }

    public bool IsDrag { get; private set; }

    public Transform DragRoot { get; set; }

    public GridGroup_Prep prepData;

    public void AddDragGroup(GridGroup_Prep v)
    {
        //PoolMgr.Recycle(prepData);
        v.CreatGrids(DragRoot);
    }

    public void SetDragDown(GridGroup v)
    {
        IsDrag = true;
        prepData = PoolMgr.Allocate(PoolsType.GridGroup_Prep)as GridGroup_Prep;
        prepData.SetData(v.DataArray, PoolsType.GridDataDef);
        AddDragGroup(prepData);
    }

    public void SetDragUp(PrepAddGridGroup v)
    {
        DragRoot.localPosition = GameGloab.OutScreenV2;
        PoolMgr.Recycle(prepData);
        IsDrag = false;
    }
}
