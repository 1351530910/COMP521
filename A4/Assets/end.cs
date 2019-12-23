using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //detect if a shopper moved to the end
        if (other.gameObject.name.Contains("Customer"))
        {
            Destroy(other.gameObject);
            Flyer.available.RemoveAll(x => x == null);
        } 
    }
}
