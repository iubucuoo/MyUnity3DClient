using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PoolsType
{
    GridGroup_Ground,
    GridGroup_MinPrep,
    GridGroup_Prep,
    GridData,
    GridDataMin,
    GridDataDef,
}
public interface IPool
{
    PoolsType PoolType { get; }
    void OnRecycled();//重置
    bool IsRecycled { get; set; }

}
public interface IPoolable
{
    IPool Allocate(PoolsType _typ);//分配
    bool Recycle(IPool obj);//回收
}
public interface IObjectFactory<T>
{
    T Create(PoolsType _type);
}
class CreateInstance : IObjectFactory<IPool>
{
    public IPool Create(PoolsType _type)
    {
        return Activator.CreateInstance(Type.GetType(_type.ToString()), true) as IPool;
    }
}
public abstract class Pool:IPoolable
{
    public int CurCount { get { return mCacheStack.Count; } }

    public bool IsRecycled { get ; set ; }

    protected IObjectFactory<IPool> mFactory;
    protected Stack<IPool> mCacheStack = new Stack<IPool>();
    public virtual IPool Allocate(PoolsType _type)
    {
        //Debug.LogError(_type.ToString() + "    " + CurCount);
        if (CurCount <= 0)
        {
            Debug.LogError(_type.ToString() + "    " + CurCount);
            return mFactory.Create(_type);
        }
        else
            return mCacheStack.Pop();
    }
    public abstract bool Recycle(IPool obj);
}

public class ObjectPool : Pool 
{
    public ObjectPool()
    {
        mFactory = new CreateInstance();
    }
    public override IPool Allocate(PoolsType _type)
    {
        IPool result = base.Allocate(_type);
        result.IsRecycled = false;
        return result;
    }

    public override bool Recycle(IPool obj)
    {
        if (obj == null || obj.IsRecycled)
        {
            return false;
        }
        obj.IsRecycled = true;
        obj.OnRecycled();
        mCacheStack.Push(obj);
        return true;
    }
}