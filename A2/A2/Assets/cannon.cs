using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    public const float cannonForce = 8.5f;
    public void Update()
    {
        //shoot cannonball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject cannonball = GameObject.Instantiate(Game.cannonball);
            //cannonball.name = ""+cannonForce * (new Vector3(-Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z), -Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z), 0));
            cannonball.transform.position = new Vector3(3.98f, -2, 0);
            cannonball.transform.position = cannonForce * (new Vector3(-Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z), -Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z), 0));

            cannonball.SetActive(true);
        }
    }
    public void FixedUpdate()
    {
        //turn upward
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -1));
            //set max angle
            if (transform.eulerAngles.z < 270)
                transform.eulerAngles = new Vector3(0, 0, -90);
        }

        //turn downward
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1));
            //set min angle
            if (transform.eulerAngles.z > 359)
                transform.eulerAngles = new Vector3(0, 0, -1);
        }
            
        
    }
}
