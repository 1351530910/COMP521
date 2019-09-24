﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game 
{
    //constants
    public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);
    public static readonly Vector3 UpVector = new Vector3(0, 1, 0);
    public static readonly Vector3 DownVector = new Vector3(0, -1, 0);
    public static readonly Quaternion Agnle90 = Quaternion.Euler(0, 90, 0);
    public static string[] disables = { "Key"};
    




    //gamedata
    public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    public static int nTeapots = 0;
    public static int teapotCount = 0;
    public static int keyObtained = 0;


    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
        {

            prefabs[obj.name] = obj;
            if (disables.Contains(obj.name)) obj.SetActive(false);
            if (obj.CompareTag("Hitable")) nTeapots++;
        }
    }
}