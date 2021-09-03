using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddGridGroup : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsUse;
    [SerializeField]
    GridGroup gridData;
    // Start is called before the first frame update
    void Start()
    {
         
    }
     
    
    public void SetGridData(GridGroup v)
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
