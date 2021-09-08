using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGroupMgr : MonoBehaviour
{
    public Image preproot;
    public Image defgrid;
    public Image mingrid;
    public Image usegrid;
    public Image swgrid;
    readonly Dictionary<int, int> postox = new Dictionary<int, int>()
    {
        [-270] = 0,
        [-210] = 1,
        [-150] = 2,
        [-90] = 3,
        [-30] = 4,
        [30] = 5,
        [90] = 6,
        [150] = 7,
        [210] = 8,
        [270] = 9,
    };
    public Dictionary<int, int> Postox { get { return postox; } }
    readonly Dictionary<int, int> postoy = new Dictionary<int, int>()
    {
        [-270] = 9,
        [-210] = 8,
        [-150] = 7,
        [-90] = 6,
        [-30] = 5,
        [30] = 4,
        [90] = 3,
        [150] = 2,
        [210] = 1,
        [270] = 0,
    };
    public Dictionary<int, int> Postoy { get { return postoy; } }
    List<GridData> swGridList = new List<GridData>();//临时展示在面包上的格子
    public GridGroup_Ground gridGroup_Ground;//主面板数据
    public static GridGroupMgr Inst;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        preproot = ResourceMgr.Instance.LoadRes<Image>("Prefab/addgridbg");
        mingrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockmin");
        defgrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockno");
        usegrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block");
        swgrid = ResourceMgr.Instance.LoadRes<Image>("Prefab/blocksw");
        GameGloab.Sprites["usegrid"] = usegrid.sprite;
        GameGloab.Sprites["mingrid"] = mingrid.sprite;
        GameGloab.Sprites["defgrid"] = defgrid.sprite;
        GameGloab.Sprites["swgrid"] = swgrid.sprite;

        gridGroup_Ground = new GridGroup_Ground();
    }
    /// <summary>
    /// 还原临时显示的grid
    /// </summary>
    public void RevertswGrid()
    {
        foreach (var v in swGridList)
        {
            v.Revert();
        }
    }
    /// <summary>
    /// 检测现在的位置能不能放
    /// </summary>
    public void CheckAvailable(Vector2 pos)
    {
        var gdata = DragingGridMgr.Inst.gridData;
        var alldata = gridGroup_Ground;
        //根据 pos 计算出 i j 对应的grid
        int x = OutGridPos(pos.x);
        if (!Postox.ContainsKey(x))
        {
            RevertswGrid();
            return;//超出 不处理
        }
        int y = OutGridPos(pos.y);
        if (!Postoy.ContainsKey(y))
        {
            RevertswGrid();
            return;//超出 不处理
        }
        int _i = Postoy[y];
        int _j = Postox[x];
        //y是行数 x是列数
        Debug.Log(y + "   " + x + "  " + _i + "   " + _j);
        //当前选中的位置 根据拖动出来的展开获取需要处理的grid

        for (int i = 0; i < gdata.H_count; i++)
        {
            for (int j = 0; j < gdata.W_count; j++)
            {
                if (gdata.DataArray[i, j] == 1)
                {
                    //有数据的情况
                }
            }
        }

        var grid = alldata.Grid[_i, _j];
        if (grid != null)
        {
            swGridList.Add(grid);
            grid.Status = 2;
        }
    }

    public static int OutGridPos(float index)
    {
        //x -270 到 270 为0到9   y 270 到 -270 为0到9
        //x    : -270  -210  -150  -90  -30   30   90   150   210   270
        //30倍数   -9    -7    -5   -3   -1    1   3     5     7     9
        //        0       1     2    3    4    5    6    7    8      9   
        // 坐标数除30 得到奇数向下取整  偶数向上取整
        float num = index / 30;//30倍数
        int p_n = num > 0 ? 1 : -1;//正负值
        float num_abs = M_math.Abs(num);
        int endind = 0;
        if (M_math.Even((int)num_abs))
            endind = (int)(30 * p_n * Math.Ceiling(num_abs));//向上取整
        else
            endind = (int)(30 * p_n * (float)Math.Floor(num_abs));//向下取整
        if (M_math.Abs(endind - index) < 20)
            return endind;
        else
            return 0;
    }
}
