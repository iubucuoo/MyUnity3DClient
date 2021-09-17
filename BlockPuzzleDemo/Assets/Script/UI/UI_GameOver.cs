using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public Button btnRefresh;
    // Start is called before the first frame update
    void Start()
    {
        btnRefresh.onClick.AddListener(OnBtnRefresh);
    }

    private void OnBtnRefresh()
    {
        gameObject.SetActive(false);
        GridGroupMgr.Inst.GameReset();//重新启动游戏
        AudioManager.Instance.PlayGameOpen();
    }
}
