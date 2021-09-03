using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragingGridMgr 
{
     GridGroup gridData;
     Transform dragroot;
     bool Isdrag;
     static DragingGridMgr _Instance;
    public static DragingGridMgr Instance
    {
        get
        {
            if (_Instance==null)
            {
                _Instance = new DragingGridMgr();
            }
            return _Instance;
        }
    }
    public bool IsDrag
    {
        get { return Instance.Isdrag; }
    }
    public Transform DragRoot
    {
        get
        {
            return Instance.dragroot;
        }
        set
        {
            Instance.dragroot = value;
        }
    }

    void DestroyChild()
    {
        int childCount = DragRoot.childCount;
        for (int i = 0; i < childCount; i++)
        {
            UnityEngine.Object.Destroy(DragRoot.GetChild(i).gameObject);
        }
    }
    public void AddDragGroup(GridGroup v)
    {
        DestroyChild();
        int _width = 0;
        int _height = GameGloab.min_hcount - 1; ;
        var info = v.DataArray;
        var grid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block");

        for (int j = 0; j < info.Length; j++)
        {
            int _id = info[j];
            if (_id == 1)
            {
                var bg =UnityEngine.Object.Instantiate(grid);
                bg.transform.parent = DragRoot;
                bg.transform.localPosition = new Vector3((_width - GameGloab.min_wcount * 0.5f + 0.5f) * 60, (_height - GameGloab.min_hcount * 0.5f + 0.5f) * 60);
            }
            _width++;
            if (_width == GameGloab.min_wcount)
            {
                _width = 0;
                _height--;
            }
        }
    }
    public void SetDragDown(GridGroup v)
    {
        Instance.Isdrag = true;
        Instance.gridData = v;
        //生成拖动的格子组

        AddDragGroup(v);

    }
 

    public void SetDragUp(GridGroup v)
    {
        Instance.Isdrag = false;
        Instance.gridData = null;
        DestroyChild();
    }
}
