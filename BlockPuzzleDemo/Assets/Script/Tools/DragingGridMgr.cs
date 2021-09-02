using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragingGridMgr 
{
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
    public void SetDragDown(GridData v)
    {
        Instance.Isdrag = true;
        
        
    }
    public void SetDragUp(GridData v)
    {
        Instance.Isdrag = false;
    }
}
