using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aura : MonoBehaviour
{
    Transform parentTransform;  //shopper's transform
    navigator parentnv;     //shopper's navigator
    void Start()
    {
        parentTransform = transform.parent;
        parentnv = transform.parent.gameObject.GetComponent<navigator>();
    }

    void OnTriggerStay(Collider other)
    {
        //flee steering force
        if (other.isTrigger&&!other.gameObject.name.Contains("flyer"))
        {
            Vector3 dir = (other.gameObject.transform.position - parentTransform.position);
            parentnv.direction -= (1.5f / dir.magnitude) * dir.normalized;
        }
    }
}
