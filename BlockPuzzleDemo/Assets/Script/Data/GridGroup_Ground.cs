using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Ground : GroupBase
{
    public GridGroup_Ground()
    {
        Isbg = true;
        DataArray = new int[,]{
            { 0, 0, 0, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0 , 0, 0, 1, 0, 0 },
            { 1, 1, 0, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1 , 0, 0, 1, 0, 0 }
        };
        SetData(DataArray);
    }
}
