using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridData
{
    int status;
    public int Status
    {
        get { return status; }
        private set
        {
            if (status!=value && Image != null)
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

    //还原显示状态
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
    public bool IsUse;//判断是否存放了格子
    //public Color g_coolr;//格子的颜色
    public Image Image;
    public Text Text;
}
