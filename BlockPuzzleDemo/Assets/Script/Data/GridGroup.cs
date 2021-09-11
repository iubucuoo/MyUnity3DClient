using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGroup : IPoolable
{
    public virtual IPoolsType IPoolsType { get { return IPoolsType.GridGroup; } }
    IPoolsType gridpooltype;
    public bool IsRecycled { get; set; }
    protected bool Isbg;
    protected int g_width;
    protected int g_height;
    public string resName { get; protected set; }
    public int W_count { get; private set; }
    public int H_count { get; private set; }
    public GridData[,] Grid{ get; protected set; }
    public int[,] DataArray { get; protected set; }
    Transform Root;

    public void SetData(int[,] _data, Transform _root, IPoolsType _pooltype)
    {
        Root = _root;
        DataArray = _data;
        gridpooltype = _pooltype;
    }
    Vector3 Pos = new Vector3(0, 0);
    public void CreatGrids()
    {
        W_count = DataArray.GetLength(1);
        H_count = DataArray.GetLength(0);
        Grid = new GridData[H_count, W_count];
        int h_1 = H_count - 1;
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (DataArray[i, j] == 1 || Isbg)
                {
                    if (Grid[i, j] == null)
                    {
                        Grid[i, j] = PoolMgr.Allocate(gridpooltype) as GridData;
                    }
                    Grid[i, j].IsUse = DataArray[i, j] == 1;

                    Pos.x = (j - W_count * 0.5f + 0.5f) * g_width;
                    Pos.y = (h_1 - i - H_count * 0.5f + 0.5f) * g_height;
                    Grid[i, j].CreatObj(Root, Pos, resName);
#if UNITY_EDITOR
                    if (Grid[i, j].IsUse)
                        Grid[i, j].Text.text = i + ":" + j;
                    else
                        Grid[i, j].Text.text = "";
#endif
                }
            }
        }
    }

    public void OnRecycled()
    {
        foreach (var v in Grid)
        {
            if (v!=null)
            {
                PoolMgr.Recycle(v);
            }
        }
    }
}
