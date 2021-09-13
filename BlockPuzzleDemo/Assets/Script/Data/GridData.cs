using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridData : IPoolable
{
    GameObject gridobj;
    public Transform parent;
    public Text Text { get; private set; }
    Image DefImage;
    Image desImage;//将要删除的展示图;
    Image prepImage;//将要放置的展示图;
    string resName;

    int _turestaus;
    public int TrueStatus { get { return _turestaus; } set { _turestaus = value; _TempStatus = _turestaus; } }//配置里的值，主面板的可修改
    public int _TempStatus;//临时存放的truestatus，如可以放置或着可销毁的时候临时用的

    public Vector3 position { get { return gridobj ? gridobj.transform.position : Vector3.zero; } }

    void SetSprites()
    {
        if (TrueStatus == 0)
        { DefImage.sprite = GameGloab.Sprites["SelectBubble"]; }
        else if (TrueStatus == 1)
        { DefImage.sprite = GameGloab.Sprites["FreezeStonr"]; }
        else if (TrueStatus == 2)
        { DefImage.sprite = GameGloab.Sprites["swgrid"]; }
        else if (TrueStatus == 3)
        { DefImage.sprite = GameGloab.Sprites["BlueBubble"]; }
        else if (TrueStatus == 4)
        { DefImage.sprite = GameGloab.Sprites["ColorBubble"]; }
    }
    public virtual IPoolsType IPoolsType { get { return IPoolsType.GridData; } }

    public bool IsRecycled { get; set; }

    public bool IsUse;//判断是否存放了格子
    public void Revert()
    {
        TrueStatus = _TempStatus;
        if (IPoolsType == IPoolsType.GridDataDef)
        {
            desImage.gameObject.SetActive(false);
            prepImage.gameObject.SetActive(false);
        }
        SetSprites();
    }
    /// <summary>
    /// 将要删除的展示
    /// </summary>
    public void swClear()
    {
        if (IPoolsType == IPoolsType.GridDataDef)
        {
            desImage.gameObject.SetActive(true);
            prepImage.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 将要删除的展示还原
    /// </summary>
    public void swClearRevert()
    {
        if (IPoolsType == IPoolsType.GridDataDef)
            desImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 将要放入的展示
    /// </summary>
    public void swPrep()
    {
        if (IPoolsType == IPoolsType.GridDataDef)
            prepImage.gameObject.SetActive(true);
    }
    /// <summary>
    /// 还原将要放入的展示
    /// </summary>
    public void swPrepRevert()
    {
        if (IPoolsType == IPoolsType.GridDataDef)
            prepImage.gameObject.SetActive(false);
        _TempStatus = TrueStatus;
    }
    public void CreatObj(Transform _parent, Vector2 _Pos, string res)
    {
        resName = res;
        if (gridobj == null)
        {
            gridobj = ObjectMgr.InstantiateGameObj(ResourceMgr.Inst.LoadRes<GameObject>(res));
            DefImage = gridobj.transform.Find("def").GetComponent<Image>();
            if (IPoolsType == IPoolsType.GridDataDef)
            {
                desImage = gridobj.transform.Find("des").GetComponent<Image>();
                prepImage = gridobj.transform.Find("prep").GetComponent<Image>();
            }
            if (IPoolsType == IPoolsType.GridDataPrep)
            {
                DefImage.rectTransform.sizeDelta *= 0.8f;
            }
#if UNITY_EDITOR
            gridobj.name = resName;
            //Text = gridobj.transform.Find("Text").GetComponent<Text>();
#endif
        }
        gridobj.SetActive(true);
        gridobj.transform.SetParent(_parent);
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
