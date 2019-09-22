using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other!=GameLogic.Player.GetComponent<Collider>())
        {
            
            GameLogic.Bullet.SetActive(false);
        }
    }
}
