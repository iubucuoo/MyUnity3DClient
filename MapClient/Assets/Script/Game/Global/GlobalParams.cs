//============================================
//作 者:GK
//时 间:2017-03-12 17:57:49
//备 注:
//公 司:杭州白掌网络科技有限公司
//============================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalParams
{
    #region param    
    //Grid大小，Grid半径,图片大小，
    public const byte tile_width = 48, tile_helf_width = 24, tile_height = 32, tile_helf_height = 16, big_width =6, big_height = 6;
    public const ushort img_width = 512, img_height = 256;   

    public string[,] EquipRes= new string[10, 8] 
    {
    { "1_1","1_2","1_3","1_4","1_5","1_6","1_7","1_8"},
    { "2_1","2_2","2_3","2_4","2_5","2_6","2_7","2_8" } ,
    { "3_1", "3_2", "3_3", "3_4", "3_5","3_6","3_7","3_8" },
    { "4_1","4_2","4_3","4_4","4_5","4_6","4_7","4_8"},
    { "5_1","5_2","5_3","5_4","5_5","5_6","5_7","5_8"},
    { "6_1","6_2","6_3","6_4","6_5","6_6","6_7","6_8"},
    { "9_1","9_2","9_3","9_4","9_5","9_6","9_7","9_8"},
    { "13_1","13_2","13_3","13_4","13_5","13_6","13_7","13_8"},
    { "21_1","21_2","21_3","21_4","21_5","21_6","21_7","21_8"},
    { "22_1","22_2","22_3","22_4","22_5","22_6","22_7","22_8"}
    };  
    #endregion
           
    public bool is_touch_skill = false;
    
    public float splity = 0.66666667f;
    public volatile int CurrentSceneID;
    public float world_width;
    public float world_heigth;
    public int cid, sid;
    public int myPlayerEntityId, myHeroEntityId;

    
    public int IsHdType = 0;
    

    public bool IsInSafeArea = false;    
}