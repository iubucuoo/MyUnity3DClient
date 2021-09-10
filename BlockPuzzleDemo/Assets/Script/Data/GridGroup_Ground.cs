using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Ground : GridGroup,IPool
{
    public GridGroup_Ground()
    {
        g_width = 60;
        g_height = 60;
        resName = "Prefab/blockdef";//默认的背景格子
        Isbg = true;
        DataArray = new int[,]{
            { 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 , 0, 0, 0, 0, 0 }
        };
        SetData(DataArray,PoolsType.GridDataDef);
    }

    public PoolsType PoolType { get { return PoolsType.GridGroup_MinPrep; } }

    public bool IsRecycled { get ; set ; }

    public void OnRecycled()
    {
        RecycleGrid();
    }
}
