using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour
{
    public GameObject p1, p2;
    Vector3 topot =Vector3.zero;
    int index = 0;
    [SerializeField] private List<GameObject> lists = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        topot = lists[index].transform.position + new Vector3(0, 1, 0);
        Drawtree(10, 100, 200, 20, 90);
    }
    // Update is called once per frame
    void Update()
    {
        //移动
        var dir = topot - transform.position;
        if (dir.magnitude < 0.1f)
        {
            if (lists.Count > ++index)
            {
                topot = lists[index].transform.position + new Vector3(0, 1, 0);
                if (index - 1 >= 0)
                {
                    lists[index - 1].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
            else
            {
                index = 0;
            }
        }
        else
        {
            transform.position = transform.position + dir.normalized * Time.deltaTime * 5;
            transform.forward = Vector3.Lerp(transform.forward, dir.normalized, Time.deltaTime * 15);
        }
    }

    void Drawtree(int n,double x0,double y0,double leng, double th)
    {
        if (n==0)
        {
            return;
        }
        double x1 = x0 + leng * System.Math.Cos(th);
        double y1 = y0 + leng * System.Math.Sin(th);
        Drawline((float)x0, (float)y0, (float)x1, (float)y1, n / 2);
        Drawtree(n - 1, x1, y1, Random.value * leng, th + Random.value);
        Drawtree(n - 1, x1, y1, Random.value * leng, th - Random.value);
        
     
    }
    void Drawline(float x0, float y0, float x1, float y1, int width) {
        var xxx = new Vector3(x0, y0, 0);
        var xxx1 = new Vector3(x1, y1, 0);
        //var _gameObject  = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //_gameObject.transform.position = xxx;
        //var _gameObject1  = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //_gameObject1.transform.position = xxx;
        Debug.DrawLine(xxx,xxx1, Color.green,100);
    }
}
