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
    public static int[,] distance;
    public static bool[,] set;
    public static int entranceID = 0;

    public static List<GameObject> walls = new List<GameObject>();
    public static GameObject entranceWall;

    public static int resetMap()
    {
        //fill map with wall every side
        map = new int[H, V];
        set = new bool[H, V];
        distance = new int[H, V];

        for (int x = 0; x < H; x++)
        {
            for (int y = 0; y < V; y++)
            {
                map[x, y] = allwall;
                set[x, y] = false;
                distance[x, y] = int.MaxValue;
            }
        }

        //clear up initial space
        for (int x = 0; x < H; x++)
        {
            map[x, 0] = down;
            map[x, 1] = nowall;
            set[x, 0] = true;
            set[x, 1] = true;
        }
        map[0, 0] = down | left;
        map[0, 1] = left;
        map[H-1, 0] = down | right;
        map[H-1, 1] = right;

        int max = createmaze();

        draw();
        PlayerController.onset = false;
        return max;
    }

    static int createmaze()
    {
		System.DateTime now = System.DateTime.Now;
        System.Random r = new System.Random(System.DateTime.Now.Millisecond+System.DateTime.Now.Second);

        v2i previous = new v2i(r.Next(H), 9);
        v2i operation = previous;
        v2i next = previous;

        
        set[previous.x, previous.y] = true;
        map[previous.x,previous.y] = allwall-up;
        distance[previous.x, previous.y] = 1;

        int assigned = 1;
        while (assigned<mazeSize)
        {
            operation = operations[r.Next(operations.Length)];
            next = previous + operation;
            if (next.x >= 0 && next.x < H && next.y >= 2 && next.y < V)
            {
                if (!set[next.x,next.y])
                {
                    distance[next.x, next.y] = distance[previous.x, previous.y] + 1;
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
        
        Game.text = "";
        for (int i = V-1; i >= 0; i--)
        {
            for (int j = 0; j < H; j++)
            {
                if (distance[j,i]<100)
                {
                    if (distance[j, i] < 10)
                    {
                        Game.text += "_" + distance[j, i] + " ";
                    }
                    else
                    {
                        Game.text += distance[j, i] + " ";
                    }
                }
            }
            Game.text += "\n";
        }

        entranceID = 0;
        int currmax = -1;
        for (int i = 0; i < H; i++)
        {
            if (distance[i, 2] < 16 && distance[i, 2] > currmax)
            {
                entranceID = i;
                currmax = distance[i, 2];
            }
        }
        map[entranceID, 2] -= down;
        return currmax;
    }

    static void draw()
    {
        foreach (var item in walls)
        {
            GameObject.Destroy(item);
        }
        walls = new List<GameObject>();

        entranceWall = GameObject.Instantiate(Game.prefabs["HWall"]);
        entranceWall.transform.position = new Vector3(entranceID * walllength, 0, 1 * walllength + 2.5f);
        entranceWall.SetActive(true);
        Debug.Log(entranceID);
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

}
