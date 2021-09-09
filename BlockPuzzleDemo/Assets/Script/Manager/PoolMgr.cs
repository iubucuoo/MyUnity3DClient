using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolMgr : MonoBehaviour
{
    [SerializeField]
    public Queue<Object> Ground_Stack = new Queue<Object>();
    [SerializeField]
    public Queue<Object> MinPrep_Stack = new Queue<Object>();
    [SerializeField]
    public Queue<Object> Prep_Stack = new Queue<Object>();
    private static PoolMgr _instance;
    public static PoolMgr Inst
    {
        get {
            if (_instance ==null)
            {
                GameObject go = new GameObject("poolmag");
                go.AddComponent<PoolMgr>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public object GetPool(GroupType type)
    {
        switch (type)
        {
            case GroupType.Ground:
                return Get(Ground_Stack, type);
            case GroupType.MinPrep:
                return Get(MinPrep_Stack, type);
            case GroupType.Prep:
                return Get(Prep_Stack, type);
            default:
                return Get(Ground_Stack, type);
        }
    }
    object Get(Queue<Object> stack, GroupType type)
    {
        object _obj;
        if (stack.Count == 0)
        {
            Object obj;
            switch (type)
            {
                case GroupType.Ground:
                    obj = ResourceMgr.Inst.LoadRes<Object>("Prefab/blockdef");
                    break;
                case GroupType.MinPrep:
                    obj = ResourceMgr.Inst.LoadRes<Object>("Prefab/blockmin");
                    break;
                case GroupType.Prep:
                    obj = ResourceMgr.Inst.LoadRes<Object>("Prefab/blockdrag");
                    break;
                default:
                    obj = ResourceMgr.Inst.LoadRes<Object>("Prefab/blockdef");
                    break;
            }
            _obj = ObjectMgr.InstantiateObj(obj);
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
    public void Release(Object obj, GroupType type)
    {
        //obj.transform.parent = transform;
        //obj.gameObject.SetActive(false);
        switch (type)
        {
            case GroupType.Ground:
                Ground_Stack.Enqueue(obj);
                break;
            case GroupType.MinPrep:
                MinPrep_Stack.Enqueue(obj);
                break;
            case GroupType.Prep:
                Prep_Stack.Enqueue(obj);
                break;
            default:
                Ground_Stack.Enqueue(obj);
                break;
        }
    }
}
