using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;  //the player that owns the bullet
    public void Start()
    {
        player = GameObject.Find("Player");
    }
    public void FixedUpdate()
    {
        //check if the bullet is too far, the bullet disappear in case
        if ((transform.position-player.transform.position).magnitude>100)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        //if hit player just ignore
        if (other.gameObject.name.Equals("Player"))
        {
            return;
        }

        //bullet disappear if hit something else
        gameObject.SetActive(false);

        //if hit a hitable then destroy that
        if (other.tag.Equals("Hitable"))
        {
            Game.ObjectivesHit++;
            Destroy(other.gameObject);

            //if enough objective hit then the key appear
            if (Game.ObjectivesHit==Game.TotalObjectives)
            {
                var key = Instantiate(Game.prefabs["Key"]);
                key.transform.position = transform.position;
                key.SetActive(true);
            }
        }
        
    }
}
