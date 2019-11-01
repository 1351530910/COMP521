using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost
{
    const int twoPi = 63;
    const float minx = -8.5f;
    const float maxx = -2.5f;
    public const float sizeSqr = 0.41f * 0.41f;
    //ghost vertices
    static readonly Vector3[] initialVertices =
    {
        new Vector3(-0.5f,2.5f,0),
        new Vector3(0.5f,2.5f,0),
        new Vector3(1.5f,1.5f,0),
        new Vector3(1.5f,-0.5f,0),
        new Vector3(1f,-2f,0),
        new Vector3(0.5f,-0.5f,0),
        new Vector3(0f,-1.5f,0),
        new Vector3(-0.5f,0f,0),
        new Vector3(-1f,-1.2f,0),
        new Vector3(-1.5f,1f,0),
        new Vector3(-1.5f,1.5f,0),
    };
    static readonly Vector3 leftEyeCenter = new Vector3(-0.5f, 1f, 0);
    static readonly Vector3 rightEyeCenter = new Vector3(0.5f, 0.5f, 0);
    
    static Vector3[] initialleftEye;
    static Vector3[] initialrightEye;

    Vector3[] leftEye;
    Vector3[] rightEye;

    public GameObject gameObject;
    public Vector3[] vertices;
    Material material;
    float targetY;
    public Vector3 lastposition;
    Vector3 aV = new Vector3(0, 0.25f, 0);
    Vector3 aH = new Vector3(0.5f, 0, 0);

    public Matrix4x4 angularSpeed = Matrix4x4.identity;

    static Ghost()
    {
        float leftEyeDiameter = Random.Range(0.2f,0.4f);
        float rightEyeDiameter = Random.Range(0.2f, 0.4f);
        initialleftEye = new Vector3[twoPi];
        initialrightEye = new Vector3[twoPi];
        for (int i = 0; i < twoPi; i++)
        {
            initialleftEye[i] = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0) * leftEyeDiameter + leftEyeCenter;
            initialrightEye[i] = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0) * rightEyeDiameter + rightEyeCenter;
        }
    }


    public Ghost()
    {
        vertices = (Vector3[])initialVertices.Clone();
        gameObject = new GameObject("ghost");
        gameObject.AddComponent(typeof(SpriteRenderer));
        material = gameObject.GetComponent<SpriteRenderer>().material;
        gameObject.transform.position = new Vector3(Random.Range(minx,maxx), -2.5f, 0);
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        gameObject.SetActive(true);
        targetY = Random.Range(StoneHenge.top, StoneHenge.top + 2);
        lastposition = gameObject.transform.position;
        rightEye = (Vector3[])initialrightEye.Clone();
        leftEye = (Vector3[])initialleftEye.Clone();
    }

    public void update(float dtsqr)
    {
        if (gameObject.transform.position.y > Game.scrTop || gameObject.transform.position.x > Game.scrRight || gameObject.transform.position.x < -Game.scrRight)
        {
            gameObject.transform.position = new Vector3(Random.Range(minx, maxx), -2.5f, 0);
            lastposition = gameObject.transform.position;
            angularSpeed = Matrix4x4.identity;
            vertices = (Vector3[])initialVertices.Clone();
            rightEye = (Vector3[])initialrightEye.Clone();
            leftEye = (Vector3[])initialleftEye.Clone();
            return;
        }
        if (gameObject.transform.position.y<targetY)
        {
            Debug.Log(aV * dtsqr);
            var nextposition = (2 * gameObject.transform.position - lastposition + aV * dtsqr);
            lastposition = gameObject.transform.position;
            gameObject.transform.position = nextposition;
            if (gameObject.transform.position.y> targetY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,targetY+0.1f,0);
                lastposition = gameObject.transform.position;
            }
        }
        else
        {
            var nextposition = (2 * gameObject.transform.position - lastposition + aH * dtsqr);
            lastposition = gameObject.transform.position;
            gameObject.transform.position = nextposition;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = angularSpeed*vertices[i];
            
        }
        for (int i = 0; i < rightEye.Length; i++)
        {
            rightEye[i] = angularSpeed * rightEye[i];
            leftEye[i] = angularSpeed * leftEye[i];
        }
    }
    
    public void Render()
    {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale);
        GL.PushMatrix();
        GL.MultMatrix(mat);
        material.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.blue);

        //draw body
        GL.Vertex(vertices[0]);
        for (int i = 0; i < vertices.Length; i++)
        {
            GL.Vertex(vertices[i]);
            GL.Vertex(vertices[i]);
        }
        GL.Vertex(vertices[0]);

        //draw eyes
        GL.Vertex(leftEye[0]);
        for (int i = 0; i < leftEye.Length; i++)
        {
            GL.Vertex(leftEye[i]);
            GL.Vertex(leftEye[i]);
        }
        GL.Vertex(leftEye[0]);

        GL.Vertex(rightEye[0]);
        for (int i = 0; i < rightEye.Length; i++)
        {
            GL.Vertex(rightEye[i]);
            GL.Vertex(rightEye[i]);
        }
        GL.Vertex(rightEye[0]);

        GL.End();
        GL.PopMatrix();
    }
}
