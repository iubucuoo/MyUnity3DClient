using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainC : MonoBehaviour
{
    private void Awake()
    {
        var canver = GameObject.Find("Canvas");
        GameGloab.root_bg = canver.transform.Find("gamebg/BGROOT");
        GameGloab.root_prep = canver.transform.Find("gamebg/ADDROOT");
        DragingGridMgr.Inst.DragRoot = canver.transform.Find("gamebg/DragRoot");
    }
    public List<Sprite> sprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<GridGroupMgr>();
        foreach (var v in sprites)
        {
            GameGloab.Sprites[v.name] = v;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
