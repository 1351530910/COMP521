using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;
    public void Start()
    {
        player = GameObject.Find("Player");
    }
    public void FixedUpdate()
    {
        if ((transform.position-player.transform.position).magnitude>100)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name.Equals("Player"))
        {
            return;
        }
        gameObject.SetActive(false);
        if (other.tag.Equals("Hitable"))
        {
            Game.teapotCount++;
            Destroy(other.gameObject);
            if (Game.teapotCount==Game.nTeapots)
            {
                var key = Instantiate(Game.prefabs["Key"]);
                key.transform.position = transform.position;
                key.SetActive(true);
            }
        }
        
    }
}
