using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridData:IPoolable
{
    GameObject gridobj;
    public Transform parent;
    public Text Text { get; private set; }
    Image Image;
    string resName;
    int status;
    public int Status//临时显示的修改
    {
        get { return status; }
        protected set
        {
            if (status != value && Image != null)
            {
                if (value == 0)
                { Image.sprite = GameGloab.Sprites["defgrid"]; }
                else if (value == 1)
                { Image.sprite = GameGloab.Sprites["usegrid"]; }
                else if (value == 2)
                { Image.sprite = GameGloab.Sprites["swgrid"]; }
                else if (value == 3)
                { Image.sprite = GameGloab.Sprites["mingrid"]; }
            }
            status = value;
        }
    }

    public virtual IPoolsType IPoolsType { get { return IPoolsType.GridData; } }

    public bool IsRecycled { get ; set; }

    public bool IsUse;//判断是否存放了格子
    public void Revert()
    {
        if (IsUse)
        {
            Status = 1;
        }
        else
        {
            Status = 0;
        }
    }
    /// <summary>
    /// 将要删除的展示
    /// </summary>
    public void swClear()
    {
        Status = 3;
    }
    /// <summary>
    /// 将要删除的展示还原
    /// </summary>
    public void swClearRevert()
    {
        Revert();
    }

    /// <summary>
    /// 将要放入的展示
    /// </summary>
    public void swPrep()
    {
        Status = 2;
    }
    /// <summary>
    /// 还原将要放入的展示
    /// </summary>
    public void swPrepRevert()
    {
        Revert();
    }
    public void CreatObj(Transform _parent ,Vector2 _Pos, string res)
    {
        resName = res;
        if (gridobj==null)
        {
            gridobj = ObjectMgr.InstantiateGameObj(ResourceMgr.Inst.LoadRes<Image>(res).gameObject);
            Image = gridobj.GetComponent<Image>();
#if UNITY_EDITOR
            Text = gridobj.transform.Find("Text").GetComponent<Text>();
#endif
        }
        gridobj.SetActive(true);
        gridobj.transform.parent = _parent;
        gridobj.transform.localPosition = _Pos;
        Revert();
    }
    public virtual void OnRecycled()
    {
        if (gridobj)
        {
            gridobj.SetActive(false);
        }
    }
}
