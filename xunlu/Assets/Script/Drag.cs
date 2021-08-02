using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public LayerMask cubelayer;
    public Transform cube;
    Vector3 camdir;
    // Start is called before the first frame update
    void Start()
    {
        camdir = Camera.main.transform.position - cube.position;
    }
    Transform clickgameObj;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo,100, cubelayer))
            {
                //划出射线，只有在scene视图中才能看到
                Debug.DrawLine(ray.origin, hitInfo.point,Color.red,2);
                 clickgameObj = hitInfo.collider.gameObject.transform;
                Debug.Log("click object name is " + clickgameObj.name);
                //当射线碰撞目标为boot类型的物品，执行拾取操作
                 
            }
        }
        if (clickgameObj!=null)
        {
            var dir = clickgameObj.position - cube.position;
            var Axis = Vector3.Cross(dir.normalized, camdir.normalized);
            float dotValue = Vector3.Dot(dir.normalized, camdir.normalized);
            var angle = Mathf.Acos(dotValue) * Mathf.Rad2Deg;
            
            Debug.DrawRay(cube.position, camdir, Color.green);
            Debug.DrawRay(cube.position, dir.normalized * 3, Color.blue);
            
            cube.RotateAround(cube.position,Axis, angle * Time.deltaTime * 5);
            //cube.Rotate( Axis, angle * Time.deltaTime*5,Space.World);
        }
    }
}
