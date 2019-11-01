using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHenge
{
    //stonehenge properties
    public const float top = -1.22f;
    public const float right = 0.74f;
    public const float left = -1.22f;


    public GameObject stonehenge;
    public List<GameObject> rocks = new List<GameObject>();

    public StoneHenge(Vector3 position)
    {
        stonehenge = new GameObject("stonehenge");
        stonehenge.transform.position = position;
        rocks.Add(Polygon.createPerlinRectangle(new Vector3(-1.18f, -1.61f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(400, 80)));
        rocks.Add(Polygon.createPerlinRectangle(new Vector3(-0.95f, -2.51f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(80, 200)));
        rocks.Add(Polygon.createPerlinRectangle(new Vector3(0, -2.51f, 0), new Vector3(0.005f, 0.005f, 0.005f), new Vector2(80, 200)));
        foreach (var rock in rocks)
            rock.transform.parent = stonehenge.transform;
    }
    
}
