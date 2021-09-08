using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainC : MonoBehaviour
{
    private void Awake()
    {
        GameGloab.root_bg = transform.Find("BGROOT");
        GameGloab.root_prep = transform.Find("ADDROOT");
        DragingGridMgr.Inst.DragRoot = transform.Find("DragRoot");
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<GridGroupMgr>();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
