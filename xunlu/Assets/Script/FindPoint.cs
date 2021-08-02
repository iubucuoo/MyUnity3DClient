using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPoint : MonoBehaviour
{
    Vector3 topot = Vector3.zero;
    int index = 0;
    [SerializeField] private List<GameObject> lists = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        topot = lists[index].transform.position + new Vector3(0, 1, 0);
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
        }
        else
        {
            transform.position = transform.position + dir.normalized * Time.deltaTime * 5;
            transform.forward = Vector3.Lerp(transform.forward, dir.normalized, Time.deltaTime * 15);
        }
    }
    
}
