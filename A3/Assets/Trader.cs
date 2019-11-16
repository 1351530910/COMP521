using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Trader 
{
    public int n = 0;
    public GameObject gameObject;

    public static readonly int[,] actions =
    {
        {2,0,0,0,0,0,0 },
        {-2,1,0,0,0,0,0 },
        {0,-2,1,0,0,0,0 },
        {-4,0,0,1,0,0,0 },
        {-1,0,-1,0,1,0,0 },
        {-2,-1,0,-1,0,1,0 },
        {0,0,-4,0,0,0,1 },
        {0,-1,0,-1,-1,0,1 },
    };
    public static readonly int[] modifs = { 2, -1, -1, -1, -1, -3, -3, -2 };
    public Trader(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public void setn(int n)
    {
        this.n = n;
        gameObject.name = (n+1) + "";
        if (gameObject.GetComponent<TMPro.TextMeshPro>()==null)
        {
            Debug.Log("not found");
        }
        gameObject.GetComponent<TMPro.TextMeshPro>().text = (n+1) + "";
    }
    public Vector3 getposition()
    {
        return gameObject.transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {

    }

    public int[] trade(int[] data)
    {
        int[] arr = new int[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            arr[i] = data[i] + actions[n, i];
        }
        if (arr.Any(x => x < 0))
        {
            Debug.Log("success");
            return data;
        }
        else
            return arr;
    }
}
