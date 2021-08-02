using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bherry;
public abstract class PoolBase :List<IPools>
{
    protected int maxcount = 500;
    private int allpoolNum=0;
    public virtual void Reset(IPools pools)
    {
        if (Count>=maxcount)
        {
            allpoolNum--;
            pools.Dispose();//数量超出预订值 直接删除
            return;
        }
        if (pools.IsUsing)
        {
            Add(pools);//加到池内
            pools.Reset();
        }
        pools.IsUsing = false;
    }
    public virtual void ClearAll()
    {
        Clear();
    }
    public int AllpoolNum { get { return allpoolNum; }}

}
