using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //size of eat area
    public const int w = 11, h = 5;
    /// <summary>
    /// 0 available
    /// 1 unavailable
    /// </summary>
    int[,] map = new int[w, h];

    // Start is called before the first frame update
    void Start()
    {
        GameObject shop = GameObject.Find("shop");
        for (int i = 0; i < 17; i++)
        {
            GameObject obj = Instantiate(shop);
            obj.name = "shop";
            obj.transform.position = new Vector3(i - 8, 0, 4);
            obj = Instantiate(shop);
            obj.name = "shop";
            obj.transform.position = new Vector3(i - 8, 0, -4);

        }
        Destroy(shop);

        //2-5 planters
        int nplanter = Random.Range(2, 6);
        GameObject planter = GameObject.Find("planter");
        for (int i = 0; i < nplanter; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);
            while (map[x, y] != 0 && x % 2 != 0)
            {
                x = Random.Range(0, w);
                y = Random.Range(0, h);
            }
            map[x, y] = 1;
            GameObject obj = Instantiate(planter);
            obj.name = "planter";
            obj.transform.position = new Vector3(x - w/2, 0, y - h/2);
            obj.transform.localScale = new Vector3(Random.Range(0.6f, 0.9f), 1, Random.Range(0.6f, 0.9f));
        }
        Destroy(planter);

        //3-4 tables
        int ntables = Random.Range(3, 5);
        GameObject table = GameObject.Find("t");
        for (int i = 0; i < ntables; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);
            while (map[x, y] != 0)
            {
                x = Random.Range(0, w);
                y = Random.Range(0, h);
            }
            map[x, y] = 1;
            GameObject obj = Instantiate(table);
            obj.name = "t";
            obj.transform.position = new Vector3(x - w / 2, 0, y - h / 2);
            obj.transform.Rotate(new Vector3(0, Random.Range(0, 90), 0));
        }
        Destroy(table);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
