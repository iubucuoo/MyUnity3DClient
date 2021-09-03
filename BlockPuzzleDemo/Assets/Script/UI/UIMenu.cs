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
        EventTriggerListener.Get(gameObject).onDrag = OnDragUI;
        canvas = GetComponent<Canvas>();
        bgroot = transform.Find("BGROOT");
        addroot = transform.Find("ADDROOT");
        bgrectTransform = bgroot.transform as RectTransform;
        addgridbg = ResourceMgr.Instance.LoadRes<Image>("Prefab/addgridbg");
        grid = ResourceMgr.Instance.LoadRes<Image>("Prefab/block");
        gridmin = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockmin");
        gridno = ResourceMgr.Instance.LoadRes<Image>("Prefab/blockno");

        StartBg();
        StartAddGrid();
        btn_start.onClick.AddListener(OnBtnStart);
    }
    int dragtime = 0;
    int dragcount = 0;
    void OnDragUI(GameObject go)//拖动事件 当手指移动的时候调用
    {
        dragtime++;
        if (dragtime % 10 == 0)//隔10针检测一次
        {
            //Debug.Log(dragcount++);

            if (DragingGridMgr.Instance.IsDrag)
            {
                //检测能不能放到空格子里
                //根据拖动物体的位置跟 BGROOT 位置比较
            }
            dragtime = 0;
        }
    }

    void StartAddGrid()
    {
        var data = new GridGroup();
        float width = gridmin.rectTransform.rect.width;
        float height = gridmin.rectTransform.rect.height;

        for (int i = 0; i < 3; i++)
        {
            var obj = Instantiate(addgridbg);
            obj.transform.parent = addroot;
            obj.transform.localPosition = new Vector2((i - 1) * width * (GameGloab.min_Grid_width+2), 0);
            obj.name = i.ToString();
            var addgriddata = obj.gameObject.AddComponent<AddGrid>();
            addgriddata.SetGridData(data);
            int _width = 0;
            int _height = GameGloab.min_Grid_height - 1; ;
            var info = data.DataArray;
            for (int j = 0; j < info.Length; j++)
            {
                int _id = info[j];
                if (_id == 1)
                {
                    Image bg = Instantiate(gridmin);
                    bg.transform.parent = obj.transform;
                    bg.transform.localPosition =new Vector3((_width- GameGloab.min_Grid_width * 0.5f+ 0.5f) * width , (_height - GameGloab.min_Grid_height * 0.5f + 0.5f) * height );
                }
                _width++;
                if (_width == GameGloab.min_Grid_width)
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
        for (int i = 0; i < GameGloab.Grid_width; i++)
        {
            for (int j = 0; j < GameGloab.Grid_height; j++)
            {
                Image bg = Instantiate(gridno);
                bg.transform.parent = bgroot;
                bg.transform.localPosition = new Vector2((i - GameGloab.Grid_width * 0.5f+ 0.5f) * width , (j- GameGloab.Grid_height * 0.5f+ 0.5f) * height) ;
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

    }
}
