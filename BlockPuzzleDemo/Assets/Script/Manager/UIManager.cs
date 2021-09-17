using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst;
    private void Awake()
    {
        Inst = this;
    }
    public GameObject setpanel;
    public GameObject gameoverpanel;

    public void OpenGameOverPanel()
    {
        gameoverpanel.SetActive(true);
    }
    public void OnBtnSetHide()
    {
        AudioManager.Inst.ButtonClick();
        setpanel.SetActive(false);
    }
    public void OnBtnSetSw()
    {
        AudioManager.Inst.ButtonClick();
        setpanel.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Inst.isPlaying_Music = PlayerPrefs.GetInt("MusicIsOn", 0) == 0;
        AudioManager.Inst.isPlaying_Sound = PlayerPrefs.GetInt("SoundIsOn", 0) == 0;
    }
     
}
