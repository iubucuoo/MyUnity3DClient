using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolMgr : MonoBehaviour
{
    [SerializeField]
    public Queue<GameObject> Ground_Stack = new Queue<GameObject>();
    [SerializeField]
    public Queue<GameObject> MinPrep_Stack = new Queue<GameObject>();
    [SerializeField]
    public Queue<GameObject> Prep_Stack = new Queue<GameObject>();
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
        GameObject obj;
        switch (type)
        {
            case GroupType.Ground:
                obj =  Get(Ground_Stack, type);
                break;
            case GroupType.MinPrep:
                obj = Get(MinPrep_Stack, type);
                break;
            case GroupType.Prep:
                obj = Get(Prep_Stack, type);
                break;
            default:
                obj = Get(Ground_Stack, type);
                break;
        }
        obj.gameObject.SetActive(true);
        return obj;
    }
    GameObject Get(Queue<GameObject> stack, GroupType type)
    {
        GameObject _obj;
        if (stack.Count == 0)
        {
            GameObject obj;
            switch (type)
            {
                case GroupType.Ground:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockdef").gameObject;
                    break;
                case GroupType.MinPrep:
                    obj = ResourceMgr.Inst.LoadRes<Image>("Prefab/blockmin").gameObject;
                    break;
                case GroupType.Prep:
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
    public void Release(GameObject obj, GroupType type)
    {
        obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
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
