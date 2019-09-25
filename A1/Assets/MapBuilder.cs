using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapBuilder
{
    const int walllength = 5;
    static Vector2 location;
    static readonly Vector2 up = new Vector2(0, 1);
    static readonly Vector2 down = new Vector2(0, -1);
    static readonly Vector2 left = new Vector2(-1, 0);
    static readonly Vector2 right = new Vector2(1, 0);

    const int H = 8;
    const int V = 10;
    
    public static bool[,] HWalls { get; set; }
    public static bool[,] VWalls { get; set; }
    static bool[,] set;

    static List<GameObject> walls = new List<GameObject>();

    public static void buildmap()
    {
        HWalls = new bool[H+1, V+2];
        VWalls = new bool[H + 2, V+1];

        //init with wall everywhere
        for (int x = 0; x < HWalls.GetUpperBound(0); x++)
        {
            for (int y = 0; y < HWalls.GetUpperBound(1); y++)
            {
                HWalls[x, y] = true;
            }
        }
        for (int x = 0; x < VWalls.GetUpperBound(0); x++)
        {
            for (int y = 0; y < VWalls.GetUpperBound(1); y++)
            {
                VWalls[x, y] = true;
            }
        }

        set = new bool[H, V];

        //clear up start place
        
        for (int x = 0; x < H-1; x++)
        {
            VWalls[x+1, 0] = false;
            VWalls[x + 1, 1] = false;
            HWalls[x + 1, 1] = false;
        }
        

        redraw();
       
    }
    public static void redraw()
    {
        foreach (var item in walls)
        {
            GameObject.Destroy(item);
        }

        walls = new List<GameObject>();

        for (int x = 0; x < HWalls.GetUpperBound(0); x++)
        {
            for (int y = 0; y < HWalls.GetUpperBound(1); y++)
            {
                if (HWalls[x,y])
                {
                    var wall = GameObject.Instantiate(Game.prefabs["HWall"]);
                    wall.transform.position = new Vector3(x * walllength + 2.5f, 0, y * walllength);
                    wall.SetActive(true);
                    walls.Add(wall);
                }
            }
        }
        for (int x = 0; x < VWalls.GetUpperBound(0); x++)
        {
            for (int y = 0; y < VWalls.GetUpperBound(1); y++)
            {
                if (VWalls[x,y])
                {
                    var wall = GameObject.Instantiate(Game.prefabs["VWall"]);
                    wall.transform.position = new Vector3(x * walllength, 0, y * walllength + 2.5f);
                    wall.SetActive(true);
                    walls.Add(wall);
                }
            }
        }
    }

}
