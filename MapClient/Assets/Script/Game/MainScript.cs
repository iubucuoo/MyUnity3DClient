using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    internal static MainScript inst;
    internal Camera _camera;
    private void Awake()
    {
        inst = this;
        StageCamera.CheckMainCamera();
    }
    void InitCameraUI()
    {
        _camera = GameObject.Find("Stage Camera").GetComponent<Camera>();
        _camera.backgroundColor = Color.black;
        _camera.clearFlags = CameraClearFlags.SolidColor;
        _camera.transform.parent = transform;
        _camera.depth = 8;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitCameraUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
