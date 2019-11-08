using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan
{
    GameObject gameobject;
    public Caravan(string name)
    {
        gameobject = GameObject.Find(name);
    }

    public void move(Vector3 v)
    {
        if (true)
        {
            gameobject.transform.position += v;
        }
    }
}
