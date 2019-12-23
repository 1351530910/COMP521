using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    public static List<GameObject> available = new List<GameObject>(); 
    void OnTriggerEnter(Collider other)
    {
        navigator n;
        //if it is a valid navigator person
        if ((n = other.GetComponent<navigator>()) != null)
        {
            //make it stay for 2 seconds
            n.deltatime = 0;
            n.targettime = 2;
            n.stay = true;
            n.flyered = true;
            //add it to the list so advertisers will see this person
            available.Add(n.gameObject);
            Destroy(gameObject);
        }
    }
}
