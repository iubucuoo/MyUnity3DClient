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
        get { return Isdrag; }
    }
    public void SetDragDown(GridGroup v)
    {
        Instance.Isdrag = true;
        gridData = v;


    }
    public void SetDragUp(GridGroup v)
    {
        Instance.Isdrag = false;
        gridData = null;
    }
}
