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
    void StartAddGrid()
    {
        var data = new GridData();
        float width = gridmin.rectTransform.rect.width;
        float height = gridmin.rectTransform.rect.height;

        for (int i = 0; i < 3; i++)
        {
            var obj =Instantiate(addgridbg);
            obj.transform.parent = addroot;
            obj.transform.localPosition = new Vector2((i - 1) * width * 7, 0);
            obj.name = i.ToString();
            var addgriddata = obj.gameObject.AddComponent<AddGrid>();
            addgriddata.SetGridData(data);
            int _width = 0;
            int _height = 4;
            var info = data.DataArray;
            for (int j = 0; j < info.Length; j++)
            {
                int _id = info[j];
                if (_id == 1)
                {
                    Image bg = Instantiate(gridmin);
                    bg.transform.parent = obj.transform;
                    bg.transform.localPosition = new Vector3(_width * width - (5 * width * 0.5f) + width * 0.5f, _height * height - (5 * height * 0.5f) + height * 0.5f);
                }
                _width++;
                if (_width == 5)
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
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Image bg = Instantiate(gridno);
                bg.transform.parent = bgroot;
                bg.transform.localPosition = new Vector2(i * width - (10 * width * 0.5f) + width * 0.5f, j * height - (10 * height * 0.5f) + height * 0.5f);
            }
        }
    }
    void OnBtnStart()
    {
        Debug.Log("开始游戏");
        img_bg.gameObject.SetActive(false);
        btn_start.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgrectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos))
            {
                //rectTransform.anchoredPosition = pos;
                Debug.Log("鼠标相对于bgroot的ui位置" + pos);
            }
        }
    }
}
