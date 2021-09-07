using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrepAddGridGroup : MonoBehaviour
{
    public Transform Root;
    public bool IsUse;
    [SerializeField]
    GroupBase gridData;
    // Start is called before the first frame update
    void Start()
    {
        EventTriggerListener.Get(gameObject).onDown = OnPointerDown;
        EventTriggerListener.Get(gameObject).onUp = OnPointerUp;
    }
     
    
    public void SetGridData(GroupBase v)
    {
        gridData = v;
    }
    public void OnPointerUp(GameObject eventData)
    {
        DragingGridMgr.Inst.SetDragUp(gridData);
        Debug.Log("OnPointerUp   " + transform.name);
    }
    public void OnPointerDown(GameObject eventData)
    {
        DragingGridMgr.Inst.SetDragDown(gridData);
        Debug.Log("OnPointerDown   " + transform.name);
    }
}
