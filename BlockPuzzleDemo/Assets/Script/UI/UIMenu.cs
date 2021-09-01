using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public Image img_bg;
    public Button btn_start;
    // Start is called before the first frame update
    void Start()
    {
        btn_start.onClick.AddListener(OnBtnStart);
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
        
    }
}
