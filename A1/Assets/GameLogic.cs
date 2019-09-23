using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

public class GameLogic
{
    public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public static GameObject Player;
    public static GameObject Bullet;
    public static GameObject Camera;
    public static GameObject Wall;
    public static GameObject Hitable;
    public static int[,] Maze;
    public static List<string> inventory = new List<string>();

    public static int Objectives = 0;
    public const int TotalObjectives = 1;
    const float BulletSpeed = 50.0f;   //initial speed of the bullet
    public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);       //empty vector
    public static readonly Vector3 UpVector = new Vector3(0, 1, 0);         //up vector for the camera

    
    

    [RuntimeInitializeOnLoadMethod]
    public static void initialize()
    {
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>().Where(x=>!x.CompareTag("untracked")))
        {
            prefabs.Add(obj.name, obj);
            obj.SetActive(false);
        }
        Init();
    }

    public static void Init()
    {
        inventory = new List<string>();
        if (Player!=null)
        {
            GameObject.Destroy(Camera);
            GameObject.Destroy(Player);
            GameObject.Destroy(Bullet);
        }
        //initialize player and bullet
        Player = GameObject.Instantiate(prefabs["Player"]);
        
        Camera = Player.transform.GetChild(1).gameObject;
        Bullet = Player.transform.GetChild(0).gameObject;
        Hitable = GameObject.Instantiate(prefabs["Hitable"]);
        Wall = GameObject.Instantiate(prefabs["Wall"]);
        Player.SetActive(true);
        Camera.SetActive(true);
        Bullet.SetActive(false);
        Wall.SetActive(true);
        Hitable.SetActive(true);
        Player.transform.LookAt(new Vector3(10, 1, 10), UpVector);
    }
    

    public static void shoot()
    {
        Bullet.SetActive(true);
        Bullet.GetComponent<Rigidbody>().velocity = ZeroVector;
        Bullet.GetComponent<Rigidbody>().AddForce(Player.transform.forward * BulletSpeed, ForceMode.Impulse);
        Bullet.transform.localPosition = ZeroVector;
    }
}
