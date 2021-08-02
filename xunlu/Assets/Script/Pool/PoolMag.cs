using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMag : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> poollist = new List<GameObject>();
    private static PoolMag _instance;
    public static PoolMag instance
    {
        get {
            if (_instance ==null)
            {
                GameObject go = new GameObject("poolmag");
                go.AddComponent<PoolMag>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public GameObject GetPool()
    {
        for (int i = 0; i < poollist.Count; i++)
        {
            if (poollist[i].activeInHierarchy == false)
            { return poollist[i]; }
        }
        GameObject addgo = Instantiate(Resources.Load("Sphere") as GameObject);
        addgo.transform.parent = this.transform;
        addgo.transform.position = Vector3.zero;
        addgo.SetActive(false);
        poollist.Add(addgo);
        return addgo;
    }
}
