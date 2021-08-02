using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Color))]
public class MapUnit : MonoBehaviour
{
    public GameObject basemap;
    public GameObject ObstaclePrefabs;
    public Color UpColor;
    public Color ButtomColor;

    public Vector2 mapSize;
    [Range(0, 1)] public float mapspacing;


    [Range(0, 1)] public float obstaclePercentage;
    Queue<Coord> coords;

    bool[,] bostacleIsStay;
    Coord mapCenter;

    // Start is called before the first frame update
    void Start()
    {
        InItMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InItMap()
    {
        string rootName = "Root";
        if (transform.Find(rootName))
        {
            DestroyImmediate(transform.Find(rootName).gameObject);
        }
        Transform Root = new GameObject(rootName).transform;
        Root.parent = transform;
        List<Coord> point = new List<Coord>();

        int max_x = (int)mapSize.x;
        int max_y = (int)mapSize.y;
        for (int x = 0; x < max_x; x++)
        {
            for (int y = 0; y < max_y; y++)
            {
                GameObject go = Instantiate(basemap, GetPos(x, y), Quaternion.Euler(90, 0, 0), Root);
                go.transform.localScale = Vector3.one * (1 - mapspacing);
                point.Add(new Coord(x, y));
            }
        }

        mapCenter = new Coord((max_x / 2), (max_y / 2));
        bostacleIsStay = new bool[max_x, max_y];

        int obstacleStayCount = 0;

        coords = new Queue<Coord>(Shuffle.Range(point.ToArray()));
        int obstacleCount = (int)(max_x * max_y * obstaclePercentage);
        for (int i = 0; i < obstacleCount; i++)
        {
            var obstacle = GetObstacle();
            bostacleIsStay[obstacle.x, obstacle.y] = true;
            obstacleStayCount++;
            if (mapCenter != obstacle && ObstacleInit(bostacleIsStay, obstacleStayCount))
            {
                float scaleY = UnityEngine.Random.Range(1.0f, 2.0f);

                var oustacl = Instantiate(ObstaclePrefabs, GetPos(obstacle.x, obstacle.y) + Vector3.up * scaleY * 0.5f, Quaternion.identity, Root);
                oustacl.transform.localScale = new Vector3(1, scaleY, 1);
                float bili = (obstacle.y * 1.0f) / mapSize.y;
                if (Application.isPlaying)
                    oustacl.GetComponent<MeshRenderer>().material.color = Color.Lerp(UpColor, ButtomColor, bili);
            }
            else
            {
                bostacleIsStay[obstacle.x, obstacle.y] = false;
                obstacleStayCount--;
            }
        }
    }

    private bool ObstacleInit(bool[,] bostacleIsStay, int obstacleCount)
    {
        int max_x = bostacleIsStay.GetLength(0);
        int max_y = bostacleIsStay.GetLength(1);
        bool[,] ischeck = new bool[max_x, max_y];

        ischeck[mapCenter.x, mapCenter.y] = true;
        int konggecount = 1;

        Queue<Coord> coords = new Queue<Coord>();
        coords.Enqueue(new Coord(mapCenter.x, mapCenter.y));
        while (coords.Count > 0)
        {
            var coord = coords.Dequeue();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 || y == 0)
                    {
                        int check_x = coord.x + x;
                        int check_y = coord.y + y;
                        if (check_x >= 0 && check_x < max_x && check_y >= 0 && check_y < max_y)
                        {
                            if (!bostacleIsStay[check_x, check_y] && !ischeck[check_x, check_y])
                            {
                                ischeck[check_x, check_y] = true;
                                konggecount++;
                                coords.Enqueue(new Coord(check_x, check_y));
                            }
                        }
                    }
                }
            }
        }


        int _obstacleCount = max_x * max_y - konggecount;
        return _obstacleCount == obstacleCount;
    }

    Vector3 GetPos(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + x + 0.5f, 0, -mapSize.y / 2 + y + 0.5f);
    }
    Coord GetObstacle()
    {
        var coord = coords.Dequeue();
        coords.Enqueue(coord);
        return coord;
    }
}
public struct Coord
{
    public int x;
    public int y;
    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }
    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }
}
public class Shuffle
{
    public static T[] Range<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int range = UnityEngine.Random.Range(i, array.Length);
            var item = array[range];
            array[range] = array[i];
            array[i] = item;
        }
        return array;
    }
}
