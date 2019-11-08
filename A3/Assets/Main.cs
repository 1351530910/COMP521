using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Caravan caravan;
    public static GameObject canvas;
    public static List<Trader> traders = new List<Trader>();
    public static Map map;

    [RuntimeInitializeOnLoadMethod]
    public static void init() {
        canvas = GameObject.Find("Canvas");
        caravan = new Caravan("Caravan");

        var traderprefab = GameObject.Find("Trader");
        for (int i = 0; i < Trader.ntraders; i++)
            traders.Add(new Trader(traderprefab, i));
        GameObject.Destroy(traderprefab);
        map = new Map();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            caravan.move(new Vector3(0, 1));
        if (Input.GetKeyUp(KeyCode.DownArrow))
            caravan.move(new Vector3(0, -1));
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            caravan.move(new Vector3(-1, 0));
        if (Input.GetKeyUp(KeyCode.RightArrow))
            caravan.move(new Vector3(1, 0));
    }

    void OnPostRender()
    {
        map.draw();
    }
}
