using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolMag : MonoBehaviour
{
    [SerializeField]
    public List<Image> poollist = new List<Image>();
    private static PoolMag _instance;
    public static PoolMag Inst
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
    public Image GetPool(Image obj)
    {
        for (int i = 0; i < poollist.Count; i++)
        {
            if (poollist[i].gameObject.activeInHierarchy == false)
            {
                poollist[i].gameObject.SetActive(true);
                return poollist[i];
            }
        }
        var addgo = Instantiate(obj);
        poollist.Add(addgo);
        return addgo;
    }
}
