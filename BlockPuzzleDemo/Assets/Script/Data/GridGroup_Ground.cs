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
        Grid = new GridData[100];
        DataArray = new int[100];
        for (int i = 0; i < DataArray.Length; i++)
        {
            if (Grid[i]==null)
            {
                Grid[i] = new GridData();
            }
            Grid[i].sprite = GameGloab.Sprites["defgrid"];
            Grid[i].IsUse = DataArray[i] == 1;
        }

    }
}
