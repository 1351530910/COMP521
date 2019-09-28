using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeCount : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Player"&&++Game.time>Game.MaxTime)
        {
            Game.lostCount++;
            Game.Reset();
        }
    }
}
