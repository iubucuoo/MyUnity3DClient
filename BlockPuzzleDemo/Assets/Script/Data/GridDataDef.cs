using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridDataDef : GridData
{
    public override PoolsType PoolType => PoolsType.GridDataDef;

   

    public override void OnRecycled()
    {
        base.OnRecycled();
    }

}