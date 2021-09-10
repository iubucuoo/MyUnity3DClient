using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_MinPrep : GridGroup,IPool
{
    public GridGroup_MinPrep()
    {
        g_width = 30;
        g_height = 30;
        resName = "Prefab/blockmin";//min的格子
    }

    public PoolsType PoolType { get { return PoolsType.GridGroup_MinPrep; } }

    public bool IsRecycled { get; set ; }

    public void OnRecycled()
    {
        RecycleGrid();
    }
}
