using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Prep : GroupBase
{
    public GridGroup_Prep()
    {
        W_count = 5;
        H_count = 4;// 5;
        DataArray = new int[,]{
            //{ 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 }
        };
        Grid = new GridData[H_count, W_count];
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Grid[i, j] == null)
                {
                    Grid[i, j] = new GridData();
                }
            }
        }
    }
}
