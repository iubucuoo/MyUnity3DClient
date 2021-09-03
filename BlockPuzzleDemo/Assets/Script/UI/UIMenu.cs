using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    Transform bgroot;
    Transform addroot;
    Image addgridbg;
    Image grid;
    Image gridmin;
    Image gridno;
    Canvas canvas;
    RectTransform bgrectTransform;
    public Image img_bg;
    public Button btn_start;

    // Start is called before the first frame update
    void Start()
    {
        img_bg.gameObject.SetActive(true);
        //EventTriggerListener.Get(img_bg.gameObject).onDrag = OnDragUI;
        canvas = GetComponent<Canvas>();
        DragingGridMgr.Instance.DragRoot = transform.Find("DrogRoot");
        bgroot = transform.Find("BGROOT");
        addroot = transform.Find("ADDROOT");
        bgrectTransform = gameObject.GetComponent< RectTransform>();
        addgridbg = ResourceMgr.Instance.LoadRes<Image>("Prefab/addgridbg");
        grid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block");
        gridmin = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockmin");
        gridno = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockno");

        StartBg();
        StartAddGrid();
        btn_start.onClick.AddListener(OnBtnStart);
    }
    //int dragtime = 0;
    //int dragcount = 0;
    //void OnDragUI(GameObject go)//拖动事件 当手指移动的时候调用
    //{
    //    dragtime++;
    //    if (dragtime % 10 == 0)//隔10针检测一次
    //    {
    //        Debug.Log(DragingGridMgr.Instance.IsDrag);
    //        if (DragingGridMgr.Instance.IsDrag)
    //        {
    //            Debug.Log(dragcount++);
    //            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
    //            {
    //                DragingGridMgr.Instance.DragRoot.localPosition = pos;
    //                //rectTransform.anchoredPosition = pos;
    //                Debug.Log("鼠标相对于bgroot的ui位置" + pos);
    //            }
    //            //检测能不能放到空格子里
    //            //根据拖动物体的位置跟 BGROOT 位置比较
    //        }
    //        dragtime = 0;
    //    }
    //}

    void StartAddGrid()
    {
        float width = gridmin.rectTransform.rect.width;
        float height = gridmin.rectTransform.rect.height;

        for (int i = 0; i < 3; i++)
        {
            var data = new GridGroup();
            var obj = Instantiate(addgridbg);
            obj.transform.parent = addroot;
            Vector2 pos = new Vector2((i - 1) * width * (GameGloab.min_wcount + 2), 0);
            obj.transform.localPosition = pos;
            obj.name = i.ToString();
            var addgriddata = obj.gameObject.AddComponent<AddGridGroup>();
            data.Pos = pos;
            addgriddata.SetGridData(data);
            int _width = 0;
            int _height = GameGloab.min_hcount - 1;
            var info = data.DataArray;
            for (int j = 0; j < info.Length; j++)
            {
                int _id = info[j];
                if (_id == 1)
                {
                    Image bg = Instantiate(gridmin);
                    bg.transform.parent = obj.transform;
                    bg.transform.localPosition = new Vector3((_width - GameGloab.min_wcount * 0.5f + 0.5f) * width, (_height - GameGloab.min_hcount * 0.5f + 0.5f) * height);
                }
                _width++;
                if (_width == GameGloab.min_wcount)
                {
                    _width = 0;
                    _height--;
                }
            }
        }
    }
    void StartBg()
    {
        float width = grid.rectTransform.rect.width;
        float height = grid.rectTransform.rect.height;
        //bg
        for (int i = 0; i < GameGloab.w_count; i++)
        {
            for (int j = 0; j < GameGloab.h_count; j++)
            {
                Image bg = Instantiate(gridno);
                bg.transform.parent = bgroot;
                bg.transform.localPosition = new Vector2((i - GameGloab.w_count * 0.5f + 0.5f) * width, (j - GameGloab.h_count * 0.5f + 0.5f) * height);
            }
        }
    }
    void OnBtnStart()
    {
        Debug.Log("开始游戏");

        btn_start.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
        //    {
        //        //rectTransform.anchoredPosition = pos;
        //        Debug.Log("鼠标相对于bgroot的ui位置" + pos);
        //    }
        //}
        if (Input.GetMouseButton(0))
        {

            if (Time.frameCount % 10 == 0)//隔10针检测一次
            {

                if (DragingGridMgr.Instance.IsDrag)
                {

                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
                    {
                        DragingGridMgr.Instance.DragRoot.position = pos;
                        //rectTransform.anchoredPosition = pos;
                        Debug.Log("鼠标相对于bgroot的ui位置" + pos);
                    }
                    //检测能不能放到空格子里
                    //根据拖动物体的位置跟 BGROOT 位置比较
                }
            }
        }
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                 
                if (Time.frameCount % 10 == 0)//隔10针检测一次
                {

                    if (DragingGridMgr.Instance.IsDrag)
                    {

                        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
                        {
                            DragingGridMgr.Instance.DragRoot.position = pos;
                            //rectTransform.anchoredPosition = pos;
                            Debug.Log("鼠标相对于bgroot的ui位置" + pos);
                        }
                        //检测能不能放到空格子里
                        //根据拖动物体的位置跟 BGROOT 位置比较
                    }
                }
            }
        }

    }
}
