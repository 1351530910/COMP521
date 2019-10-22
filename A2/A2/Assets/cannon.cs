using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    public const float cannonForce = 8.5f;
    public void FixedUpdate()
    {
        //turn upward
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -1));
            //set max angle
            if (transform.eulerAngles.z < 290)
                transform.eulerAngles = new Vector3(0, 0, -70);
        }

        //turn downward
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1));
            //set min angle
            if (transform.eulerAngles.z > 345)
                transform.eulerAngles = new Vector3(0, 0, -15);
        }
            
        //shoot cannonball
        if (Input.GetKey(KeyCode.Space)&&!Game.cannonball.activeSelf)
        {
            cannonBall.velocity = cannonForce*(new Vector3(-Mathf.Cos(Mathf.Deg2Rad* transform.eulerAngles.z),-Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z),0));
            Game.cannonball.transform.position = new Vector3(3.98f, -2, 0);
            Game.cannonball.SetActive(true);
        }
    }
}
