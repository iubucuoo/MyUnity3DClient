using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Prep : GridGroup,IPool
{
    public PoolsType PoolsType = PoolsType.GridGroup_Prep;
    public GridGroup_Prep()
    {
        g_width = 60;
        g_height = 60;
        resName = "Prefab/blockdrag";//拖动出来的格子
    }

    public PoolsType PoolType { get { return PoolsType.GridGroup_Prep; } }

    public bool IsRecycled { get ; set ; }

    public void OnRecycled()
    {
        RecycleGrid();
    }
}
