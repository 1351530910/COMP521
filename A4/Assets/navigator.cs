using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class navigator : MonoBehaviour
{
    //number of steps
    public List<GameObject> targets = new List<GameObject>();
    public Vector3 direction = new Vector3(0, 0, 0);    //sum of steering forces
    const float velocity = 1.5f;
    public bool stay = false;   //if person is staying
    public float deltatime = 0; //time counter
    public float targettime = 0;    //max time counter
    public bool occupy = false;
    public bool flyered = false;
    void Start()
    {
        if (Random.Range(0,10)>5)
        {
            targets.Add(Game.shops[Random.Range(0, Game.shops.Count)]);
            targets.Add(Game.seats[Random.Range(0, Game.seats.Count)]);
        }
        targets.Add(GameObject.Find("End"));
        transform.position = new Vector3(-8.5f, 0, Random.Range(-2.2f, 2.2f));
    }
    void Update()
    {
        //if not moving
        if (stay)
        {
            //increase timer
            stay = (deltatime += Time.deltaTime) < targettime;
            //if stop by a flyer then do nothing
            if (!flyered)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                //make it disappear to enter shop or eat
                targets[0] = GameObject.Find("End");
            }
            return;
        }
        //set normal scale
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        direction.y = 0;

        try
        {
            //move according to fleeing force and seeking force
            transform.position += (direction + (targets[0].transform.position - transform.position).normalized).normalized * velocity * Time.deltaTime;
        }
        catch (System.Exception)
        {
            //in case of concurrency issues (destroyed by End but script not stopped)
            Destroy(gameObject);
        }
        //reset direction for next frame
        direction = new Vector3(0, 0, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Any(x => x.otherCollider.gameObject == targets[0]))
        {
            //if seat then take that seat
            if (targets[0].name=="seat")
            {
                //if nobody else already took that seat then take it
                if (Game.occupy[targets[0]] == null|| !Game.occupy[targets[0]].stay)
                {
                    Game.occupy[targets[0]] = this;
                    targettime = Random.Range(2f, 3f);      //stay 2-3 sec eating
                }
                else
                {
                    //otherwise find another seat
                    targets[0] = Game.seats[Random.Range(0, Game.seats.Count)];
                    return;
                }
            }
            else
            {
                //stay 1 sec in shop
                targettime = 1;
            }
            stay = true;
            deltatime = 0;
            targets.RemoveAt(0);
        }
    }

    
    void OnCollisionStay(Collision collisionInfo)
    {
        if (stay) return;
        //avoid structure overlap
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            transform.position -= Time.deltaTime*10*(contact.otherCollider.gameObject.transform.position - transform.position);
        }
    }
}
