using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Game 
{
    public static GameObject stonehenge;
    public static GameObject ground;
    public static GameObject cannon;
    public static GameObject cannonball;




    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        stonehenge = new GameObject("stonehenge");
        stonehenge.transform.position = new Vector3(0, -2f, 0);
        Polygon.createPerlinRectangle(new Vector3(-1.18f, -1.61f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(400, 80)).transform.parent = stonehenge.transform;
        Polygon.createPerlinRectangle(new Vector3(-0.95f, -2.51f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(80, 200)).transform.parent = stonehenge.transform;
        Polygon.createPerlinRectangle(new Vector3(0, -2.51f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(80, 200)).transform.parent = stonehenge.transform;
        ground = Polygon.createRectangle(new Vector3(-20, -2.57f, 0), new Vector3(1, 0.01f, 1), new Vector2(1000, 5), Color.black, "ground");
        cannon = GameObject.Find("CannonTransform");
        cannonball = GameObject.Find("cannonball");
        cannonball.SetActive(false);

    }
    
}
