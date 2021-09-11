using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.tag == "Brick"){
            // drop brick
            other.GetComponent<Rigidbody2D>().isKinematic = false;	// let it fall
            other.GetComponent<Collider2D>().isTrigger = true;	// let it be transparent
            other.gameObject.tag = "FallBrick";	// to avoid a second contact
				
			Destroy(gameObject);	// destroy itself
		}
	}
}
