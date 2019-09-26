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

    public static List<GameObject> walls = new List<GameObject>();
    public static GameObject entranceWall;
    public static GameObject exitWall;

    public static void createmap()
    {
        //fill map with wall every side
        map = new int[H, V];
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
        System.Random r = new System.Random(System.DateTime.Now.Millisecond);

        v2i previous = new v2i(r.Next(H), 2);
        v2i operation = previous;
        v2i next = previous;

        map[previous.x,previous.y] = allwall-down;
        int assigned = 1;
        while (assigned<mazeSize/2)
        {
            operation = operations[r.Next(operations.Length)];
            next = previous + operation;
            if (next.x >= 0 && next.x < H && next.y >= 2 && next.y < V)
            {
                if (map[next.x,next.y] == allwall)
                {
                    assigned++;
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
        
        int steps = 0;
        while (assigned < mazeSize && ++steps<500)
        {
            List<string> history = new List<string>();
            //choose start point
            do
            {
                previous = new v2i(r.Next(H), r.Next(H) + 2);
            } while (map[previous.x, previous.y] != allwall);
            assigned++;
            history.Add(previous.ToString());
        nextcell:
            do
            {
                operation = operations[r.Next(operations.Length)];
                next = previous + operation;
            } while ((!(next.x >= 0 && next.x < H && next.y >= 2 && next.y < V)) && !history.Contains(next.ToString()));
            history.Add(next.ToString());
            Debug.Log(string.Join(",",history.ToArray()));
            if (map[next.x, next.y] == allwall)
            {
                Debug.DrawLine(new Vector3(previous.x * 5+0.2f, 0.5f, previous.y * 5 + 0.2f), new Vector3(next.x * 5 + 0.2f, 0.5f, next.y * 5 + 0.2f), Color.blue, 3000000);
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
                assigned++;
                previous = next;
                goto nextcell;
            }
            else
            {
                Debug.DrawLine(new Vector3(previous.x * 5, 0.5f, previous.y * 5), new Vector3(next.x * 5, 0.5f, next.y * 5), Color.red, 3000000);
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
        }

        Debug.Log(steps);
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


        map = new int[H, H];

        //clear up start place
        for (int x = 0; x < H-1; x++)
        {
            VWalls[x+1, 0] = false;
            VWalls[x + 1, 1] = false;
            HWalls[x + 1, 1] = false;
        }

        generateMaze();
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
    public static void generateMaze()
    {
        
    }

}
