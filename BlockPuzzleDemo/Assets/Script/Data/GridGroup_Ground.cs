using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Ground : GroupBase
{
    public GridGroup_Ground()
    {
        Isbg = true;
        W_count = 10;
        H_count = 10;
        Grid = new GridData[10,10];
        DataArray = new int[,]{
            { 0, 0, 1, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 0 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 }
        };
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Grid[i,j]==null)
                {
                    Grid[i,j] = new GridData();
                }
            }
        }

    }

}
