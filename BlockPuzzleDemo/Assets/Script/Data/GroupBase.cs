using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GroupType
{
    Ground,
    MinPrep,
    Prep,
}
public class GroupBase  
{
    public int g_width;
    public int g_height;
    public GroupType g_type;
    public string resName;

    public bool Isbg;
    public bool IsDrag;

    public int W_count { get; private set; }
    public int H_count { get; private set; }
    public Vector2 ParentRoot;
    public GridData[,] Grid;
    public int[,] DataArray { get; protected set; }
    public void SetData(int[,] data)
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
                    Grid[i, j] = new GridData();
                }
                Grid[i, j].IsUse = data[i, j] == 1;
            }
        }
    }
    public void CreatGrids(Transform root,bool isdrag= false)
    {
        Vector3 Pos = new Vector3(0, 0);
        float width = g_width;
        float height = g_height;
        int h_1 = H_count - 1;
        for (int i = 0; i < H_count; i++)
        {
            for (int j = 0; j < W_count; j++)
            {
                if (Isbg)
                {
                    if (Grid[i, j].Image == null)
                    {
                        Grid[i, j].parent = root;
                        var bg = PoolMgr.Inst.GetPool(g_type) as GameObject;
                        bg.transform.parent = root;
                        Pos.x = (j - W_count * 0.5f + 0.5f) * g_width;
                        Pos.y = (h_1 - i - H_count * 0.5f + 0.5f) * g_height;
                        bg.transform.localPosition = Pos;
                        Grid[i, j].Image = bg.GetComponent<Image>();
                        Grid[i, j].Revert();
#if UNITY_EDITOR
                        Grid[i, j].Text = bg.transform.Find("Text").GetComponent<Text>();
                        if (Grid[i, j].IsUse)
                            Grid[i, j].Text.text = i + ":" + j;
                        else
                            Grid[i, j].Text.text = "";
#endif
                    }
                }
                else if (Grid[i, j].IsUse)
                {
                    var bg = PoolMgr.Inst.GetPool(g_type) as GameObject;// Object.Instantiate(obj);
                    bg.transform.parent = root;
                    //if (isdrag && M_math.Even(W_count))
                    //    Pos.x = (j - W_count * 0.5f) * g_width;
                    //else
                        Pos.x = (j - W_count * 0.5f + 0.5f) * g_width;
                    //if (isdrag && M_math.Even(H_count))
                    //    Pos.y = (h_1 - i - H_count * 0.5f) * g_height;
                    //else
                        Pos.y = (h_1 - i - H_count * 0.5f + 0.5f) * g_height;
                    bg.transform.localPosition = Pos;
                    Grid[i, j].Image = bg.GetComponent<Image>();
                    Grid[i, j].Revert();
#if UNITY_EDITOR
                    Grid[i, j].Text = bg.transform.Find("Text").GetComponent<Text>();
                    Grid[i, j].Text.text = i + ":" + j;
#endif
                }
            }
        }
    }
}
