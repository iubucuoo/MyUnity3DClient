﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetGame: MonoBehaviour {

    private static SetGame _instance;   // singleton

	// Collider components
	public Transform leftWall;
	public Transform rightWall;
	public Transform topWall;
	public Transform bottomWall;

	// set pad and ball to original position
	private Transform pad = null;
	private Transform ball = null;
	
	// record some values
	private float leftPos;
	private float rightPos;
	private float topPos;
	private float bottomPos;
	
	// the bricks
	public GameObject brick;
	float spacePercentage = 0.2f;	// how many left blank
    public Sprite[] brickSprites;
    List<GameObject> bricks = null; // used to recycle garbage

    private SetGame() {}

    public static SetGame Instance
    {
        get
        {
            return _instance;
        }
    }

	void Awake () {
        _instance = this;
		pad = GameObject.Find("Pad").transform;
		ball = GameObject.Find("Ball").transform;
		Random.seed = System.DateTime.Now.Millisecond;
	}

	// Use this for initialization
	void Start () {
		// get the border
        Camera mainCam = Camera.main;
		leftPos = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        rightPos = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        topPos = mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        bottomPos = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
		
		// set walls, all leave some spaces, the bottom one can have more
		leftWall.position = new Vector3(leftPos - leftWall.GetComponent<Collider2D>().bounds.size.x, 0, 0);
		rightWall.position = new Vector3(rightPos + rightWall.GetComponent<Collider2D>().bounds.size.x, 0, 0);
		topWall.position = new Vector3(0, topPos + topWall.GetComponent<Collider2D>().bounds.size.y, 0);
		bottomWall.position = new Vector3(0, bottomPos - bottomWall.GetComponent<Collider2D>().bounds.size.y - pad.GetComponent<Collider2D>().bounds.size.y, 0);      // leave space

        bricks = new List<GameObject>();
        Reset();    // reset pad, ball and bricks
	}

    public void Reset()
    {
        // set the pad and the ball
        SetPadAndBall();

        // before spawn new bricks, remove old bricks and collect garbage
        foreach (var b in bricks)
        {
            Destroy(b);
        }
        bricks.Clear();
        System.GC.Collect();    // the best time to do GC

        // spawn bricks randomly.
        SpawnBricks();

        // at last, update UI
        GameUIHelper.Instance.DrawStage(Manager.GetStage());
        GameUIHelper.Instance.DrawLife(Manager.GetLifeNum());
        GameUIHelper.Instance.DrawScore(0);
        GameUIHelper.Instance.DrawTargetScore(Manager.GetTargetScore());

    }

	public void SetPadAndBall() {
		// set pad position
		pad.position = new Vector3(0, bottomPos + pad.GetComponent<Collider2D>().bounds.size.y / 3, 0);
		// set ball position
		ball.position = pad.position + new Vector3(0, ball.gameObject.GetComponent<Collider2D>().bounds.size.y / 2, 0);
		
		// mark the ball as unreleased
		Manager.Released = false;

		// return pad to normal
		pad.SendMessage("Reset");
		
		// return ball to normal
        ball.SendMessage("Reset");

	}
	
	public void SpawnBricks () {
		// get brick info. add a little distance between them
        float brickWidth = brickSprites[0].bounds.size.x * 1f;
        float brickHeight = brickSprites[0].bounds.size.y * 1.1f;

		// set block to spawn bricks
		float distX = brickWidth / 4;
		float distY = (topPos - bottomPos) * 3 / 7;
        float width = rightPos - leftPos - 2 * distX;
		float height = (topPos - bottomPos) / 2;
	
		// array to mark if a place is occupied
		int rowNum = (int)(width / brickWidth);
		int colNum = (int)(height / brickHeight);
		bool[,] used = new bool[rowNum, colNum];
		
        for (int i = 0; i < rowNum * colNum * spacePercentage; i++)
        {
            int row = Random.Range(0, rowNum);
            int col = Random.Range(0, colNum);
            used[row, col] = true;
        }

		int brickCount = 0;	// count how many bricks are generated
        for (int row = 0; row < rowNum; row++)
        {
            for (int col = 0; col < colNum; col++)
            {
                if (used[row, col])
                    continue;

                used[row, col] = true;
                brickCount++;
                float x = leftPos + distX + brickWidth * (row + 0.5f);
                float y = bottomPos + distY + brickHeight * (col + 0.5f);
                var theBrick = Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                int index = Random.Range(0, brickSprites.Length);
                bricks.Add(theBrick);   // add it to general management
                theBrick.GetComponent<SpriteRenderer>().sprite = brickSprites[index];
            }
        }
		
		Manager.SetTargetScoreByBrick(brickCount);	// set target score according to bricks
	}

}