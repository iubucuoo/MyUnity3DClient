using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_MinPrep : GroupBase
{
    public GridGroup_MinPrep(int[,] DataArray)
    {
        g_width = 30;
        g_height = 30;
        g_type = GroupType.MinPrep;
        resName = "Prefab/blockmin";//min的格子
        SetData(DataArray);
    }
}
