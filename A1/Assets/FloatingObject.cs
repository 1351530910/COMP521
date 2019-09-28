using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    const float period = 100f;  
    int time = 0; //current time
    void Start()
    {
        //start with a random height
        time = Random.Range(0, 360 * (int)period);
    }
    void FixedUpdate()
    {
        //floating up and down based on time, trig function
        transform.position = new Vector3(transform.position.x, Mathf.Sin(++time / period) / 2 + 1, transform.position.z);
        if (time > 360 * period)
        {
            time -= 360 * (int)period;
        }
    }
}
