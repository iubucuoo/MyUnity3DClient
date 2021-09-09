using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Prep : GroupBase
{
    public GridGroup_Prep(int[,] DataArray)
    {
        g_width = 60;
        g_height = 60;
        g_type = GroupType.Prep;
        resName = "Prefab/blockdrag";//拖动出来的格子
        SetData(DataArray);
    }
}
