using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGloab
{
    public static int wh = 90;// 60;
    public static int wh_2 = 45;// 30;
    public static Vector2 OutScreenV2 = new Vector2(5000, 5000);
    public static Vector2 DragUp = new Vector2(0, GameGloab.wh * 4);//y高度 对应的倍数
    public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
 
    public static Transform root_bg;
    public static Transform root_prep;


}
