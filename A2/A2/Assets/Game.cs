using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;

public class Game : MonoBehaviour
{
    public static float scrTop;
    public static float scrRight;
    public static GameObject ground;
    public static GameObject cannon;
    public static GameObject cannonball;
    public static Ghost[] ghosts = new Ghost[4];
    public static StoneHenge stonehenge;


    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        stonehenge = new StoneHenge(new Vector3(0, -2f, 0));
        ground = Polygon.createRectangle(new Vector3(-20, -2.57f, 0), new Vector3(1, 0.01f, 1), new Vector2(1000, 5), Color.black, "ground");
        cannon = GameObject.Find("CannonTransform");
        cannonball = GameObject.Find("cannonball");
        cannonball.SetActive(false);
        scrTop = Screen.height/158.8f;
        scrRight = Screen.width/158.8f;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i] = new Ghost();
        }
    }
    void Update()
    {
        var dt = Time.deltaTime* Time.deltaTime;
        foreach (var ghost in ghosts)
        {
            
            ghost.update(dt);
        }
    }
    void OnPostRender()
    {
        foreach (var ghost in ghosts)
        {
            ghost.Render();
        }
    }
}
