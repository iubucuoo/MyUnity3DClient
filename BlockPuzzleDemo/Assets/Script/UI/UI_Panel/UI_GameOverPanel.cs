using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOverPanel : UIPanelBase
{
    public GameObject gameover;
    public GameObject newrecord;
    public Text newrecordtxt;
    public Button btnRefresh;
    // Start is called before the first frame update
    void Start()
    {
        btnRefresh.onClick.AddListener(OnBtnRefresh);
    }
    public void ShowGameOver()
    {
        gameObject.SetActive(true);
        bool isnewrecord = UIManager.Inst.IsTopScore();
        gameover.SetActive(!isnewrecord);
        newrecord.SetActive(isnewrecord);
        if (isnewrecord)
        {
            AudioManager.Inst.PlayNewRecord();//播放 新记录音乐UI
            newrecordtxt.text = GameGloab.Topscore.ToString();
        }
        else
        {
            AudioManager.Inst.PlayGameOver();
        }
    }
    private void OnBtnRefresh()
    {
        GridGroupMgr.Inst.GameReset();//重新启动游戏
        AudioManager.Inst.PlayGameOpen();
        UIManager.Inst.ResetTop();
        gameObject.SetActive(false);
    }
}
