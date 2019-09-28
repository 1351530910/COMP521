using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game 
{
    //constants
    public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);
    public static readonly Vector3 UpVector = new Vector3(0, 1, 0);
    public static readonly Vector3 DownVector = new Vector3(0, -1, 0);
    public static readonly Quaternion Agnle90 = Quaternion.Euler(0, 90, 0);
    public static string[] disables = { "Key","HWall","VWall","PositionDetect"};
    public const int HMapSize = 8;
    public const int VMapSize = 10;
    public const int MaxTime = 16;
    

    //gamedata
    public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    public static List<GameObject> objectives = new List<GameObject>();
    public static GameObject player;
    public static GameObject Key;
    public static int ObjectivesHit = 0;
    public static int[,] map = new int[HMapSize, VMapSize];
    public static string text = "";
    public static int solvedCount = 0;
    public static int lostCount = 0;
    public static int resetCount = 0;
    public static int time = 0;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            prefabs[obj.name] = obj;
            if (obj.CompareTag("Hitable")) objectives.Add(obj);
            if (obj.name == "Player") player = obj;
            if (disables.Contains(obj.name)) obj.SetActive(false);
        }



        //reset map until find a valid map
        while (MapBuilder.resetMap() < 0) ;
        //create time detection colliders
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var collider = GameObject.Instantiate(prefabs["PositionDetect"]);
                collider.transform.position = new Vector3(x * 5, 0, y * 5 +10);
                collider.SetActive(true);
            }
        }

    }
    public static void Reset()
    {
        time = 0;
        player.transform.position = PlayerController.startPosition;
        ObjectivesHit = 0;
        foreach (GameObject teapot in objectives)
        {
            teapot.SetActive(true);
        }
        MapBuilder.resetMap();
    }
}
