using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader
{
    public const int ntraders = 8;
    GameObject gameObject;
    int number;
    static bool[,] tradermap = (bool[,])Map.map.Clone();
    static Trader()
    {
        tradermap[3, 3] = false;
    }

    public Trader(GameObject prefab,int n)
    {
        this.number = n+1;
        gameObject = GameObject.Instantiate(prefab);
        gameObject.name = $"trader {number}";
        gameObject.transform.parent = Main.canvas.transform;
        Vector2Int position;
        do
        {
            position = new Vector2Int(Random.Range(0, 7), Random.Range(0,7));
        } while (!tradermap[position.x,position.y]);

        tradermap[position.x,position.y] = false;
        gameObject.transform.position = new Vector3(position.x-3,position.y-3);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Text>().text = $"trader {number}";
        gameObject.SetActive(true);
    }



}
