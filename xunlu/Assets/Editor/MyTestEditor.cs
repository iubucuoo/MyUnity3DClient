using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyTestEditor : Editor
{

    [MenuItem("Builder/Refresh")]
    public static void Refre()
    {
        AssetDatabase.Refresh();
    }
    [MenuItem("Builder/PointAllAssetPath")]
    public static void Builder()
    {
        
        //var _ResPaths = AssetDatabase.GetAllAssetPaths();
        //for (int i = 0; i < _ResPaths.Length; i++)
        //{
        //    if (i < 50) { 
        //    var temp = _ResPaths[i];
        //    Debug.Log(temp);
        //    }
        //}
    }

    
}
