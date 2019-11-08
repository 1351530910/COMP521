using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public static Vector3[] vertices =
    {
        new Vector3(1,1),
        new Vector3(2,1),
        new Vector3(2,0),
        new Vector3(3,0),
        new Vector3(3,1),
        new Vector3(4,1),
        new Vector3(4,0),
        new Vector3(5,0),
        new Vector3(5,1),
        new Vector3(6,1),
        new Vector3(6,2),
        new Vector3(7,2),
        new Vector3(7,3),
        new Vector3(6,3),
        new Vector3(6,4),
        new Vector3(7,4),
        new Vector3(7,5),
        new Vector3(6,5),
        new Vector3(6,6),
        new Vector3(5,6),
        new Vector3(5,7),
        new Vector3(4,7),
        new Vector3(4,6),
        new Vector3(3,6),
        new Vector3(3,7),
        new Vector3(2,7),
        new Vector3(2,6),
        new Vector3(1,6),
        new Vector3(1,5),
        new Vector3(0,5),
        new Vector3(0,4),
        new Vector3(1,4),
        new Vector3(1,3),
        new Vector3(0,3),
        new Vector3(0,2),
        new Vector3(1,2)
    };
    public static Vector3[] center =
    {
        new Vector3(4,3),
        new Vector3(3,3),
        new Vector3(3,4),
        new Vector3(4,4),
    };
    const int mapDim = 7;
    public static readonly bool[,] map = {
        { false,false,true,false,true,false,false},
        { false,true,true,true,true,true,false},
        { true,true,true,true,true,true,true},
        { false,true,true,true,true,true,false},
        { true,true,true,true,true,true,true},
        { false,true,true,true,true,true,false},
        { false,false,true,false,true,false,false}
    };
    Material material;
    GameObject wall;

    public Map()
    {
        wall = new GameObject("map");
        wall.AddComponent(typeof(SpriteRenderer));
        material = wall.GetComponent<SpriteRenderer>().material;
        wall.transform.position = new Vector3(-3.5f, -3.5f, 0);
        wall.transform.localScale = new Vector3(1, 1, 1);

    }
    
    public void draw()
    {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(wall.transform.position, wall.transform.rotation, wall.transform.localScale);
        GL.PushMatrix();
        GL.MultMatrix(mat);

        material.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.black);

        GL.Vertex(vertices[0]);
        foreach (var vertex in vertices)
        {
            GL.Vertex(vertex);
            GL.Vertex(vertex);
        }
        GL.Vertex(vertices[0]);

        GL.Vertex(center[0]);
        foreach (var v in center)
        {
            GL.Vertex(v);
            GL.Vertex(v);
        }
        GL.Vertex(center[center.Length-1]);

        GL.End();
        GL.PopMatrix();
        
    }
}
