using System.Collections;
using System.Collections.Generic;
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

    public static void buildmap()
    {
        HWalls = new bool[H+1, V+1];
        VWalls = new bool[H + 1, V + 1];

        //init with wall everywhere
        for (int x = 0; x < HWalls.GetUpperBound(0); x++)
        {
            for (int y = 0; y < HWalls.GetUpperBound(1); y++)
            {
                HWalls[x, y] = true;
                VWalls[x, y] = true;
            }
        }
        set = new bool[H, V];

        //clear up start place
        for (int x = 0; x < HWalls.GetUpperBound(0); x++)
        {
            VWalls[x, HWalls.GetUpperBound(1) - 1] = false;
            VWalls[x, HWalls.GetUpperBound(1) - 2] = false;
            HWalls[x, HWalls.GetUpperBound(1) - 1] = false;
        }

    }

    public static void drawHWall(int x,int y)
    {
        var wall = GameObject.Instantiate(Game.prefabs["HWall"]);
        wall.transform.position = new Vector3(x*walllength,y*walllength)
    }

    
}
