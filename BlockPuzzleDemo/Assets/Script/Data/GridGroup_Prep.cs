using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Prep : GridGroup
{
    public IPoolsType PoolsType = IPoolsType.GridGroup_Prep;
    public GridGroup_Prep()
    {
        g_width = 60;
        g_height = 60;
        resName = "Prefab/blockdrag";//拖动出来的格子
    }

    public override IPoolsType IPoolsType { get { return IPoolsType.GridGroup_Prep; } }
}
