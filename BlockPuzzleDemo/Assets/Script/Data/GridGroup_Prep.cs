﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup_Prep : GroupBase
{
    public GridGroup_Prep()
    {
        W_count = 5;
        H_count = 5;
        DataArray = new int[]{
            0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        1, 1, 1, 1, 1};
 
    }
}
