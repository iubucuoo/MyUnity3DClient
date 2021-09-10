using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridDataMin : GridData
{
    public override PoolsType PoolType => PoolsType.GridDataMin;

    public override void OnRecycled()
    {
        base.OnRecycled();
    }
}