using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    Transform bgroot;
    Transform addroot;
    Canvas canvas;
    RectTransform CanvasRectTransform;
    RectTransform bgrectTransform;
    public Button btn_start;
    Dictionary<int,int> postox = new Dictionary<int, int>()
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
    Dictionary<int, int> postoy = new Dictionary<int, int>()
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
    Vector2 DragUp = new Vector2(0, 0);//y高度 对应60的倍数
    List<GridData> swGridList = new List<GridData>();
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        DragingGridMgr.Inst.DragRoot = transform.Find("DrogRoot");
        bgroot = transform.Find("BGROOT");
        addroot = transform.Find("ADDROOT");
        bgrectTransform = bgroot.GetComponent<RectTransform>();
        CanvasRectTransform = gameObject.GetComponent<RectTransform>();
        btn_start.onClick.AddListener(OnBtnStart);
    }

    PrepAddGridGroup[] PrepGroup = new PrepAddGridGroup[3];
    void StartAddGroupRoot()
    {
        var rootbg = GridGroupMgr.Inst.preproot;
        for (int i = 0; i < 3; i++)
        {
            Vector2 pos = new Vector2((i - 1) * 210, 0);
            var obj = Instantiate(rootbg);
            obj.transform.parent = addroot;
            obj.transform.localPosition = pos;
#if UNITY_EDITOR
            obj.name = i.ToString();
#endif
            PrepGroup[i] = obj.transform.GetComponent<PrepAddGridGroup>();
            if (PrepGroup[i] == null)
            {
                PrepGroup[i] = obj.gameObject.AddComponent<PrepAddGridGroup>();
            }
            PrepGroup[i].Root = obj.transform;
        }

    }

    void OnBtnStart()
    {
        Debug.Log("开始游戏");
        StartAddGroupRoot();
        btn_start.gameObject.SetActive(false);
        StartBg();
        RefreshGridGroup();
    }

    void StartBg()
    {
        GridTools.AddGrids(bgroot, GridGroupMgr.Inst.gridGroup_Ground, GridGroupMgr.Inst.defgrid);
    }

    Vector3 oldmousepos;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos1))
            {
                int posx = OutPos(pos1.x);
                int posy = OutPos(pos1.y);
                if (postox.ContainsKey(posx) && postoy.ContainsKey(posy))
                {
                    Debug.Log("鼠标相对于bgroot的ui位置" + pos1 + "     " +  posy + "   " + posx + "     " + postoy[posy] + "   " + postox[posx]);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.frameCount % 10 == 0 && DragingGridMgr.Inst.IsDrag)//隔10针检测一次
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
                {
                    DragPos = pos + DragUp;
                }
                if ((oldmousepos - Input.mousePosition).sqrMagnitude > 90)
                {
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos1))
                    {
                        //Debug.Log("鼠标相对于bgroot的ui位置" + pos1 + (oldmousepos - Input.mousePosition).sqrMagnitude);
                        CheckAddGrid(pos1);
                    }
                    oldmousepos = Input.mousePosition;
                }
                //检测能不能放到空格子里
                //根据拖动物体的位置跟 BGROOT 位置比较
            }
        }

        //手机端 检测touch
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Moved &&
            Time.frameCount % 10 == 0 &&
            DragingGridMgr.Inst.IsDrag)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
            {
                DragPos = pos + DragUp;
            }
            //检测能不能放到空格子里
            //根据拖动物体的位置跟 BGROOT 位置比较
        }
    }

    Vector2 DragPos;
    Vector2 OldDragPos;
    private void FixedUpdate()
    {
        if (DragingGridMgr.Inst.IsDrag)
        {
            if (DragPos != OldDragPos)
            {
                DragingGridMgr.Inst.DragRoot.localPosition = DragPos;
                OldDragPos = DragPos;
            }
        }
    }
    void CheckAddGrid(Vector2 pos)
    {
        var gdata = DragingGridMgr.Inst.gridData;
        var alldata = GridGroupMgr.Inst.gridGroup_Ground;
        //根据 pos 计算出 i j 对应的grid
        int x = OutPos(pos.x);
        if (! postox.ContainsKey(x))
        {
            foreach (var v in swGridList)
            {
                v.Revert();
            }
            return;//超出 不处理
        }
        int y = OutPos(pos.y);
        if (! postoy.ContainsKey(y))
        {
            foreach (var v in swGridList)
            {
                v.Revert();
            }
            return;//超出 不处理
        }
        int _i = postoy[y];
        int _j = postox[x];
        //y是行数 x是列数
        Debug.Log(y + "   " + x+"  "+ _i + "   " + _j );
        //当前选中的位置 根据拖动出来的展开获取需要处理的grid

        for (int i = 0; i < gdata.H_count; i++)
        {
            for (int j = 0; j < gdata.W_count; j++)
            {
                if (gdata.DataArray[i,j]==1)
                {
                    //有数据的情况
                }
            }
        }

        var grid = alldata.Grid[_i, _j];
        if (grid!=null)
        {
            grid.Status = 2;
        }
    }

    private static int OutPos(float index)
    {
        //x -270 到 270 为0到9   y 270 到 -270 为0到9
        //x    : -270  -210  -150  -90  -30   30   90   150   210   270
        //30倍数   -9    -7    -5   -3   -1    1   3     5     7     9
        //        0       1     2    3    4    5    6    7    8      9   
        // 坐标数除30 得到奇数向下取整  偶数向上取整
        float num = index / 30;//30倍数
        int isz = num > 0 ? 1 : -1;
        float jdz = M_math.Abs(num);
       
        int endind = 0;
        if (M_math.Even((int)jdz))
        {
            endind =(int)(30 * isz * Math.Ceiling(jdz));//向上取整
        }
        else
        {
            endind =(int)(30 * isz * (float)Math.Floor(jdz));//向下取整
        }
        //Debug.Log(endind+" -  "+ index + "  =  "+ (Math.Abs(endind - index)));
              
        if (M_math.Abs(endind - index) < 20)
        {
            return endind;
        }
        else
            return 0;
    }

    void RefreshGridGroup()
    {
        var gird = GridGroupMgr.Inst.mingrid;
        for (int i = 0; i < 3; i++)
        {
            var trs = PrepGroup[i].Root;
            var data = new GridGroup_Prep();
            data.ParentRoot = trs.localPosition;
            PrepGroup[i].SetGridData(data);
            GridTools.AddGrids(trs, data, gird);
        }
    }
}
