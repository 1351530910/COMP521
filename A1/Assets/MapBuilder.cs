using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MapBuilder
{
    //wall attributes
    const int up = 0b0001;
    const int down = 0b0010;
    const int left = 0b0100;
    const int right = 0b1000;
    const int nowall = 0b0000;
    const int allwall = 0b1111;

    static readonly v2i[] operations = { new v2i(1, 0), new v2i(-1, 0), new v2i(0, 1), new v2i(0, -1) };
    
    const int mazeSize = 64;
    const int walllength = 5;
    
    const int H = 8;
    const int V = 10;
    
    public static bool[,] HWalls { get; set; }
    public static bool[,] VWalls { get; set; }
    public static int[,] map;
    public static bool[,] set;

    public static List<GameObject> walls = new List<GameObject>();
    public static GameObject entranceWall;
    public static GameObject exitWall;

    public static void createmap()
    {
        //fill map with wall every side
        map = new int[H, V];
        set = new bool[H, V];
        for (int x = 0; x < H; x++)
        {
            for (int y = 0; y < V; y++)
            {
                map[x, y] = allwall;
            }
        }


        //clear up initial space
        for (int x = 0; x < H; x++)
        {
            map[x, 0] = down;
            map[x, 1] = up;
            set[x, 0] = true;
            set[x, 1] = true;
        }
        map[0, 0] = down | left;
        map[0, 1] = left;
        map[H-1, 0] = down | right;
        map[H-1, 1] = right;
        

        createmaze();
        draw();
    }

    static void createmaze()
    {
		System.DateTime now = System.DateTime.Now;
        System.Random r = new System.Random(System.DateTime.Now.Millisecond);

        v2i previous = new v2i(r.Next(H), 2);
        v2i operation = previous;
        v2i next = previous;

        
        set[previous.x, previous.y] = true;
        map[previous.x,previous.y] = allwall-down;
        int assigned = 1;
        while (assigned<mazeSize)
        {
            operation = operations[r.Next(operations.Length)];
            next = previous + operation;
            if (next.x >= 0 && next.x < H && next.y >= 2 && next.y < V)
            {
                if (!set[next.x,next.y])
                {
                    assigned++;
                    set[next.x, next.y] = true;
                    switch (operation.ToString())
                    {
                        case "-1 0":
                            map[next.x, next.y] &= (allwall - right);
                            map[previous.x, previous.y] &= (allwall - left);
                            break;
                        case "1 0":
                            map[next.x, next.y] &= (allwall - left);
                            map[previous.x, previous.y] &= (allwall - right);
                            break;
                        case "0 1":
                            map[next.x, next.y] &= (allwall - down);
                            map[previous.x, previous.y] &= (allwall - up);
                            break;
                        case "0 -1":
                            map[next.x, next.y] &= (allwall - up);
                            map[previous.x, previous.y] &= (allwall - down);
                            break;
                        default:
                            break;
                    }
                }
                previous = next;
            }
        }
		Debug.Log((System.DateTime.Now - now).TotalMilliseconds);
		return;
		
    }

    static void draw()
    {
        foreach (var item in walls)
        {
            GameObject.Destroy(item);
        }
        walls = new List<GameObject>();

        for (int x = 0; x < H; x++)
        {
            for (int y = 0; y < V; y++)
            {
                if ((map[x, y]&up)!=nowall)
                {
                    var wall = GameObject.Instantiate(Game.prefabs["HWall"]);
                    wall.transform.position = new Vector3(x * walllength, 0, y * walllength+2.5f);
                    wall.SetActive(true);
                    walls.Add(wall);
                }
                if ((map[x, y] & down) != nowall)
                {
                    var wall = GameObject.Instantiate(Game.prefabs["HWall"]);
                    wall.transform.position = new Vector3(x * walllength , 0, y * walllength - 2.5f);
                    wall.SetActive(true);
                    walls.Add(wall);
                }
                if ((map[x, y] & left) != nowall)
                {
                    var wall = GameObject.Instantiate(Game.prefabs["VWall"]);
                    wall.transform.position = new Vector3(x * walllength - 2.5f, 0, y * walllength);
                    wall.SetActive(true);
                    walls.Add(wall);
                }

                if ((map[x, y] & right) != nowall)
                {
                    var wall = GameObject.Instantiate(Game.prefabs["VWall"]);
                    wall.transform.position = new Vector3(x * walllength + 2.5f, 0, y * walllength);
                    wall.SetActive(true);
                    walls.Add(wall);
                }
            }
        }
    }

   
    public static bool allset()
    {
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (!set[i,j])
                {
                    return false;
                }
            }
        }
        return true;
    }

}
