using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    const float MaxBulletRange = 100f;  //maximum distance a bullet can travel
    void OnTriggerEnter(Collider other)
    {
        
        if (other!=GameLogic.Player.GetComponent<Collider>())
        {
            if (other.name.Contains("Hitable"))
            {
                if (++GameLogic.Objectives >= GameLogic.TotalObjectives)
                {
                    GameObject key = Instantiate(GameLogic.prefabs["Key"]);
                    key.SetActive(true);
                    key.transform.position = transform.position;
                }
                Destroy(other.gameObject);
                
            }
            GameLogic.Bullet.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        //check if bullet is out of range
        if (transform.localPosition.magnitude > MaxBulletRange) gameObject.SetActive(false);
    }
}
