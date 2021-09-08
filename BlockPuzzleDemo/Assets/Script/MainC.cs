using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainC : MonoBehaviour
{
    private void Awake()
    {
        var canver = GameObject.Find("Canvas");
        GameGloab.root_bg = canver.transform.Find("BGROOT");
        GameGloab.root_prep = canver.transform.Find("ADDROOT");
        DragingGridMgr.Inst.DragRoot = canver.transform.Find("DragRoot");
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
