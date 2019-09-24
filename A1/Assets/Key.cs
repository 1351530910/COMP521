using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : FloatingObject
{
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Game.keyObtained++;
            gameObject.SetActive(false);
        }
    }
}
