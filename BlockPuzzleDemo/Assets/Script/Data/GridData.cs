using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridData
{
    public bool IsUse;//判断是否存放了格子
    public Color g_coolr;//格子的颜色
    public Image image;
    Sprite _sprite;
    public Sprite sprite { get { return _sprite; }set { _sprite = value; if (image != null) { image.sprite = _sprite; } } }
}
