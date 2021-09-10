using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolMgr
{
    public Dictionary<int, Pool> dic;
    [SerializeField]
    public Queue<GameObject> Ground_Stack = new Queue<GameObject>();
    [SerializeField]
    public Queue<GameObject> MinPrep_Stack = new Queue<GameObject>();
    [SerializeField]
    public Queue<GameObject> Prep_Stack = new Queue<GameObject>();
    private static PoolMgr _inst;
    public static PoolMgr Inst
    {
        get {
            if (_inst ==null)
            {
                _inst = new PoolMgr();
                _inst.dic = new Dictionary<int, Pool>();
            }
            return _inst;
        }
    }

 
    IPool GetValue(PoolsType _type)
    {
        if (!dic.TryGetValue((int)_type,out Pool pool))
        {
            pool = new ObjectPool();
            dic.Add((int)_type, pool);
        }
        return pool.Allocate(_type);
    }
    void ResetValue(IPool pool)
    {
        if (dic.TryGetValue((int)pool.PoolType,out Pool v))
        {
            v.Recycle(pool);
        }
        else
        {
            //字典中不存在的类型
        }
    }
    public static void Recycle(IPool pool)
    {
        Inst.ResetValue(pool);
    }
    public static IPool Allocate(PoolsType _type)
    {
        return Inst.GetValue(_type);
    }
 
    public object GetGameObjectPool(PoolsType type)
    {
        GameObject obj;
        switch (type)
        {
            case PoolsType.GridGroup_Ground:
                obj =  Get(Ground_Stack, type);
                break;
            case PoolsType.GridGroup_MinPrep:
                obj = Get(MinPrep_Stack, type);
                break;
            case PoolsType.GridGroup_Prep:
                obj = Get(Prep_Stack, type);
                break;
            default:
                obj = Get(Ground_Stack, type);
                break;
        }
        obj.gameObject.SetActive(true);
        return obj;
    }
    GameObject Get(Queue<GameObject> stack, PoolsType type)
    {
        GameObject _obj;
        if (stack.Count == 0)
        {
            GameObject obj;
            switch (type)
            {
                case PoolsType.GridGroup_Ground:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockdef").gameObject;
                    break;
                case PoolsType.GridGroup_MinPrep:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockmin").gameObject;
                    break;
                case PoolsType.GridGroup_Prep:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockdrag").gameObject;
                    break;
                default:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockdef").gameObject;
                    break;
            }
            _obj = ObjectMgr.InstantiateGameObj(obj);
        }
        else
        {
            while (true)
            {
                _obj = stack.Dequeue();
                if (_obj != null)
                {
                    return _obj;
                }
                if (stack.Count == 0)
                {
                    return Get(stack, type);
                }
            }
        }
        if (_obj==null)
        {
            Debug.LogError("obj==null");
        }
        return _obj;
    }
    public void Release(GameObject obj, PoolsType type)
    {
        //obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
        switch (type)
        {
            case PoolsType.GridGroup_Ground:
                Ground_Stack.Enqueue(obj);
                break;
            case PoolsType.GridGroup_MinPrep:
                MinPrep_Stack.Enqueue(obj);
                break;
            case PoolsType.GridGroup_Prep:
                Prep_Stack.Enqueue(obj);
                break;
            default:
                Ground_Stack.Enqueue(obj);
                break;
        }
    }
}
