using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_MinPrep : GridGroup,IPoolable
{
    public GridGroup_MinPrep()
    {
        g_width = 30;
        g_height = 30;
        resName = "Prefab/blockmin";//min的格子
    }

    public override IPoolsType IPoolsType { get { return IPoolsType.GridGroup_MinPrep; } }
}
