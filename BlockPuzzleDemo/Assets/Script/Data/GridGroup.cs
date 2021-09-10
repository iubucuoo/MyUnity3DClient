using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGroup
{
    public int g_width;
    public int g_height;
    public PoolsType g_type;
    public string resName;

    public bool Isbg;
    public bool IsDrag;

    public int W_count { get; private set; }
    public int H_count { get; private set; }
    public Vector2 ParentRoot;
    public GridData[,] Grid;
    public int[,] DataArray { get; protected set; }
    protected void RecycleGrid()
    {
        foreach (var v in Grid)
        {
            if (v.IsUse)
            {
                PoolMgr.Recycle(v);
            }
        }
    }
    public void SetData(int[,] data , PoolsType poolsType)
    {
        DataArray = data;
        W_count = data.GetLength(1);
        H_count = data.GetLength(0);
        Grid = new GridData[H_count, W_count];
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Grid[i, j] == null)
                {
                    Grid[i, j] = PoolMgr.Allocate(poolsType) as GridData;
                }
                Grid[i, j].IsUse = data[i, j] == 1;
            }
        }
    }
    public void CreatGrids(Transform root)
    {
        Vector3 Pos = new Vector3(0, 0);
        int h_1 = H_count - 1;
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Isbg)
                {
                    Pos.x = (j - W_count * 0.5f + 0.5f) * g_width;
                    Pos.y = (h_1 - i - H_count * 0.5f + 0.5f) * g_height;
                    Grid[i, j].CreatObj(root, Pos, resName);
#if UNITY_EDITOR
                    if (Grid[i, j].IsUse)
                        Grid[i, j].Text.text = i + ":" + j;
                    else
                        Grid[i, j].Text.text = "";
#endif
                }
                else if (Grid[i, j].IsUse)
                {
                    Pos.x = (j - W_count * 0.5f + 0.5f) * g_width;
                    Pos.y = (h_1 - i - H_count * 0.5f + 0.5f) * g_height;
                    Grid[i, j].CreatObj(root, Pos, resName);
#if UNITY_EDITOR
                    Grid[i, j].Text.text = i + ":" + j;
#endif
                }
            }
        }
    }
}
