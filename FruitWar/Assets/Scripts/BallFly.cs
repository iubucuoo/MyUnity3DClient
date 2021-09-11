﻿using UnityEngine;
using System.Collections;

public class BallFly : MonoBehaviour {
	
	const float normalSpeed = 5f;
	
	// if speed smaller or greater than speed threshold, set the speed to value
	float speedMinThreshold;
	float speedMinValue;
	float speedMaxThreshold;
	float speedMaxValue;
	// if velocity angle smaller than angle threshold, add speed at y axis by value
	float angleThreshold;
	float multifyValue;

    float rotateSpeed = 50f;
	
	// speed will return to normal after resetTime
	private const float resetTime = 3f;
	
	// the probability of generating property
	float propertyProbability = 0.3f;

    public GameObject star;
    public AudioClip winAudio;
	
	// properties
	public GameObject[] properties;

    // in case the ball is shot when user clicks reset
    float startTime;

    bool drunk;

	bool verticalHint = false;
	
	void Start () {
        Reset();
        Random.seed = (int)startTime;
	}
	
	void Update () {
		// Control the ball.
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_METRO
		bool fire = Input.GetButtonUp("Fire1");
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
        bool fire = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
#else
        bool fire = false;
#endif
		if (fire && !Manager.Released 
                && Time.timeScale > 0f && Time.time > startTime + 0.5) {
			Debug.Log("Fire");

            // shoot the ball at the direction
            float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            var direct = new Vector2(- Mathf.Sin(angle), - Mathf.Cos(angle));
			GetComponent<Rigidbody2D>().velocity = direct * normalSpeed;
			
			// mark as released
			Manager.Released = true;
        }
        // rotating the ball
        if(!Manager.Released)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.PingPong(Time.time * rotateSpeed, 120) - 60);
        }
	}
	
	/* 	This method is to avoid the situations below:
	 * 	1. where ball flies too slow or too fast after collision
	 * 	2. where ball flies horizontally
	 *  3. where ball flies vertically
	 */
	void FixedUpdate () {
        
		if (Manager.Released) 
        {
			Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		
			// to solve problem 1
			float speed = velocity.sqrMagnitude;
			if (speed < speedMinThreshold) {
				velocity.Normalize();
				GetComponent<Rigidbody2D>().velocity = velocity * speedMinValue;
			}
			else if(speed > speedMaxThreshold) {
				velocity.Normalize();
				GetComponent<Rigidbody2D>().velocity = velocity * speedMaxValue;
			}
			
			// to solve problem 2
			float angle = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
			if (Mathf.Abs(angle) < angleThreshold) {
				// Debug.Log("Angle amendment takes effect: "+angle);
				float amend = Mathf.Sign(velocity.y) * multifyValue;
				GetComponent<Rigidbody2D>().velocity = velocity + new Vector3(0, amend, 0);
			}

			// to detect problem 3 and hint
			angle = Mathf.Atan(velocity.x / velocity.y) * Mathf.Rad2Deg;
			if (Mathf.Abs(angle) < angleThreshold && !verticalHint) {
				GameUIHelper.Instance.DrawHint("尝试在球接触挡板时左右移动！", 2f);
				verticalHint = true;
			}

            // drunk ball mode
            if (drunk)
            {
                float force = 10f;
                // choose a random direction	
                float dirX = Random.Range(-0.8f, 0.8f);
			    float dirY = Mathf.Sqrt(1 - dirX * dirX);
			    Vector2 direct = new Vector2(dirX, dirY);
                GetComponent<Rigidbody2D>().AddForce(direct * force);
            }

        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
	}
	
	// for normal ball
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Brick") {
            if(Manager.Released)
                GetComponent<AudioSource>().Play();
			// drop brick
			other.rigidbody.isKinematic = false;	// let it fall
			other.collider.isTrigger = true;	// let it be transparent
			other.gameObject.tag = "FallBrick";	// to avoid a second contact
			// drop property
			float probable = Random.Range(0f, 1f);
			if(probable < propertyProbability){
				Debug.Log("ball drops a property");
				int index = Random.Range(0, properties.Length - 1);	// choose a property
				Instantiate(properties[index], transform.position, Quaternion.identity);
			}
		}
	}
	
	// for fireball
	void OnTriggerEnter2D(Collider2D other){
		// Debug.Log("fireball working");
		if (other.gameObject.tag == "Brick") {
			// drop brick
			other.GetComponent<Rigidbody2D>().isKinematic = false;	// let it fall
			other.GetComponent<Collider2D>().isTrigger = true;	// let it be transparent
			other.gameObject.tag = "FallBrick";	// to avoid a second contact
			// drop property
			float probable = Random.Range(0f, 1f);
			if(probable < propertyProbability){
				Debug.Log("fireball drops a property");
				int index = Random.Range(0, properties.Length - 1);	// choose a property
				Instantiate(properties[index], transform.position, Quaternion.identity);
			}
		}
	}
	
	void SetVariables () {	

        if (Manager.Released)
        {
            // recover previous speed
            Debug.Log("return to normal");
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * normalSpeed;
        }
        else
        {            
            Debug.Log("zero speed");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;    // zero ball speed
            GetComponent<Rigidbody2D>().angularVelocity = 0f;        // zero angular speed
        }
		// recover thresholds and values
		speedMinValue = normalSpeed - 1;
		speedMinThreshold = speedMinValue * speedMinValue;
		speedMaxValue = normalSpeed + 1;
		speedMaxThreshold = speedMaxValue * speedMaxValue;
		angleThreshold = 10f;
		multifyValue = 3f;
	}

	void SetSpeedByRatio (float ratio) {
		Debug.Log("set speed by "+ratio);

		// change the speed
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * ratio;
		// change thresholds and values to avoid speed restriction
		speedMinThreshold *= ratio * ratio;
		speedMinValue *= ratio;
		speedMaxThreshold *= ratio * ratio;
		speedMaxValue *= ratio;
		
		// call reset after some delay
		Invoke("SetVariables", resetTime);
	}
	
	void MakeFireBall (float time){
		GetComponent<Collider2D>().isTrigger = true;
		ConvertBall.work = true;
        StartCoroutine("FireBallCoroutine");
		Invoke ("LoseFireBall", time);
	}

    IEnumerator FireBallCoroutine()
    {
        var sr = GetComponent<SpriteRenderer>();
        Color color1 = sr.color;
        Color color2 = new Color(color1.r, color1.g, color1.b, 0.6f);
        while (true)
        {
            sr.color = color1;
            yield return new WaitForSeconds(0.3f);
            sr.color = color2;
            yield return new WaitForSeconds(0.1f);
        }
    }
	
	void LoseFireBall (){
        GameUIHelper.Instance.DrawProperty(null);
		GetComponent<Collider2D>().isTrigger = false;
		ConvertBall.work = false;
        StopCoroutine("FireBallCoroutine");
        GetComponent<SpriteRenderer>().color = Color.white;
	}

    void MakeDrunkBall(float time)
    {
        drunk = true;
        Invoke("LoseDrunkBall", time);
    }

    void LoseDrunkBall()
    {
        GameUIHelper.Instance.DrawProperty(null);
        drunk = false;
    }

    void DropStar()
    {
        Vector2 position = new Vector3(0, 2, 0);
        AudioSource.PlayClipAtPoint(winAudio, position);
        Instantiate(star, position, Quaternion.identity);
        GameUIHelper.Instance.DrawHint("吃掉星星，立即过关", 5f, blink: true);
    }

    void Reset()
    {
        // disable fireball
        LoseFireBall();
        // disable drunkball
        LoseDrunkBall();
        // reset speed
        SetVariables();        
    }

    void SetStartTime()
    {
        // set start time to now
        startTime = Time.time;
    }

}
