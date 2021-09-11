﻿using UnityEngine;
using System.Collections;

public class DrunkBall : MonoBehaviour {

    private float time = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pad")
        {
            Debug.Log("get drunkball");
            GameUIHelper.Instance.DrawProperty(GetComponent<SpriteRenderer>().sprite);
            GameUIHelper.Instance.DrawHint("喝醉了");
            GameObject.Find("Ball").SendMessage("MakeDrunkBall", time);
        }
    }
}
