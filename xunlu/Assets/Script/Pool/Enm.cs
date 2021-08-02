using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enm : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 5000));
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward  * 15);
        if (transform.position.z>100 || transform.position.y<-40)
        {
            transform.position = Vector3.zero;
            transform.eulerAngles =Vector3.zero; GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
