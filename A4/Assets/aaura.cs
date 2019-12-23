using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaura : MonoBehaviour
{
    public GameObject parent;   //parent gameobject
    advertiser parentav;        //parent navigator
    void Start()
    {
        parent = transform.parent.gameObject;
        parentav = transform.parent.gameObject.GetComponent<advertiser>();
    }

    
    void OnTriggerStay(Collider other)
    {
        //fleeing steering force
        if (other.isTrigger)
        {
            Vector3 dir = (other.gameObject.transform.position - parent.transform.position);
            parentav.direction -= (1.5f / dir.magnitude) * dir.normalized;
        }
        
    }
}
