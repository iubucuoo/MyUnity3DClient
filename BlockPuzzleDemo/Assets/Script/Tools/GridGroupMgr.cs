using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGroupMgr : MonoBehaviour
{
    public Image preproot;
    public Image defgrid;
    public Image mingrid;
    public Image usegrid;

    public GridGroup_Ground gridGroup_Ground;//展示数据
    public static GridGroupMgr Inst;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        preproot = ResourceMgr.Instance.LoadRes<Image>("Prefab/addgridbg");
        mingrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockmin");
        defgrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockno");
        usegrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block");
        GameGloab.Sprites["usegrid"] = usegrid.sprite;
        GameGloab.Sprites["mingrid"] = mingrid.sprite;
        GameGloab.Sprites["defgrid"] = defgrid.sprite;

        gridGroup_Ground = new GridGroup_Ground();
    }

    /// <summary>
    /// 检测现在的位置能不能放
    /// </summary>
    public static void CheckAvailable()
    {

    }
}
