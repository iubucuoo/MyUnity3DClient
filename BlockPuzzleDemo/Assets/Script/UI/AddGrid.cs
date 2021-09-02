using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddGrid : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsUse;
    [SerializeField]
    GridData gridData;
    // Start is called before the first frame update
    void Start()
    {
         
    }
     
    
    public void SetGridData(GridData v)
    {
        gridData = v;
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        DragingGridMgr.Instance.SetDragDown(gridData);
        Debug.Log("OnPointerDown   " + transform.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DragingGridMgr.Instance.SetDragUp(gridData);
        Debug.Log("OnPointerUp   " + transform.name);
    }
}
