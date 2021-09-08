using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBase  
{
    public bool Isbg;
    public bool IsDrag;

    public int W_count { get; private set; }
    public int H_count { get; private set; }
    public Vector2 ParentRoot;
    public GridData[,] Grid;
    protected int[,] DataArray;
    public void SetData(int[,] data)
    {
        W_count = data.GetLength(1);
        H_count = data.GetLength(0);
        Grid = new GridData[H_count, W_count];
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Grid[i, j] == null)
                {
                    Grid[i, j] = new GridData();
                }
                Grid[i, j].IsUse = data[i, j] == 1;
            }
        }
    }
}
