using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapUnit))]
public class MapUnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("刷新"))
        {
            var mapunit = target as MapUnit;
            mapunit.InItMap();
        }
    }
}
