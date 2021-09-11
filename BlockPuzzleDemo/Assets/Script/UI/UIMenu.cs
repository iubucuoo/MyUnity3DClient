using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    Canvas canvas;
    RectTransform rectTr_canvas;
    RectTransform rectTr_bg;
    public Button btn_start;
  
    Vector2 DragUp = new Vector2(0, 180);//y高度 对应60的倍数
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        rectTr_bg = GameGloab.root_bg.GetComponent<RectTransform>();
        rectTr_canvas = gameObject.GetComponent<RectTransform>();
        btn_start.onClick.AddListener(OnBtnStart);
    }

    PrepAddGridGroup[] PrepGroup = new PrepAddGridGroup[3];
    void StartAddGroupRoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 pos = new Vector2((i - 1) * 210, 0);
            var obj = ObjectMgr.InstantiateGameObj(ObjectMgr.LoadResource("Prefab/addgridbg")as GameObject);
            obj.transform.parent = GameGloab.root_prep;
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
        GridGroupMgr.Inst.gridGroup_Ground.CreatGrids(GameGloab.root_bg);
        //GridTools.CreatGrids(GameGloab.root_bg, GridGroupMgr.Inst.gridGroup_Ground, GridGroupMgr.Inst.defgrid);
    }
    void RefreshGridGroup()
    {
        List<int[,]> datalist = new List<int[,]>();
        datalist.Add(new int[,]{
            { 1,1 },
            { 0,1 },
        });
        datalist.Add(new int[,]{
            { 1,1 },
            { 1,0 },
        });
        datalist.Add(new int[,]{
            { 0,1 },
            { 1,1 },
        });
        datalist.Add(new int[,]{
            { 1,0 },
            { 1,1 },
        });
        
        for (int i = 0; i < 3; i++)
        {
            PrepGroup[i].Reset();
            var trs = PrepGroup[i].Root;
            var data = PoolMgr.Allocate(IPoolsType.GridGroup_MinPrep)as GridGroup_MinPrep;
            data.SetData(datalist[UnityEngine.Random.Range(0, 4)]);
            data.ParentRoot = trs.localPosition;
            PrepGroup[i].SetGridData(data);
            data.CreatGrids(trs);
        }
    }

    Vector3 oldmousepos;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos1))
        //    {
        //        int posx = GridGroupMgr.OutGridPos(pos1.x);
        //        int posy = GridGroupMgr.OutGridPos(pos1.y);
        //        if (GridGroupMgr.Inst.Postox.ContainsKey(posx) && GridGroupMgr.Inst.Postoy.ContainsKey(posy))
        //        {
        //            Debug.Log("鼠标相对于bgroot的ui位置" + pos1 + "     " +  posy + "   " + posx + "     " + GridGroupMgr.Inst.Postoy[posy] + "   " + GridGroupMgr.Inst.Postox[posx]);
        //        }
        //    }
        //}
        if (Input.GetMouseButton(0))
        {
            if (Time.frameCount % 10 == 0 && DragingGridMgr.Inst.IsDrag)//隔10针检测一次
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTr_canvas, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
                {
                    DragPos = pos + DragUp;
                }
                if ((oldmousepos - Input.mousePosition).sqrMagnitude > 90)
                {
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTr_bg, Input.mousePosition, canvas.worldCamera, out Vector2 pos1))
                    {
                        //Debug.Log("鼠标相对于bgroot的ui位置" + pos1 + (oldmousepos - Input.mousePosition).sqrMagnitude);
                        GridGroupMgr.Inst.CheckAvailable(pos1 + DragUp);
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
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTr_bg, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
            {
                DragPos = pos + DragUp;
            }
            //检测能不能放到空格子里
            //根据拖动物体的位置跟 BGROOT 位置比较
        }
    }

    Vector2 DragPos;
    Vector2 OldDragPos;
    void FixedUpdate()
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
}
