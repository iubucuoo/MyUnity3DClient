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

    Vector2 DragUp;
    // Start is called before the first frame update
    void Start()
    {
        DragUp = new Vector2(0, 200);
        canvas = GetComponent<Canvas>();
        DragingGridMgr.Instance.DragRoot = transform.Find("DrogRoot");
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.frameCount % 10 == 0 && DragingGridMgr.Instance.IsDrag)//隔10针检测一次
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
                {
                    DragPos = pos+ DragUp;
                }
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos1))
                {
                    Debug.Log("鼠标相对于bgroot的ui位置" + pos1);
                    CheckAddGrid();
                }
                //检测能不能放到空格子里
                //根据拖动物体的位置跟 BGROOT 位置比较
            }
        }

        //手机端 检测touch
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Moved &&
            Time.frameCount % 10 == 0 &&
            DragingGridMgr.Instance.IsDrag)
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
        if (DragingGridMgr.Instance.IsDrag)
        {
            if (DragPos != OldDragPos)
            {
                DragingGridMgr.Instance.DragRoot.localPosition = DragPos;
                OldDragPos = DragPos;
            }
        }
    }
    void CheckAddGrid()
    {

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
