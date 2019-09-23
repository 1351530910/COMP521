using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time)/3 + 1, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==GameLogic.Player.gameObject)
        {
            GameLogic.inventory.Add("Key");
        }
        Destroy(gameObject);
    }
}
